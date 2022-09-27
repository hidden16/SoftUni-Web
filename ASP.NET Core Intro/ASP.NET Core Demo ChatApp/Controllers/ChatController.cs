using ASP.NET_Core_Demo_ChatApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Demo_ChatApp.Controllers
{
    public class ChatController : Controller
    {
        private static List<KeyValuePair<string, string>> Messages = new List<KeyValuePair<string, string>>();
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Show()
        {
            if (Messages.Count() < 1)
            {
                return View(new ChatViewModel());
            }

            var chatModel = new ChatViewModel()
            {

                Messages = Messages
                .Select(x => new MessageViewModel()
                {
                    Sender = x.Key,
                    MessageText = x.Value
                })
                .ToList()
            };
            return View(chatModel);
        }
        [HttpPost]
        public IActionResult Send(ChatViewModel chat)
        {
            var msg = chat.CurrentMessage;
            Messages.Add(new KeyValuePair<string, string>(msg.Sender, msg.MessageText));
            return RedirectToAction("Show");
        }

    }
}
