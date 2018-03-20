using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_Discord_Bot.Modules
{

    [Group("randomquote")]
    public class RandomQuotes : ModuleBase<SocketCommandContext>
    {
        Quoter quoter = new Quoter(@"C:\Users\Kacper\Desktop\DumbQuotes.xml");

        [Command]
        public async Task DefaultCommand()
        {

            Quote randQuote = quoter.getRandomQuote();
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle(randQuote.name)
                .WithDescription(randQuote.quote)
                .WithColor(Color.Red);

            await ReplyAsync("", false, builder);
        }
    }
}
