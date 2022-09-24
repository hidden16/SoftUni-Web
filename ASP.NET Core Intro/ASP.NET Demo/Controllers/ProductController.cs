using ASP.NET_Demo.Models;
using Microsoft.AspNetCore.Mvc;

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
         public IActionResult All()
        {
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

    }
}
