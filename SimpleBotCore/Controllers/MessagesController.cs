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
        private MessageRepository _messageRepository;

        public MessagesController(SimpleBotUser bot, MessageRepository mongoConn)
        {
            this._bot = bot;
            this._messageRepository = mongoConn;
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
            }

            // HTTP 202
            return Accepted();
        }

        // Estabelece comunicacao entre o usuario e o SimpleBotUser
        async Task HandleActivityAsync(Activity activity)
        {
            var message = new SimpleMessage(activity.From.Id, activity.From.Name, activity.Text);
            SalvarMensagemBase(message);
            string response = _bot.RespostaComQtde(message, ProcurarMensagens(message));

            await ReplyUserAsync(activity, response);
        }

        // Responde mensagens usando o Bot Framework Connector
        async Task ReplyUserAsync(Activity message, string text)
        {
            var connector = new ConnectorClient(new Uri(message.ServiceUrl));

            var reply = message.CreateReply(text);
            await connector.Conversations.ReplyToActivityAsync(reply);
        }

        private void SalvarMensagemBase(SimpleMessage message)
        {
            _messageRepository.InserirBase(message);
        }

        private int ProcurarMensagens(SimpleMessage message)
        {
            return _messageRepository.ObterQuantidadeDeMensagemPorUser(message);
        }
    }
}
