using Discord.WebSocket;
using Discord.Commands;
using System;
using System.Threading.Tasks;
using Discord;

namespace DM_Discord_Bot
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient client;
        private CommandHandler handler;

        public async Task RunBotAsync()
        {
            client = new DiscordSocketClient();

            //Request the bot prefix
            //Console.WriteLine("Enter bot token");
            //string botToken = Console.ReadLine();

            client.Log += Log;
            await client.LoginAsync(TokenType.Bot, "NDI1MTAwNzY1NTI3Mjc3NTc4.DZFSTg.eqM62PP5Rv36UQEZxYgvVyIuuxM");
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
