﻿using GameServer.States;
using GameServer.World;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

namespace GameServer.Game
{
    class GameEngine
    {
        private Timer _ticker;
        public HashSet<WebSocket> ClientSockets = new HashSet<WebSocket>();
        public GameState GameState = GameState.Instance;
        public GameEvents GameEvents;
        private Random random;

        public GameEngine()
        {
            _ticker = new Timer(Tick, null, 0, 1000 / Config.SERVER_TICK);
            GameEvents = new GameEvents(this);
            random = new Random();
            WorldLoader.LoadMap();
        }

        private void Tick(object state)
        {
            foreach (var player in GameState.Instance.Players)
            {
                player.X += 2 * (random.NextDouble() - 0.5);
                player.Z += 2 * (random.NextDouble() - 0.5);
            }

            SendGameState();
        }

        private void SendGameState()
        {
            var gameStateResponse = new {
                Type = "gamestate",
                GameState = GameState,
            };
            
            string json = JsonConvert.SerializeObject(gameStateResponse);
            ArraySegment<byte> bytes = new ArraySegment<byte>(Encoding.ASCII.GetBytes(json));

            foreach (var socket in ClientSockets)
            {
                socket.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        public Player ConnectPlayer(string connectionRequest)
        {
            dynamic json = JsonConvert.DeserializeObject(connectionRequest);

            Player player = new Player
            {
                Name = json.Name
            };
            GameState.Players.Add(player);
            Console.WriteLine(String.Format("[INFO] Player #{0} ({1}) connected.", player.Id, player.Name));

            GameEvents.OnPlayerConnected(player);

            return player;
        }

        public void DisconnectPlayer(Player player)
        {
            Console.WriteLine(String.Format("[INFO] Player #{0} disconnected.", player.Id));

            GameEvents.OnPlayerDisconnected(player);

            GameState.Players.Remove(player);
        }
    }
}

