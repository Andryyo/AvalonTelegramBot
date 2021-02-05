using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace Avalon.Web
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        public async Task Post(Update update)
        {

        }
    }
}
