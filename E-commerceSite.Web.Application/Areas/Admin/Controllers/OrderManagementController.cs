using Microsoft.AspNetCore.Mvc;
using Website.Services.Data.Interfaces;
using Website.ViewModels.Admin.OrderManagmentViewModels;
using Website.ViewModels.Admin.UserManagementViewModel;

namespace E_commerceSite.Web.Application.Areas.Admin.Controllers
{
    public class OrderManagementController : Controller
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
    }
}
