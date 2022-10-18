using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Watchlist.Data.Common;
using Watchlist.Models;
using Watchlist.Services;

namespace Watchlist.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private IMovieService service;

        public MoviesController(IMovieService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await service.AllMoviesAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddMovieViewModel()
            {
                Genre = await service.GetAllGenreAsync()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddMovieViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await service.AddMovie(model);
            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> AddToCollection(int movieId)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            try
            {
                await service.AddToCollection(movieId, user);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error");
            }
            return RedirectToAction(nameof(All));
        }
        public async Task<IActionResult> Mine()
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var result = await service.WatchedMovies(user);

            return View(result);
        }
        public async Task<IActionResult> RemoveFromCollection(int movieId)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            await service.RemoveFromCollection(movieId, user);
            return RedirectToAction(nameof(Mine));
        }
    }
}
