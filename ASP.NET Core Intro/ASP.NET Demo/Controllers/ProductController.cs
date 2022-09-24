using ASP.NET_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ASP.NET_Demo.Controllers
{
    public class ProductController : Controller
    {
        private IEnumerable<ProductViewModel> products =
            new List<ProductViewModel>()
            {
                new ProductViewModel()
                {
                    Id = 1,
                    Name = "Cheese",
                    Price = 7.00
                },
                new ProductViewModel()
                {
                    Id = 2,
                    Name = "Ham",
                    Price = 5.50
                },
                 new ProductViewModel()
                {
                    Id = 3,
                    Name = "Bread",
                    Price = 1.50
                }

            };
        public IActionResult Index()
        {
            return View();
        }
        [ActionName("My-Products")]
        public IActionResult All(string keyword)
        {
            if (keyword != null)
            {
                var prod = products
                    .Where(x => x.Name.ToLower().Contains(keyword.ToLower()));
                return View(prod);
            }
            return View(products);
        }
        public IActionResult ById(int id = 1)
        {
            var product = products
                .FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return BadRequest();
            }
            return View(product);
        }
        public IActionResult AllAsJson()
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            return Json(products, options);
        }
        public IActionResult AllAsText()
        {
            var text = string.Empty;
            foreach (var item in products)
            {
                text += $"Product {item.Id}: {item.Name} - {item.Price}lv";
                text += "\r\n";
            }
            return Content(text);
        }

    }
}
