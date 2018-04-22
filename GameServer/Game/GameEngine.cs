using GameServer.Physics;
using GameServer.States;
using GameServer.World;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

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

        public Player ConnectPlayer(string connectionRequest)
        {
            dynamic json = JsonConvert.DeserializeObject(connectionRequest);

            Player player = new Player
            {
                Name = json.Name,
                Position = new System.Numerics.Vector3(0, Config.PLAYER_SIZE / 2, 0)
            };
            GameState.value.Players.Add(player);
            Console.WriteLine(String.Format("[INFO] Player #{0} ({1}) connected.", player.Id, player.Name));

            GameEvents.OnPlayerConnected(player);

            return player;
        }

        public void DisconnectPlayer(Player player)
        {
            Console.WriteLine(String.Format("[INFO] Player #{0} disconnected.", player.Id));

            GameEvents.OnPlayerDisconnected(player);

            GameState.value.Players.Remove(player);
        }
    }
}

