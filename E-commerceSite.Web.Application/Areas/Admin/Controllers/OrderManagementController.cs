using E_commerceSite.Web.Application.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Services.Data.Interfaces;
using Website.ViewModels.Admin.OrderManagmentViewModels;
using static Website.Common.ApplicationConstants;


namespace E_commerceSite.Web.Application.Areas.Admin.Controllers
{
    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public class OrderManagementController : BaseController
    {
        private readonly IOrderService orderService;

        public OrderManagementController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllOrdersViewModel> allUsers = await this.orderService
                .GetAllOrdersAsync();

            return this.View(allUsers);
        }

        [HttpPost]
        public async Task<IActionResult> AssignStatus(Guid orderId, string status)
        {
            if (orderId == Guid.Empty || string.IsNullOrWhiteSpace(status))
            {
                return this.RedirectToAction(nameof(Index));
            }

            bool orderExists = await this.orderService.OrderExistsByIdAsync(orderId);
            if (!orderExists)
            {
                return this.RedirectToAction(nameof(Index));
            }

            bool assignResult = await this.orderService.AssignOrderToStatusAsync(orderId, status);
            if (!assignResult)
            {
                // Optionally: Add error handling logic or TempData message
                return this.RedirectToAction(nameof(Index));
            }

            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveOrder(Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                return this.RedirectToAction(nameof(Index));
            }

            bool orderExists = await this.orderService.OrderExistsByIdAsync(orderId);
            if (!orderExists)
            {
                return this.RedirectToAction(nameof(Index));
            }

            bool removeResult = await this.orderService.RemoveOrderAsync(orderId);
            if (!removeResult)
            {
                // Optionally: Add error handling logic or TempData message
                return this.RedirectToAction(nameof(Index));
            }

            return this.RedirectToAction(nameof(Index));
        }
    }
}
