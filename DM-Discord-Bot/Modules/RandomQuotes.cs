using Discord;
using Discord.Commands;
using System;
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
            Quote randQuote = quoter.GetRandomQuote();
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle(randQuote.name)
                .WithDescription(randQuote.quote)
                .WithColor(Color.Red);

            await ReplyAsync("", false, builder);
        }

        [Command("add")]
        public async Task AddQuoteAsync(string name, string text)
        {
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Quote: " + text);
            quoter.AddQuote(name, text);

            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Added new quote by " + name)
                .WithCurrentTimestamp();
            await ReplyAsync("", false, builder);
        }

        [Command("help")]
        public async Task PrintHelpAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Random Quote Help")
                .AddInlineField("!randomquote", "This will randomly print a quote")
                .AddInlineField("!randomquote add \"name\" \"quote\"", "Add a quote. You must enter the name of who said it and the actual code")
                .AddInlineField("!randomquote getAll", "Prints out all the quotes");

            await ReplyAsync("", false, builder);
        }

        [Command("getAll")]
        public async Task GetAllQuotesAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("All quotes")
                .WithColor(Color.Red);
            for (int i = 0; i < quoter.GetElementNumber(); i++)
            {
                Quote q = quoter.GetQuote(i);
                builder.AddField(q.name, q.quote);
            }

            await ReplyAsync("", false, builder);
        }
    }
}
