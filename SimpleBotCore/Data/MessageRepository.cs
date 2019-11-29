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
    public class MessageRepository : MongoClient
    {
        private IMongoDatabase _database;
        private IMongoCollection<SimpleMessage> _messageCollection;

        public MessageRepository(string connString)
            : base(connString)
        {
            _database = GetDatabase("db_bot");
            _messageCollection = _database.GetCollection<SimpleMessage>("tb_mensagens");
        }

        public void InserirBase(SimpleMessage mensagem)
        {
            _messageCollection.InsertOne(mensagem);
        }

        public int ObterQuantidadeDeMensagemPorUser(SimpleMessage mensagem)
        {
            try
            {
                return _messageCollection.Find(x => x.Id_Usuario == mensagem.Id_Usuario).ToList().Count();
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
