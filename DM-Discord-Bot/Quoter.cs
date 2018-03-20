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

        //Will attempt to load the quotes from that file
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

        public Quote GetQuote(int index)
        {
            if (index < 0 || index > quoteList.Count - 1)
                throw new IndexOutOfRangeException("Index out of quote list range");

            return quoteList[index];
        }

        public void AddQuote(string name, string text)
        {
            quoteList.Add(new Quote(name, text));
            this.SaveQuotes(lastPath);
        }

        public Quote GetRandomQuote()
        {
            Random rand = new Random();
            int randomNum = rand.Next(quoteList.Count);
            Console.WriteLine(quoteList.Count + " : " + randomNum);
            return quoteList[randomNum];
        }

        public int GetElementNumber()
        {
            return quoteList.Count;
        }

        public void SaveQuotes(string path)
        {
            Stream stream = File.Open(path, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter(); //Use a binary formatter to serialize the data
            bf.Serialize(stream, quoteList); //Serialize the quoteList into the stream
            stream.Close();
            quoteList.Clear();
            lastPath = path;
        }

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
