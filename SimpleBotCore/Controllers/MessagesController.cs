using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Schema;
using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBotCore.Data;
using SimpleBotCore.Logic;

namespace SimpleBotCore.Controllers
{
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        private SimpleBotUser _bot = new SimpleBotUser();
        private MongoDataAccess _mongoConn;

        public MessagesController(SimpleBotUser bot, MongoDataAccess mongoConn)
        {
            this._bot = bot;
            this._mongoConn = mongoConn;
        }

        [HttpGet]
        public string Get()
        {
            return "Hello World";
        }

        // POST api/messages
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Activity activity)
        {
            if (activity != null && activity.Type == ActivityTypes.Message)
            {
                await HandleActivityAsync(activity);
                SalvarLogMongo(new SimpleMessage(activity.Id, activity.From.Name, activity.Text));
            }

            // HTTP 202
            return Accepted();
        }

        // Estabelece comunicacao entre o usuario e o SimpleBotUser
        async Task HandleActivityAsync(Activity activity)
        {
            string text = activity.Text;
            string userFromId = activity.From.Id;
            string userFromName = activity.From.Name;

            var message = new SimpleMessage(userFromId, userFromName, text);

            string response = _bot.Reply(message);
            
            await ReplyUserAsync(activity, response);
        }

        // Responde mensagens usando o Bot Framework Connector
        async Task ReplyUserAsync(Activity message, string text)
        {
            var connector = new ConnectorClient(new Uri(message.ServiceUrl));
            var reply = message.CreateReply(text);
            
            await connector.Conversations.ReplyToActivityAsync(reply);
        }
        
        private void SalvarLogMongo(SimpleMessage message)
        {
            _mongoConn.InserirBase("db_bot", "tb_log_mensagens", message);
        }
    }
}
