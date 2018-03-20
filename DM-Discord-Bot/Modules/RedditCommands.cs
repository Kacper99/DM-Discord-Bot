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
    class RedditCommands : ModuleBase<SocketCommandContext>
    {
        RedditHandler redditHandler = new RedditHandler();

        /*
        [Command]
        public async Task DefaultCommandAsync()
        {

        }

        [Command("help")]
        public async Task HelpCommandAsync()
        {

        }
        */

        [Command("top")]
        public async Task TopPostsAsync(string subreddit)
        {
            List<Post> posts = redditHandler.GetTopPosts(subreddit);
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Top posts from " + subreddit)
                .WithColor(Color.Orange);

            foreach(Post post in posts)
            {
                builder.AddInlineField(post.Title, post.Url);
            }

            await ReplyAsync("", false, builder);
        }
    }
}
