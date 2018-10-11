using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using GameServer.Hubs;
using GameServer.Models;
using GameServer.Physics;
using GameServer.States;
using GameServer.World;
using Microsoft.AspNetCore.SignalR;

namespace GameServer.Game
{
    public interface IGameEngine
    {
        bool AddPlayer(Player player);

        bool RemovePlayer(Player player);

        GameState GameState { get; }
    }

    public class GameEngine : IGameEngine
    {
        public Timer Ticker;
        public List<Player> Players = new List<Player>();
        private GameState _gameState = GameState.Instance;
        public GameEvents GameEvents;
        public PhysicsEngine PhysicsEngine;
        private Random random;
        private readonly IHubContext<GameHub> _hubContext;

        GameState IGameEngine.GameState { get => _gameState; }

        public GameEngine(IHubContext<GameHub> hubContext)
        {
            _hubContext = hubContext;
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

            // Send game state for every connected client
             _hubContext.Clients.All.SendAsync("updateGameState", GameState.Instance);
        }

        public bool AddPlayer(Player player)
        {
            GameState.Instance.Players.Add(player);

            GameEvents.OnPlayerAdded(player);

            return true;
        }

        public bool RemovePlayer(Player player)
        {
            GameEvents.OnPlayerRemoved(player);

            GameState.Instance.Players.Remove(player);

            return true;
        }
    }
}
