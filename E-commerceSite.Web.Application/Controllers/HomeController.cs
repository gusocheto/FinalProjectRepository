using E_commerceSite.Web.Application.Data;
using E_commerceSite.Web.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Diagnostics;
using System.Globalization;
using System.Security.Claims;
using Website.Data.Models;
using Website.Data.Models.Enums;
using Website.ViewModels.OrderViewModels;
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

        public IActionResult SuccsesfullOrder()
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
                    ProductImageUrl = p.ImageUrl ?? string.Empty,
                    ProductPrice = p.ProductPrice,
                    IsAvailable = p.IsAvailable,
                })
                .AsNoTracking()
                .ToList();

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

        public async Task<IActionResult> Cart()
        {
            string currentUserId = GetCurrentUserId() ?? string.Empty;

            if (!Guid.TryParse(currentUserId, out Guid currGuid))
            {
                throw new ArgumentException("Invalid user ID");
            }

            var model = await context.CartsProducts
                .Where(cp => cp.ApplicationUserId == currGuid)
                .Select(cp => new ProductCartViewModel
                {
                    Id = cp.Product.ProductId,
                    ImageUrl = cp.Product.ImageUrl,
                    ProductName = cp.Product.ProductName,
                    Price = cp.Product.ProductPrice,
                })
                .AsNoTracking()
                .ToListAsync();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid id)
        {
            Product? entity = await context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (entity == null || !entity.IsAvailable)
            {
                throw new ArgumentException("Invalid product ID");
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;

            if (!Guid.TryParse(currentUserId, out Guid currGuid))
            {
                throw new ArgumentException("Invalid user ID");
            }

            ApplicationUser? currAppUser = await context.ApplicationUsers
                .Include(u => u.ProductCarts)
                .FirstOrDefaultAsync(x => x.Id == currGuid);

            if (currAppUser == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            if (currAppUser.ProductCarts.Any(p => p.ProductId == entity.ProductId))
            {
                return RedirectToAction(nameof(Index));
            }

            currAppUser.ProductCarts.Add(new CartProducts
            {
                ApplicationUserId = currGuid,
                ProductId = entity.ProductId,
            });

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Cart));
        }


        public async Task<IActionResult> RemoveFromCart(Guid id)
        {
            Product? entity = await context.Products
               .Where(p => p.ProductId == id)
               .Include(p => p.CartProducts)
               .FirstOrDefaultAsync();

            if (entity == null || !entity.IsAvailable)
            {
                throw new ArgumentException("Invalid id");
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;
            CartProducts? cartProduct = entity.CartProducts.FirstOrDefault(gr => gr.ApplicationUserId.ToString() == currentUserId);

            if (cartProduct != null)
            {
                entity.CartProducts.Remove(cartProduct);

                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Cart));
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

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Order()
        {
            string currentUserId = GetCurrentUserId() ?? string.Empty;

            var model = await context.Products
                .Where(p => p.IsAvailable == true)
                .Where(p => p.CartProducts.Any(pc => pc.ApplicationUserId.ToString() == currentUserId))
                .Select(p => new ProductCartViewModel()
                {
                    Id = p.ProductId,
                    ImageUrl = p.ImageUrl,
                    ProductName = p.ProductName,
                    Price = p.ProductPrice,
                })
                .AsNoTracking()
                .ToListAsync();

            OrderFormViewModel orderModel = new OrderFormViewModel();
            orderModel.productCartViewModels = model;

            orderModel.AmountPaid = model.Sum(p => p.Price);

            return View(orderModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Order(OrderFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;

            if (!Guid.TryParse(currentUserId, out Guid currGuid))
            {
                throw new ArgumentException("Invalid user ID");
            }

            Order order = new Order
            {
                DateOnOrderCreation = DateTime.Now,
                OrderDetails = new OrderDetails
                {
                    ShippingAddress = model.ShippingAddress,
                    City = model.City,
                    Country = model.Country,
                    ZipCode = model.ZipCode,
                    AmountPaid = model.AmountPaid,
                },
                StatusId = (int)StatusEnumaration.Pending / 2,
            };

            order.OrderUsers.Add(new OrderUser
            {
                ApplicationUserId = currGuid,
                OrderId = order.OrderId,
            });

            ApplicationUser? currAppUser = await context.ApplicationUsers
                .Include(u => u.ProductCarts)
                .FirstOrDefaultAsync(x => x.Id == currGuid);

            if (currAppUser == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            currAppUser.ProductCarts.Clear();

            var cartProductsToRemove = await context.CartsProducts
                .Where(cp => cp.ApplicationUserId == currGuid)
                .ToListAsync();

            order.OrderCartProducts = cartProductsToRemove;

            context.CartsProducts.RemoveRange(cartProductsToRemove);

            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(SuccsesfullOrder));
        }

        public async Task<IActionResult> SeeOrders()
        {
            string currentUserId = GetCurrentUserId() ?? string.Empty;

            if (!Guid.TryParse(currentUserId, out Guid currGuid))
            {
                throw new ArgumentException("Invalid user ID");
            }

            var statuts = GetStatusTypes();

            var model = await context.Orders
                .Where(cp => cp.OrderUsers.Any(x => x.ApplicationUserId == currGuid))
                .Select(cp => new UserMadeOrderViewModel
                {
                    OrderId = cp.OrderId,
                    DateOnOrderMade = cp.DateOnOrderCreation,
                    ApproximetlyArrivalDate = cp.DateOnOrderCreation.AddDays(7),
                    CountOfItems = cp.OrderCartProducts.Count,
                    StatusType = cp.Status.StatusType.ToString() ?? string.Empty,
                })
                .AsNoTracking()
                .ToListAsync();

            return View(model);
        }

        private string? GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private List<Category> GetCategories()
        {
            return Enum.GetValues(typeof(CategoryEnumaration))
                       .Cast<CategoryEnumaration>()
                       .Select(e => new Category
                       {
                           CategoryId = (int)e,
                           CategoryType = e
                       })
                       .ToList();
        }

        private List<ProductType> GetProductTypes()
        {
            return Enum.GetValues(typeof(ProductCategorizationEnumaration))
                       .Cast<ProductCategorizationEnumaration>()
                       .Select(e => new ProductType
                       {
                           ProductTypeId = (int)e,
                           ProductTypeName = e
                       })
                       .ToList();
        }

        private List<Status> GetStatusTypes()
        {
            return Enum.GetValues(typeof(StatusEnumaration))
                       .Cast<StatusEnumaration>()
                       .Select(e => new Status
                       {
                           StatusId = (int)e,
                           StatusType = e
                       })
                       .ToList();
        }

    }
}
