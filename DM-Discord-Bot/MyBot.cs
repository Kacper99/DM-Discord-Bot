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
    public class MyBot
    {
        private DiscordSocketClient client;
        private CommandHandler handler;

        public async Task RunBotAsync()
        {
            client = new DiscordSocketClient();

            //Request the bot prefix
            Console.WriteLine("Enter bot token");
            string botToken = Console.ReadLine();

            //Event subscriptions
            client.Log += Log;

            await client.LoginAsync(TokenType.Bot, botToken);
            await client.StartAsync();

            handler = new CommandHandler(client);
            await Task.Delay(-1);
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);

            return Task.FromResult(0);
        }
        
    }
}
