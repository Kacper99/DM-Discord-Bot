using Discord.WebSocket;
using Discord;
using System;
using System.Threading.Tasks;

namespace DM_Discord_Bot
{
    class Program
    {

        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult(); //Initialising the bot

        private DiscordSocketClient client;
        private CommandHandler handler;

        public async Task RunBotAsync()
        {
            client = new DiscordSocketClient(); //Create a new discord client

            //Request the bot prefix
            Console.WriteLine("Enter bot token");
            string botToken = Console.ReadLine();

            client.Log += Log; //Print bot status
            client.UserJoined += AnnounceUserJoined; //When a user joins, announce that they joined

            await client.LoginAsync(TokenType.Bot, botToken); //Log in bot
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
