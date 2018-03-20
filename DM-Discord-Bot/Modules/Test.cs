using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DM_Discord_Bot.Modules
{
    public class Test : ModuleBase<SocketCommandContext>
    {
        [Command("Test")]
        public async Task TestAsync()
        {
            await Context.Channel.SendMessageAsync("Hello World");
        }

        [Command("Test2")]
        public async Task TestTwoAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Test")
                .WithDescription("Test message")
                .WithColor(Color.Red);

            await ReplyAsync("", false, builder.Build());
        }

        [Command("Test3")]
        public async Task TestThreeAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.AddField("Field 1", "Test")
                .AddInlineField("Field 2", "Test")
                .AddInlineField("Field 3", "Test");

            await ReplyAsync("", false, builder.Build());
        }
    }
}
