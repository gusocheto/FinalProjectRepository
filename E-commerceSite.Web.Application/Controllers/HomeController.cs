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
    public class HomeController : BaseController
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

    }
}
