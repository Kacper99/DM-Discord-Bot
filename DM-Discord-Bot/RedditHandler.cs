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

        public List<Post> GetTopPosts(string targetSubreddit)
        {
            List<Post> posts = new List<Post>();
            Subreddit subreddit = reddit.GetSubreddit("/r/" + targetSubreddit);

            foreach(var post in subreddit.Hot.Take(7)) //Maximum of two stickies in a subreddit
            {
                if (!post.IsStickied)
                    posts.Add(post);
                if (posts.Count > 5) //If we get 5 posts break
                    break;
            }
            return posts; //TODO: Change this
        }
    }
}
