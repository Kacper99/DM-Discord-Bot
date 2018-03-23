using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;
using RedditSharp;
using System.Collections.Generic;
using RedditSharp.Things;

namespace DM_Discord_Bot.Modules
{
    [Group("reddit")]
    public class RedditCommands : ModuleBase<SocketCommandContext>
    {
        RedditHandler redditHandler = new RedditHandler();
        
        /// <summary>
        /// No default command, instead print an error message
        /// </summary>
        /// <returns>Error message</returns>
        [Command]
        public async Task DefaultCommandAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Error: No command")
                .WithDescription("Type !reddit help for commands");

            await ReplyAsync("", false, builder);
        }

        [Command("help")]
        public async Task HelpCommandAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Reddit command help")
                .AddInlineField("!reddit top <subreddit>", "This will print the top 5 posts from that subreddit");
        }

        /// <summary>
        /// Prints the top 5 posts from a subreddit
        /// </summary>
        /// <param name="subreddit">The subreddit to get the posts from</param>
        /// <returns></returns>
        [Command("top")]
        public async Task TopPostsAsync(string subreddit)
        {
            List<Post> posts = redditHandler.GetTopPosts(subreddit);
            foreach(Post post in posts)
            {
                await Context.Channel.SendMessageAsync(post.Title + " " + post.Url);
            }
        }
    }
}
