using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using GameServer.Game;
using Newtonsoft.Json;
using GameServer.Models;

using GameServer.States;

namespace GameServer.Hubs
{
    public class GameHub : Hub
    {

        private GameEngine _gameEngine = new GameEngine();

        public void Send(string name, string message)
        {
            Clients.All.SendAsync("message", name, message);
        }


        public void OnOpen(string name)
        {
            dynamic json = JsonConvert.DeserializeObject(name);

            Player player = new Player
            {
                Name = json.Name,
                ConnectionId = Context.ConnectionId
            };

            _gameEngine.ConnectPlayer(player);

            // Confirmation status

            var connectionConfirmationResponse = new ConfirmConnectionResponse
            {
                Type = "connected",
                PlayerId = player.Id,
                Config = Config.Instance
            };

            Clients.Caller.SendAsync("connectConfirmation", connectionConfirmationResponse);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
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
        public Config Config;
    }
}