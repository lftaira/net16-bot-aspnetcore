using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBotCore.Logic
{
    [BsonIgnoreExtraElements]
    public class SimpleMessage
    {
        [BsonElement("id_usuario")]
        public string Id_Usuario { get; }
        [BsonElement("user")]
        public string User { get; }
        [BsonElement("text")]
        public string Text { get; }

        public SimpleMessage(string id, string username, string text)
        {
            this.Id_Usuario = id;
            this.User = username;
            this.Text = text;
        }
    }
}