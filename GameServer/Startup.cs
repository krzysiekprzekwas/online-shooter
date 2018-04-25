using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameServer.Game;
using GameServer.Models;
using GameServer.States;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GameServer
{
    public class Startup
    {
        private GameEngine _gameEngine;

        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            _gameEngine = new GameEngine();

            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120),
                ReceiveBufferSize = Config.BUFFER_SIZE
            };
            app.UseWebSockets(webSocketOptions);

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                                   ForwardedHeaders.XForwardedProto
            });

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/ws")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        await Echo(context, webSocket);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                else
                {
                    await next();
                }

            });
        }

        private async Task Echo(HttpContext context, WebSocket webSocket)
        {

            _gameEngine.ClientSockets.Add(webSocket);

            // Player connecting - sending connect request
            var buffer = new byte[Config.BUFFER_SIZE];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            string connectionRequest = Encoding.ASCII.GetString(buffer);
            dynamic json = JsonConvert.DeserializeObject(connectionRequest);

            Player player = new Player
            {
                Name = json.Name,
                Position = new System.Numerics.Vector3(0, Config.PLAYER_SIZE / 2, 0),
                WebSocket = webSocket,
                IpAddress = context.Connection.RemoteIpAddress
            };

            _gameEngine.ConnectPlayer(player);

            // Send back initial status
            StateController.SendConnectedConfirmation(webSocket, player);
            StateController.SendMapState(webSocket);

            // Wait for other player messages
            while (!result.CloseStatus.HasValue)
            {
                //await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

                StateController.ReceiveState(buffer, player, webSocket);
                Array.Clear(buffer, 0, result.Count);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            // Player disconnected
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);

            _gameEngine.DisconnectPlayer(player);

        }
    }
}

