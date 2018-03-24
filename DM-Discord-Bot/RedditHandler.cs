using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedditSharp;
using System.IO;
using RedditSharp.Things;

namespace DM_Discord_Bot
{
    class RedditHandler
    {
        WebAgent agent;
        Reddit reddit = new Reddit();

        /// <summary>
        /// Gets the details to initialise the reddit bot
        /// </summary>
        public RedditHandler()
        {
            //Read details from a file
            StreamReader reader = new StreamReader(@"C:\Users\Kacper\Desktop\info.txt");
            string name = reader.ReadLine();
            string password = reader.ReadLine();
            string clientID = reader.ReadLine();
            string clientSecret = reader.ReadLine();
            string redirectURL = reader.ReadLine();
            reader.Close();

            agent = new BotWebAgent(name, password, clientID, clientSecret, redirectURL);
        }

        /// <summary>
        /// Gets the top 5 posts from a subreddit 
        /// 
        /// Excludes stickied posts.
        /// </summary>
        /// <param name="targetSubreddit">Name of the subreddit to get the posts from</param>
        /// <returns>A list of the top 5 posts</returns>
        public List<Post> GetTopPosts(string targetSubreddit, int numOfPosts)
        {
            List<Post> posts = new List<Post>();
            Subreddit subreddit = reddit.GetSubreddit("/r/" + targetSubreddit);

            foreach(var post in subreddit.Hot.Take(numOfPosts + 2)) //Maximum of two stickies in a subreddit
            {
                if (!post.IsStickied)
                    posts.Add(post);
                if (posts.Count > numOfPosts) //If we get 5 posts break
                    break;
            }
            return posts; //TODO: Change this
        }

        /// <summary>
        /// Gets the top 5 rising posts from a subreddit
        /// </summary>
        /// <param name="targetSubreddit">Name of the subreddit to get the posts from</param>
        /// <returns>A list of the top 5 rising posts</returns>
        public List<Post> GetRisingPosts(string targetSubreddit, int numOfPosts)
        {
            List<Post> posts = new List<Post>();
            Subreddit subreddit = reddit.GetSubreddit("/r/" + targetSubreddit);

            foreach (var post in subreddit.Rising.Take(numOfPosts))
            {
                posts.Add(post);
            }

            return posts;
        }

        /// <summary>
        /// Gets the 5 newest posts from a subreddit
        /// </summary>
        /// <param name="targetSubreddit">Name of the subreddit to get the posts from</param>
        /// <returns>A list of the 5 newest posts</returns>
        public List<Post> GetNewPosts(string targetSubreddit, int numOfPosts)
        {
            List<Post> posts = new List<Post>();
            Subreddit subreddit = reddit.GetSubreddit("/r/" + targetSubreddit);

            foreach (var post in subreddit.New.Take(numOfPosts))
            {
                posts.Add(post);
            }

            return posts;
        }
    }
}
