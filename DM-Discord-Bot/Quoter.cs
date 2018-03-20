using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace DM_Discord_Bot
{
    public class Quoter
    {
        List<Quote> quoteList;

        public Quoter()
        {
            quoteList = new List<Quote>();
        }

        //Will attempt to load the quotes from that file
        public Quoter(string path)
        {
            quoteList = new List<Quote>();
            loadQuotes(path);
        }

        public Quote getQuote(int index)
        {
            if (index < 0 || index > quoteList.Count - 1)
                throw new IndexOutOfRangeException("Index out of quote list range");

            return quoteList[index];
        }

        public Quote getRandomQuote()
        {
            Random rand = new Random();
            int randomNum = rand.Next(quoteList.Capacity - 1);
            Console.WriteLine(quoteList.Count + " : " + randomNum);
            return quoteList[randomNum];
        }

        public void saveQuotes(string path)
        {
            Stream stream = File.Open(path, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, quoteList);
            stream.Close();
            quoteList.Clear();
        }

        public void loadQuotes(string path)
        {
            Stream stream = File.Open(path, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            quoteList = (List<Quote>)bf.Deserialize(stream);
            stream.Close();
        }
    }
}
