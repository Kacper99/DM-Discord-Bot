using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace DM_Discord_Bot.Modules
{
    [Group("randomquote")]
    public class RandomQuotes : ModuleBase<SocketCommandContext>
    {
		Quoter quoter = new Quoter(@"C:\Users\Kacper\Desktop\DumbQuotes.xml"); //Windows version

        /// <summary>
        /// Gets a rando quote and prints it
        /// </summary>
        /// <returns></returns>
        [Command]
        public async Task DefaultCommand()
        {
            Quote randQuote = quoter.GetRandomQuote(); //Random quote from the quote list
            EmbedBuilder builder = new EmbedBuilder(); //Build a new message with the quote
            builder.WithTitle(randQuote.name)
                .WithDescription(randQuote.quote)
                .WithColor(Color.Red);

            await ReplyAsync("", false, builder);
        }

        /// <summary>
        /// Takes the name and the quote and adds it to the quote list
        /// </summary>
        /// <param name="name">Name of the person who said the quote (or context)</param>
        /// <param name="text">The quote</param>
        /// <returns></returns>
        [Command("add")]
        public async Task AddQuoteAsync(string name, string text)
        {
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Quote: " + text);
            quoter.AddQuote(name, text); //Add the quote to the quote list

            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Added new quote by " + name)
                .WithCurrentTimestamp()
                .WithColor(Color.Red);
            await ReplyAsync("", false, builder);
        }

        /// <summary>
        /// Removes a quote from the quote list
        /// </summary>
        /// <param name="name">Name of the quote to remove</param>
        /// <param name="text">Text from the quote to remove</param>
        /// <returns></returns>
        [Command("remove")]
        public async Task RemoveQuoteAsync(string name, string text)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Removing quote")
                .WithColor(Color.Red);
            Quote quoteToRemove = new Quote(name, text);
            if (quoter.RemoveQuote(quoteToRemove)) //If the quote is removed tell the user what was removed, if not give error
            {
                builder.WithDescription("Quote: " + text + " by " + name + " was removed");
            }
            else
            {
                builder.WithDescription("Quote does not exist");
            }
            await ReplyAsync("", false, builder);
        }

        /// <summary>
        /// Prints help for reddit commands
        /// </summary>
        /// <returns></returns>
        [Command("help")]
        public async Task PrintHelpAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Random Quote Help")
                .WithColor(Color.Red)
                .AddInlineField("!randomquote", "This will randomly print a quote")
                .AddInlineField("!randomquote add \"name\" \"quote\"", "Add a quote. You must enter the name of who said it and the actual code")
                .AddInlineField("!randomquote getAll", "Prints out all the quotes");

            await ReplyAsync("", false, builder);
        }

        /// <summary>
        /// Get all quotes
        /// </summary>
        /// <returns></returns>
        [Command("getAll")]
        public async Task GetAllQuotesAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("All quotes")
                .WithColor(Color.Red);
            for (int i = 0; i < quoter.GetElementNumber(); i++) //Iterate through every quote and add it to the builder
            {
                Quote q = quoter.GetQuote(i);
                builder.AddField(q.name, q.quote);
            }

            await ReplyAsync("", false, builder);
        }
    }
}
