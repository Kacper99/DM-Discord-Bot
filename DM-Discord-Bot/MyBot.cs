using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace DM_Discord_Bot
{
    class MyBot
    {
        private DiscordSocketClient client;
        private CommandService commands;
        private IServiceProvider services;

        public async Task RunBotAsync()
        {
            client = new DiscordSocketClient();
            commands = new CommandService();
            services = new ServiceCollection().AddSingleton(client).AddSingleton(commands).BuildServiceProvider();

            //Request the bot prefix
            Console.WriteLine("Enter bot token");
            string botToken = Console.ReadLine();

            //Event subscriptions
            client.Log += Log;

            await client.LoginAsync(TokenType.Bot, botToken);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);

            return Task.FromResult(0);
        }

        public async Task RegisterCommandAsync()
        {
            client.MessageReceived += HandleCommandAsync;

            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage sm)
        {
            var message = sm as SocketUserMessage;

            if (message == null || message.Author.IsBot) return;

            int smPos = 0;

            if (message.HasStringPrefix("km!", ref smPos) || message.HasMentionPrefix((client.CurrentUser), ref smPos))
            {
                var context = new SocketCommandContext(client, message);

                var result = await commands.ExecuteAsync(context, smPos, services);

                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);


                
            }
        }
        
    }
}
