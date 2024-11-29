using E_commerceSite.Web.Application.Data;
using E_commerceSite.Web.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Globalization;
using Website.Data.Models;
using Website.Data.Models.Enums;
using Website.ViewModels.ProductViewModels;

namespace E_commerceSite.Web.Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
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
            var model = context.Products
                .Select(p => new ProductPageViewModel()
                {
                    Id = p.ProductId,
                    ProductName = p.ProductName,
                    ProductImageUrl = p.ImageUrl,
                    ProductPrice = p.ProductPrice
                })
                .AsNoTracking()
                .ToList();

            return View(model);

        }

        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // If the model is invalid, reload dropdown data and return the view.
                model.Categories = Enum.GetValues(typeof(CategoryEnumaration))
                                       .Cast<CategoryEnumaration>()
                                       .Select(e => new Category
                                       {
                                           CategoryId = (int)e,
                                           CategoryType = e
                                       }).ToList();

                model.ProductTypes = Enum.GetValues(typeof(ProductCategorizationEnumaration))
                                          .Cast<ProductCategorizationEnumaration>()
                                          .Select(e => new ProductType
                                          {
                                              ProductTypeId = (int)e,
                                              ProductTypeName = e
                                          }).ToList();

                return View(model);
            }

            // Map the ProductViewModel to Product entity
            Product product = new Product
            {
                ProductName = model.ProductName,
                ProductPrice = model.ProductPrice,
                ProductDescription = model.ProductDescription,
                ImageUrl = model.ImageUrl,
                StockQuantity = model.ProductQuantity,
                CategoryTypeId = model.CategoryId / 10,
                ProductTypeId = model.ProductTypeId,
            };

            // Add the product to the database
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Men)); // Redirect to the product list or home page
        }
    }
}
