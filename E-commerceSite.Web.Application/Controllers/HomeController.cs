using E_commerceSite.Web.Application.Data;
using E_commerceSite.Web.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Globalization;
using System.Security.Claims;
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
                Categories = categories,
                ProductTypes = productTypes
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
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

            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Men));
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetails(Guid id)
        {
            var model = await context.Products
                .Where(p => p.ProductId == id)
                .Where(p => p.IsAvailable == false)
                .AsNoTracking()
                .Select(p => new ProductDescriptionViewModel()
                {
                    Id = p.ProductId,
                    ImageUrl = p.ImageUrl,
                    ProductName = p.ProductName,
                    Description = p.ProductDescription,
                    Price = p.ProductPrice,
                    CategoryName = p.Category.CategoryType.ToString(),
                    Quantity = p.StockQuantity,
                    IsAvailable = p.IsAvailable,
                })
                .FirstOrDefaultAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Cart()
        {
            string currentUserId = GetCurrentUserId() ?? string.Empty;

            var model = await context.Products
                .Where(p => p.IsAvailable == true)
                .Where(p => p.CartProducts.Any(pc => pc.ProductId.ToString() == currentUserId))
                .Select(p => new ProductCartViewModel()
                {
                    Id = p.ProductId,
                    ImageUrl = p.ImageUrl,
                    ProductName = p.ProductName,
                    Price = p.ProductPrice,
                })
                .AsNoTracking()
                .ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid id)
        {
            Product? entity = await context.Products
                .Where(p => p.ProductId == id)
                .Include(p => p.CartProducts)
                .FirstOrDefaultAsync();

            if (entity == null || entity.IsAvailable == true)
            {
                throw new ArgumentException("Invalid id");
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;

            if (entity.CartProducts.Any(p => p.ProductId.ToString() == currentUserId))
            {
                return RedirectToAction(nameof(Index));
            }

            Guid currGuid;

            if (!Guid.TryParse(currentUserId, out currGuid))
            {
                // Handle invalid GUID, e.g., return an error or throw an exception
                throw new ArgumentException("Invalid user ID");
            }

            //if(!context.Carts.Any(x => x.CartID == currGuid))
            //{
            //    Cart newCart = new Cart()
            //    {
            //        CartID = currGuid,
            //        CartItems = null
            //    };
            //    await context.AddAsync(newCart);
            //}

            entity.CartProducts.Add(new CartProducts()
            {
                CartId = currGuid,
                ProductId = entity.ProductId,
            });

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Cart));
        }


        private string? GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
