using Avalon.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Avalon.Web.Services
{
    public class CallbackQueryHandler : ICallbackQueryHandler
    {
        private readonly ITelegramBotClient client;

        public CallbackQueryHandler(
            ITelegramBotClient client)
        {
            this.client = client;
        }

        public event EventHandler<CallbackQueryEventArgs> CallbackQuery;

        public async Task Handle(CallbackQuery callbackQuery)
        {
            OnCallbackQuery(callbackQuery);
            await client.AnswerCallbackQueryAsync(callbackQuery.Id);
        }

        public void OnCallbackQuery(CallbackQuery query)
        {
            var handler = CallbackQuery;
            handler?.Invoke(this, new CallbackQueryEventArgs(query.Message.MessageId, query.Data));
        }
    }
}
