using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    //Контроллер с раширенным функционалом
    [ApiController]
    public class ExtendController : ControllerBase
    {
        protected JsonResult JsonResponse(object Content)
        {
            return new JsonResult(JsonSerializer.Serialize(Content));
        }
    }
}
