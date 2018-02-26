﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

namespace GameServer.States
{
    public static class StateController
    {
        private static void SendState(dynamic state, WebSocket webSocket)
        {
            string json = JsonConvert.SerializeObject(state);
            ArraySegment<byte> bytes = new ArraySegment<byte>(Encoding.ASCII.GetBytes(json));

            webSocket.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public static void SendGameState(WebSocket webSocket)
        {
            var gameStateResponse = new
            {
                Type = "gamestate",
                GameState = GameState.Instance,
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
        
        public static void ReceiveState(byte[] buffer, Player player)
        {
            string request = Encoding.ASCII.GetString(buffer).Trim((char)0);
            dynamic jsonObject = JsonConvert.DeserializeObject(request);

            //Console.WriteLine("Received " + jsonObject.Type + " request from player #" + player.Id);

            if (jsonObject.Type == "playerstate")
                ProcessPlayerState(jsonObject, player);
        }

        public static void ProcessPlayerState(dynamic playerState, Player player)
        {
            foreach (var key in playerState.Keys)
            {
                int keyNumber = Int32.Parse(key.Name);
                bool pressed = key.Value;

                player.Keys[keyNumber] = pressed;
            }
        }
    }
}