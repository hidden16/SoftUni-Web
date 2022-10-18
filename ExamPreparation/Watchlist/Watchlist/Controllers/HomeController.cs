using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Watchlist.Data.Entities;

namespace Watchlist.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<User> signInManager;
        public HomeController(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {
            if (signInManager.IsSignedIn(User))
            {
                return RedirectToAction("All", "Movies");
            }
            return View();
        }
    }
}