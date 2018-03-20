using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace DM_Discord_Bot
{
    public class CommandHandler
    {
        private DiscordSocketClient client;
        private CommandService service;

        public CommandHandler(DiscordSocketClient _client)
        {
            client = _client;

            service = new CommandService(); //Initiate a new command service
            service.AddModulesAsync(Assembly.GetEntryAssembly());

            client.MessageReceived += HandleCommandAsync; //When a message is recieved run HandleCommandAsync
        }

        public async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage; //The message the user sent
            if (msg == null) return; //If the msg is null then don't bother doing anything else

            var context = new SocketCommandContext(client, msg);

            int argPos = 0;
            if(msg.HasCharPrefix('!', ref argPos))
            {
                var result = await service.ExecuteAsync(context, argPos);

                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    await context.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
        }
    }
}
