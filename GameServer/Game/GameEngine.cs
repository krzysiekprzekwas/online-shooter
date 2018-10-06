using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using GameServer.Models;
using GameServer.Physics;
using GameServer.States;
using GameServer.World;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GameServer.Game
{
    public class GameEngine
    {
        public Timer Ticker;
        public List<Player> Players = new List<Player>();
        public GameState GameState = GameState.Instance;
        public GameEvents GameEvents;
        public PhysicsEngine PhysicsEngine;
        private Random random;
        
        public GameEngine()
        {
            GameEvents = new GameEvents(this);
            PhysicsEngine = new PhysicsEngine(this);
            random = new Random();
            WorldLoader.LoadMap();
            Ticker = new Timer(Tick, null, 0, 1000 / Config.SERVER_TICK);
        }

        private void Tick(object state)
        {
            PhysicsEngine.ApplyPhysics();

            var currentPlayers = new Player[GameState.Instance.Players.Count];
            GameState.Instance.Players.CopyTo(currentPlayers);

            // Send game state for each client
            foreach (var player in currentPlayers)
            {
                //if (player.WebSocket.State != WebSocketState.Open)
                //{
                //    DisconnectPlayer(player);
                //    Console.WriteLine($"Connection closed by client {player}");
                //}
                //else
                    StateController.SendGameState(player.WebSocket);

            }
        }

        public bool ConnectPlayer(Player player)
        {

            GameState.Instance.Players.Add(player);

            GameEvents.OnPlayerConnected(player);

            return true;
        }

        public void DisconnectPlayer(Player player)
        {
            GameEvents.OnPlayerDisconnected(player);

            GameState.Instance.Players.Remove(player);
        }
    }
}
