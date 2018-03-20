using Discord.WebSocket;
using Discord.Commands;
using System;
using System.Threading.Tasks;
using Discord;

namespace DM_Discord_Bot
{
    class Program
    {
        /*
        static void Main(string[] args)
        {
            Quoter quoter = new Quoter(@"C:\Users\Kacper\Desktop\DumbQuotes.xml");
            Console.ReadLine();
        }
        */

        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient client;
        private CommandHandler handler;

        public async Task RunBotAsync()
        {
            client = new DiscordSocketClient();

            //Request the bot prefix
            Console.WriteLine("Enter bot token");
            string botToken = Console.ReadLine();

            client.Log += Log;
            client.UserJoined += AnnounceUserJoined;

            await client.LoginAsync(TokenType.Bot, botToken);
            await client.StartAsync();

            handler = new CommandHandler(client);

            await Task.Delay(-1);
        }

        private async Task AnnounceUserJoined(SocketGuildUser user)
        {
            var guild = user.Guild;
            var channel = guild.DefaultChannel;
            await channel.SendMessageAsync($"Welcome, {user.Mention}, you have now been placed on an FBI watch list");
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);

            return Task.FromResult(0);
        }
    }
}
