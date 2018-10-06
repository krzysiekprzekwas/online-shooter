using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameServer.Models;
using Newtonsoft.Json;

namespace GameServer.States
{
    public class StateController
    {

        private static void SendState(dynamic state, WebSocket webSocket)
        {
            string json = JsonConvert.SerializeObject(state);
            ArraySegment<byte> bytes = new ArraySegment<byte>(Encoding.ASCII.GetBytes(json));

            //webSocket.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public static void SendGameState(WebSocket webSocket)
        {
            var gameStateResponse = new
            {
                Type = "gamestate",
                GameState = GameState.Instance
            };

            SendState(gameStateResponse, webSocket);
        }

        public static void SendMapState(WebSocket webSocket)
        {
            var mapStateResponse = new
            {
                Type = "mapstate",
                MapState = MapState.Instance,
            };

            SendState(mapStateResponse, webSocket);
        }

        public static void ReceiveState(byte[] buffer, Player player, WebSocket webSocket)
        {
            string request = Encoding.ASCII.GetString(buffer).Trim((char)0);
            dynamic jsonObject = JsonConvert.DeserializeObject(request);


            if (jsonObject.Type == "playerstate")
            {
                ProcessPlayerState(jsonObject, player);
                long pingStart = jsonObject.PingStart;
                StateController.SendReceivedState(webSocket, pingStart);
            }
            else if(jsonObject.Type == "connect")
            {
                player.Name = jsonObject.Name.ToString();
            }
            else
                Console.WriteLine("Received unknown " + jsonObject.Type + " request from player #" + player.Id);
        }

        public static void ProcessPlayerState(dynamic playerState, Player player)
        {
            player.Keys = new List<KeyEnum>();
            foreach (var key in playerState.Keys)
                player.Keys.Add((KeyEnum)int.Parse(key.Value));
            
            player.Angle = playerState.Angle.Value;
        }


        internal static void SendReceivedState(WebSocket webSocket, long pingStart)
        {
            var receivedConfirmationResponse = new
            {
                Type = "received",
                PingStart = pingStart
            };

            SendState(receivedConfirmationResponse, webSocket);
        }
    }
}
