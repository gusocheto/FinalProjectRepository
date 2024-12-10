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
using Website.Services.Data.Interfaces;
using Website.ViewModels.OrderViewModels;
using Website.ViewModels.ProductViewModels;

namespace E_commerceSite.Web.Application.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService homeService;

        public HomeController(ILogger<HomeController> logger, IHomeService homeService)
        {
            _logger = logger;
            this.homeService = homeService;
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

            if (!Guid.TryParse(currentUserId, out Guid userId))
            {
                throw new ArgumentException("Invalid user ID");
            }

            var cartProducts = await homeService.GetCartProductsAsync(userId);
            return View(cartProducts);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid id)
        {
            string currentUserId = GetCurrentUserId() ?? string.Empty;

            if (!Guid.TryParse(currentUserId, out Guid userId))
            {
                throw new ArgumentException("Invalid user ID");
            }

            bool success = await homeService.AddToCartAsync(userId, id);
            if (!success)
                throw new ArgumentException("Unable to add product to cart");

            return RedirectToAction(nameof(Cart));
        }

        public async Task<IActionResult> RemoveFromCart(Guid id)
        {
            string currentUserId = GetCurrentUserId() ?? string.Empty;

            if (!Guid.TryParse(currentUserId, out Guid userId))
            {
                throw new ArgumentException("Invalid user ID");
            }

            bool success = await homeService.RemoveFromCartAsync(userId, id);
            if (!success)
                throw new ArgumentException("Unable to remove product from cart");

            return RedirectToAction(nameof(Cart));
        }
    }
}
