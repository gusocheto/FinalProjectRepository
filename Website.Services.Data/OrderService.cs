using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Website.Data.Models;
using Website.Data.Repository.Interfaces;
using Website.Services.Data.Interfaces;
using Website.ViewModels.Admin.OrderManagmentViewModels;


namespace Website.Services.Data
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IRepository<Order, Guid> orderRepository;

        public OrderService(IRepository<Order, Guid> orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public async Task<IEnumerable<AllOrdersViewModel>> GetAllOrdersAsync()
        {
            var allOrders = await this.orderRepository.GetAllAttached()
                .Include(o => o.Status)
                .Include(o => o.OrderUsers)
                    .ThenInclude(ou => ou.ApplicationUser)
                .ToListAsync();

            var allOrdersViewModel = allOrders.Select(order => new AllOrdersViewModel
            {
                OrderId = order.OrderId.ToString(),
                NameOfClient = order.OrderUsers.FirstOrDefault()?.ApplicationUser.UserName ?? "Unknown",
                Statuses = new List<string> { order.Status.StatusType.ToString() }
            });

            return allOrdersViewModel;
        }



        public Task<bool> AssignOrderToStatusAsync(Guid orderId, string statusName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveOrderAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> OrderExistsByIdAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }

    }
}
