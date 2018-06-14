using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LexWebChatBox.Models;
using LexWebChatBox.DataService;
using Microsoft.AspNetCore.Http;

namespace LexWebChatBox.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAWSLexService awsLexSvc;
        private ISession userHttpSession;
        private Dictionary<string, string> lexSessionData;
        //private List<ChatBotMessage> botMessages;
        private string botMsgKey = "ChatBotMessages",
                       botAtrribsKey = "LexSessionData",
                       userSessionID = String.Empty;


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
