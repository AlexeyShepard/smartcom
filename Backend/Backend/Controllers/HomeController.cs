using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    public class HomeController : Controller
    {

        public string Index()
        {
            return "Всё у тебя работает, чувак!";
        }
    }
}
