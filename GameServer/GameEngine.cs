using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

namespace GameServer
{
    class GameEngine
    {
        private Timer _ticker;
        public HashSet<WebSocket> ClientSockets = new HashSet<WebSocket>();
        public GameState GameState = GameState.Instance;

        public GameEngine()
        {
            _ticker = new Timer(Tick, null, 0, 1000 / Config.SERVER_TICK);
        }

        private void Tick(object state)
        {
            GameState.i++;

            SendGameState();
        }

        private void SendGameState()
        {
            foreach (var socket in ClientSockets)
            {
                string json = JsonConvert.SerializeObject(GameState);
                ArraySegment<byte> bytes = new ArraySegment<byte>(Encoding.ASCII.GetBytes(json));

                socket.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}

