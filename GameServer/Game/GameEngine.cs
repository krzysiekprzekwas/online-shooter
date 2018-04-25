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
using Newtonsoft.Json;

namespace GameServer.Game
{
    public class GameEngine
    {
        private Timer _ticker;
        public List<WebSocket> ClientSockets = new List<WebSocket>();
        public GameState GameState = GameState.Instance;
        public GameEvents GameEvents;
        public PhysicsEngine PhysicsEngine;
        private Random random;

        public GameEngine()
        {
            _ticker = new Timer(Tick, null, 0, 1000 / Config.SERVER_TICK);
            GameEvents = new GameEvents(this);
            PhysicsEngine = new PhysicsEngine(this);
            random = new Random();

            WorldLoader.LoadMap();
        }

        private void Tick(object state)
        {
            PhysicsEngine.ApplyPhysics();

            WebSocket[] currentSockets = new WebSocket[ClientSockets.Count];
            ClientSockets.CopyTo(currentSockets);

            // Send game state for each client
            foreach (var socket in currentSockets)
            {
                StateController.SendGameState(socket);
            }
        }

        public bool ConnectPlayer(Player player)
        {
            GameState.value.Players.Add(player);
            Console.WriteLine(string.Format("[INFO] Player #{0} ({1}) IP={2} connected.", player.Id, player.Name, player.IpAddress));

            GameEvents.OnPlayerConnected(player);

            return true;
        }

        public void DisconnectPlayer(Player player)
        {
            Console.WriteLine(String.Format("[INFO] Player #{0} disconnected.", player.Id));

            GameEvents.OnPlayerDisconnected(player);

            GameState.value.Players.Remove(player);
        }
    }
}
