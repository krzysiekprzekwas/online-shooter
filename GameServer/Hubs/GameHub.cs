using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using GameServer.Game;
using Newtonsoft.Json;
using GameServer.Models;
using System.Collections.Generic;
using GameServer.States;

namespace GameServer.Hubs
{
    public class GameHub : Hub
    {
        IGameEngine _gameEngine;
        static IConfig _config;
        static IMapState _mapState;

        public GameHub(IGameEngine gameEngine, IConfig config, IMapState mapState)
        {
            _gameEngine = gameEngine;
            _config = config;
            _mapState = mapState;
        }

        public void Send(string name, string message)
        {
            Clients.All.SendAsync("message", name, message);
        }

        public void OnOpen(string name)
        {
            Player player = new Player(_config)
            {
                Name = name,
                ConnectionId = Context.ConnectionId
            };
            SpawnService.SpawnPlayer(player);

            _gameEngine.AddPlayer(player);
            Console.WriteLine($"Registered player: {player.Name} ({player.Id})");
            // Confirmation status

            var connectionConfirmationResponse = new ConfirmConnectionResponse
            {
                Type = "connected",
                PlayerId = player.Id,
                Config = _config,
                MapState = _mapState,
                Weapons = WeaponService.Weapons
            };

            Clients.Caller.SendAsync("connectConfirmation", connectionConfirmationResponse);

            Clients.All.SendAsync("newPlayerConnected", player.Name);
        }

        public void ClientStateUpdate(string clientState)
        {
            // Recive state

            string request = clientState.Trim((char)0);
            dynamic playerState = JsonConvert.DeserializeObject(request);

            var player = _gameEngine.GameState.Players.Find(x => x.ConnectionId == Context.ConnectionId);

            // Ignore update from dead or untracked player
            if ( player == null || !player.IsAlive)
            {
                return;
            }
            
            // Process state

            player.Keys = new List<KeyEnum>();
            foreach (var key in playerState.Keys)
                player.Keys.Add((KeyEnum)key.Value);

            player.MouseClicked = playerState.MouseClicked.Value;

            player.Angle = playerState.Angle.Value;
        }

        public void MeasureLatency(string clientTime)
        {
            Clients.Caller.SendAsync("updateLatency", clientTime);
        }

        public void SendMapState(string clientState)
        {
            Clients.All.SendAsync("updateMapState");
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var player = _gameEngine.GameState.Players.Find(x => x.ConnectionId == Context.ConnectionId);
            _gameEngine.RemovePlayer(player);
            Console.WriteLine($"Removed player: {player.Name} ({player.Id})");
            Clients.All.SendAsync("playerDisconnected", player.Name);
            return base.OnDisconnectedAsync(exception);
        }
    }

    public class ConfirmConnectionResponse
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("playerId")]
        public int PlayerId;

        [JsonProperty("config")]
        public IConfig Config;

        [JsonProperty("mapState")]
        public IMapState MapState;
        
        [JsonProperty("weapons")]
        public Dictionary<int, Weapon> Weapons;
    }
}