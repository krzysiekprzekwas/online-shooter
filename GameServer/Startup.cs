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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            

            //// Send back initial status
            //StateController.SendConnectedConfirmation(webSocket, player);
            //StateController.SendMapState(webSocket);

            //// Wait for other player messages
            //while (!result.CloseStatus.HasValue)
            //{
            //    //await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

            //    StateController.ReceiveState(buffer, player, webSocket);
            //    Array.Clear(buffer, 0, result.Count);

            //    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            //}

            //// Player disconnected
            //await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);

            //_gameEngine.DisconnectPlayer(player);
        }
    }
}

