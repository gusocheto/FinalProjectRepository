using E_commerceSite.Web.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Website.Data.Models;
using Website.Data.Models.Enums;
using Website.ViewModels.ProductViewModels;

namespace E_commerceSite.Web.Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Men()
        {
            return View();
        }
        public IActionResult AddProduct()
        {
            // Assuming you have an enum for categories
            var categories = Enum.GetValues(typeof(CategoryEnumaration))
                                 .Cast<CategoryEnumaration>()
                                 .Select(e => new Category
                                 {
                                     CategoryId = (int)e,
                                     CategoryType = e
                                 }).ToList();

            var productTypes = Enum.GetValues(typeof(ProductCategorizationEnumaration))
                           .Cast<ProductCategorizationEnumaration>()
                           .Select(e => new ProductType
                           {
                               ProductTypeId = (int)e,
                               ProductTypeName = e
                           }).ToList();

            var model = new ProductViewModel
            {
                Categories = categories, // Populating the categories
                ProductTypes = productTypes
            };

            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
