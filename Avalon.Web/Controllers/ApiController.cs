using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalon.Web.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types;

namespace Avalon.Web
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IEnumerable<ICommandHandler> commandHandlers;

        public ApiController(
            ILogger<ApiController> logger,
            IEnumerable<ICommandHandler> commandHandlers)
        {
            this.logger = logger;
            this.commandHandlers = commandHandlers;
        }

        public async Task Post(Update update)
        {
            logger.LogDebug(JsonConvert.SerializeObject(update));
            switch (update.Type)
            {
                case Telegram.Bot.Types.Enums.UpdateType.Message:
                    await HandleMessage(update.Message);
                    break;  
                case Telegram.Bot.Types.Enums.UpdateType.CallbackQuery:
                    await HandleCallbackQuery(update.CallbackQuery);
                    break;
            }
        }

        private async Task HandleCallbackQuery(CallbackQuery callbackQuery)
        {
        }

        private async Task HandleMessage(Message message)
        {
            var commandsDescriptions = message.Entities?.Where(x => x.Type == Telegram.Bot.Types.Enums.MessageEntityType.BotCommand);

            var commands = commandsDescriptions?.Select(x => message.Text.Substring(x.Offset, x.Length));

            if (commands != null)
            {
                foreach (var command in commands)
                {
                    var handler = commandHandlers.FirstOrDefault(x => x.Command == command);
                    if (handler != null)
                    {
                        await handler.Handle(message);
                    }
                }
            }
        }
    }
}
