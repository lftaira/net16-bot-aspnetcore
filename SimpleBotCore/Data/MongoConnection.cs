using Microsoft.Bot.Schema;
using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBotCore.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Data
{
    public class MongoDataAccess : MongoClient
    {
        IMongoClient _mongoClient;

        public MongoDataAccess(string connString)
            : base(connString)
        {
        }

        public void InserirBase(String dbName, String Table, SimpleMessage mensagem)
        {
            var bDocument = new BsonDocument()
            {
                {"ID_mensagem", mensagem.Id },
                {"Nome", mensagem.User },
                {"Mesangem", mensagem.Text }
            };

            _mongoClient.GetDatabase(dbName).GetCollection<BsonDocument>(Table).InsertOne(bDocument);
        }
    }
}
