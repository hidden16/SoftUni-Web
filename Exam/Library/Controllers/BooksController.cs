using Library.Contracts;
using Library.Models.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IBookService service;
        public BooksController(IBookService service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var entities = await service.AllBooksAsync();

            return View(entities);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddBookViewModel()
            {
                Categories = await service.GetAllCategoriesAsync()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel model)
        {
            await service.AddBookAsync(model);
            return RedirectToAction(nameof(All));
        }
        public async Task<IActionResult> Mine()
        {
            var userId = GetUserId();
            var books = await service.ReadBooksAsync(userId);
            return View(books);
        }
        public async Task<IActionResult> AddToCollection(int bookId)
        {
            var userId = GetUserId();
            await service.AddToCollection(bookId, userId);
            return RedirectToAction(nameof(All));
        }
        public async Task<IActionResult> RemoveFromCollection(int bookId)
        {
            var userId = GetUserId();
            await service.RemoveFromCollection(bookId, userId);
            return RedirectToAction(nameof(Mine));
        }

        private string GetUserId()
        {
            return User?.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
