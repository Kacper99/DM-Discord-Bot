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
                .AddInlineField("!reddit top <subreddit> <number of post>", "This will print the top posts from a subreddit")
                .AddInlineField("!reddit rising <subreddit> <number of post>", "This will print the top rising posts from a subreddit")
                .AddInlineField("!reddit new <subreddit> <number of post>", "This iwll print the newest posts from a subreddit");

            await ReplyAsync("", false, builder);
        }

        /// <summary>
        /// Prints the top 5 posts from a subreddit
        /// </summary>
        /// <param name="subreddit">The subreddit to get the posts from</param>
        /// <returns></returns>
        [Command("top")]
        public async Task TopPostsAsync(string subreddit, int numOfPosts)
        {
            List<Post> posts = redditHandler.GetTopPosts(subreddit, numOfPosts);
            foreach(Post post in posts)
            {
                await Context.Channel.SendMessageAsync(post.Title + " " + post.Url);
            }
        }

        /// <summary>
        /// Prints the top 5 rising posts from a subreddit
        /// </summary>
        /// <param name="subreddit">The subreddit to get the posts from</param>
        /// <returns></returns>
        [Command("rising")]
        public async Task RisingPostsAsync(string subreddit, int numOfPosts)
        {
            List<Post> posts = redditHandler.GetRisingPosts(subreddit, numOfPosts);
            foreach (Post post in posts)
            {
                await Context.Channel.SendMessageAsync(post.Title + " " + post.Url);
            }
        }

        /// <summary>
        /// Gets the 5 newest posts from a subreddit
        /// </summary>
        /// <param name="subreddit">The subreddit to get the posts from</param>
        /// <returns></returns>
        [Command("new")]
        public async Task NewPostsAsync(string subreddit, int numOfPosts)
        {
            List<Post> posts = redditHandler.GetNewPosts(subreddit, numOfPosts);
            foreach (Post post in posts)
            {
                await Context.Channel.SendMessageAsync(post.Title + " " + post.Url);
            }
        }
    }
}
