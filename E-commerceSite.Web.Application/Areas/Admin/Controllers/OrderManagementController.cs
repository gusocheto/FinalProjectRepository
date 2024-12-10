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
    }
}
