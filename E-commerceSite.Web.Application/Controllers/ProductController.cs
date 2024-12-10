using E_commerceSite.Web.Application.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website.Data.Models;
using Website.ViewModels.ProductViewModels;

namespace E_commerceSite.Web.Application.Controllers
{
    public class ProductController : BaseController
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ApplicationDbContext context;
        private readonly RoleManager<ApplicationUser> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
       

        public ProductController(ILogger<ProductController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Men()
        {
            var model = GetAllProducts();

            return View(model);
        }
        public IActionResult Women()
        {
            var model = GetAllProducts();

            return View(model);
        }
        public IActionResult Kids()
        {
            var model = GetAllProducts();

            return View(model);
        }
        public IActionResult Accessories()
        {
            var model = GetAllProducts();

            return View(model);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            var categories = GetCategories();

            var productTypes = GetProductTypes();

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
                model.Categories = GetCategories();

                model.ProductTypes = GetProductTypes();

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
                IsAvailable = true,
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
                .AsNoTracking()
                .Select(p => new ProductDescriptionViewModel()
                {
                    Id = p.ProductId,
                    ImageUrl = p.ImageUrl,
                    ProductName = p.ProductName,
                    Description = p.ProductDescription ?? string.Empty,
                    Price = p.ProductPrice,
                    CategoryName = p.Category.CategoryType.ToString(),
                    Quantity = p.StockQuantity,
                    IsAvailable = p.IsAvailable,
                })
                .FirstOrDefaultAsync();

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProduct(Guid id)
        {
            string? currentUserId = GetCurrentUserId();

            var model = await context.Products
                .Where(p => p.ProductId == id)
                .Where(p => p.IsAvailable == true)
                .AsNoTracking()
                .Select(p => new ProductViewModel()
                {
                    ImageUrl = p.ImageUrl,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    ProductPrice = p.ProductPrice,
                    CategoryId = p.CategoryTypeId,
                    ProductTypeId = p.ProductTypeId,
                })
                .FirstOrDefaultAsync();

            model.Categories = GetCategories();
            model.ProductTypes = GetProductTypes();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProduct(ProductViewModel model, Guid id)
        {
            if (ModelState.IsValid == false)
            {
                model.Categories = GetCategories();
                model.ProductTypes = GetProductTypes();

                return View(model);
            }


            Product? entity = await context.Products.FindAsync(id);

            if (entity == null || !entity.IsAvailable)
            {
                throw new ArgumentException("Invalid id");
            }

            string? currentUserId = GetCurrentUserId();

            entity.ImageUrl = model.ImageUrl;
            entity.ProductName = model.ProductName;
            entity.ProductDescription = model.ProductDescription;
            entity.ProductPrice = model.ProductPrice;
            entity.CategoryTypeId = model.CategoryId / 10;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(ProductDetails), new { model.Id });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var model = await context.Products
                .Where(p => p.ProductId == id)
                .Where(p => p.IsAvailable == true)
                .AsNoTracking()
                .Select(p => new DeleteProductViewModel()
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    AdminId = GetCurrentUserId() ?? string.Empty,
                    AdminName = User.Identity.Name,
                })
                .FirstOrDefaultAsync();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(DeleteProductViewModel model)
        {
            Product? product = await context.Products
                .Where(p => p.ProductId == model.ProductId)
                .Where(p => p.IsAvailable == true)
                .FirstOrDefaultAsync();

            if (product != null)
            {
                product.IsAvailable = false;

                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(HomeController.Index));
        }

        private  List<ProductPageViewModel> GetAllProducts()
        {
            var model = context.Products
               .Select(p => new ProductPageViewModel()
               {
                   Id = p.ProductId,
                   ProductName = p.ProductName,
                   ProductImageUrl = p.ImageUrl ?? string.Empty,
                   ProductPrice = p.ProductPrice,
                   ProductType = p.ProductType.ProductTypeName.ToString(),
                   IsAvailable = p.IsAvailable,
               })
               .AsNoTracking()
               .ToList();

            return model;
        }
    }
}
