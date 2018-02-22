using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

namespace GameServer
{
    static class GameEngine
    {
        private static Timer Ticker = new Timer(Tick, null, 0, 1000 / Config.SERVER_TICK);
        public static HashSet<WebSocket> ClientSockets = new HashSet<WebSocket>();
        
        private static void Tick(object state)
        {
            GameState.i++;

            SendGameState();
        }

        private static void SendGameState()
        {
            foreach (var socket in ClientSockets)
            {
                socket.SendAsync(new ArraySegment<byte>(BitConverter.GetBytes(GameState.i)), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}

