using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DM_Discord_Bot.Modules
{
    public class Help : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task HelpAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Dank memes bot commands")
                .WithDescription("Type help after any of these commands to see the help section for that command")
                .AddInlineField("!reddit", "Reddit commands")
                .AddInlineField("!randomquote", "Prints a random quote");

            await ReplyAsync("", false, builder);
        }
    }
}
