﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DM_Discord_Bot
{
    [Serializable]
    public class Quote : ISerializable
    {
        public string name { get; set; }
        public string quote { get; set; }

        public Quote() { }

        public Quote(string _name, string _quote)
        {
            name = _name;
            quote = _quote;
        }

        public override string ToString()
        {
            return name + " " + quote;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Quote q = (Quote)obj;
            return (name == q.name) && (quote == q.quote);
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", name);
            info.AddValue("Text", quote);

        }

        public Quote(SerializationInfo info, StreamingContext context)
        {
            name = (string)info.GetValue("Name", typeof(string));
            quote = (string)info.GetValue("Text", typeof(string));
        }
    }
}
