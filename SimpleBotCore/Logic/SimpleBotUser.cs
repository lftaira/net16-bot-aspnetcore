using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBotCore.Logic
{
    public class SimpleBotUser
    {
        public string Reply(SimpleMessage message)
        {
            return $"{message.User} disse '{message.Text}'" ;
        }

        public string RespostaComQtde(SimpleMessage message, int qtde)
        {
            return $"{message.User} disse '{message.Text}' ({qtde} mensagens enviadas)";
        }
    }
}