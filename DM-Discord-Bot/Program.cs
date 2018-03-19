using Discord.WebSocket;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace DM_Discord_Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            MyBot bot = new MyBot();
            bot.RunBotAsync().GetAwaiter().GetResult();
        }
    }
}
