using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DM_Discord_Bot
{
    public class Quoter
    {
        
        List<Quote> quoteList; //Store all the quotes
        string lastPath; //Store the last path that was used to store the quotes

        public Quoter()
        {
            quoteList = new List<Quote>();
        }

        /// <summary>
        /// Attempts to load quotes from the file and saves them in the quote list
        /// </summary>
        /// <param name="path">Path where files are stored</param>
        public Quoter(string path)
        {
            quoteList = new List<Quote>();
            try
            {
                LoadQuotes(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            lastPath = path;
        }

        /// <summary>
        /// Gets a quote from a certain index
        /// </summary>
        /// <param name="index">Index to where get the quote from</param>
        /// <returns>The quote object from that index</returns>
        public Quote GetQuote(int index)
        {
            if (index < 0 || index > quoteList.Count - 1)
                throw new IndexOutOfRangeException("Index out of quote list range");

            return quoteList[index];
        }

        /// <summary>
        /// Adds a quote to the quote list and saves it.
        /// </summary>
        /// <param name="name">The name of the person who said the quote</param>
        /// <param name="text">The text in the quote</param>
        public void AddQuote(string name, string text)
        {
            quoteList.Add(new Quote(name, text));
            this.SaveQuotes(lastPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quote"></param>
        /// <returns>Returns whether the element was deleted</returns>
        public bool RemoveQuote(Quote quote)
        {
            foreach(Quote q in quoteList)
            {
                if (q.Equals(quote))
                {
                    quoteList.Remove(q);
                    SaveQuotes(lastPath);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets a random quote from the quote list
        /// </summary>
        /// <returns>A random quote from the quote list</returns>
        public Quote GetRandomQuote()
        {
            Random rand = new Random();
            int randomNum = rand.Next(quoteList.Count);
            Console.WriteLine(quoteList.Count + " : " + randomNum);
            return quoteList[randomNum];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The number of elements in the quote list</returns>
        public int GetElementNumber()
        {
            return quoteList.Count;
        }

        /// <summary>
        /// Saves all the quotes to a file
        /// </summary>
        /// <param name="path">File path to save to</param>
        public void SaveQuotes(string path)
        {
            Stream stream = File.Open(path, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter(); //Use a binary formatter to serialize the data
            bf.Serialize(stream, quoteList); //Serialize the quoteList into the stream
            stream.Close();
            quoteList.Clear();
            lastPath = path;
        }

        /// <summary>
        /// Loads all the quotes from file
        /// </summary>
        /// <param name="path">Path where quotes are located</param>
        public void LoadQuotes(string path)
        {
            Stream stream = File.Open(path, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            if (stream.Length == 0) //If the file is empty throw an excpetion.
            {
                stream.Close();
                throw new Exception("File is empty, cannot load any quotes");
            }
            quoteList = (List<Quote>)bf.Deserialize(stream);
            stream.Close();
            lastPath = path;
        }
    }
}
