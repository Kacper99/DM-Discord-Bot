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
            quoteList.Add(new Quote("Kacper", "Something jsd"));
            quoteList.Add(new Quote("dfsd", "sdfds jsfddfsd"));

            //Using XML
            using (Stream fs = new FileStream(@"C:\Users\Kacper\Desktop\DumbQuotes3.xml", FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Quote>));
                serializer.Serialize(fs, quoteList);
            }
        }
    }
}
