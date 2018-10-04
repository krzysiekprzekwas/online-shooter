using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameServer.Game;
using GameServer.Hubs;
using GameServer.Models;
using GameServer.States;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog.Extensions.Logging;

namespace GameServer
{
    public class Startup
    {
        private GameEngine _gameEngine;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
        }

        public static IServiceProvider BuildDi()
        {
            var services = new ServiceCollection();

            //Runner is the custom class
            services.AddTransient<GameEngine>();

            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.AddLogging((builder) => builder.SetMinimumLevel(LogLevel.Trace));

            var serviceProvider = services.BuildServiceProvider();

            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            //configure NLog
            loggerFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
            NLog.LogManager.LoadConfiguration("nlog.config");

            return serviceProvider;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseFileServer();

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/game");
            });
        }

        private async Task Echo(HttpContext context, WebSocket webSocket)
        {
            // Player connecting - sending connect request
            var buffer = new byte[Config.BUFFER_SIZE];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            string connectionRequest = Encoding.ASCII.GetString(buffer);
            dynamic json = JsonConvert.DeserializeObject(connectionRequest);

            Player player = new Player
            {
                Name = json.Name,
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

