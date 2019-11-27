using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Logic
{
    public class MongoDB
    {
        IMongoClient _mongoCliente;

        public MongoDB(IMongoClient mongoClient)
        {
            this._mongoCliente = mongoClient;
        }


    }
}
