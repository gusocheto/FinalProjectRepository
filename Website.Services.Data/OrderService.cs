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
                Statuses = new List<string> { order.Status.StatusType.ToString() }
            });

            return allOrdersViewModel;
        }

        public async Task<bool> AssignOrderToStatusAsync(Guid orderId, string statusName)
        {
            var order = await this.orderRepository.FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
            {
                return false;
            }

            var status = await this.orderRepository.FirstOrDefaultAsync(s => s.Status.ToString() == statusName);

            if (status == null)
            {
                return false;
            }

            order.StatusId = status.StatusId;
            await this.orderRepository.UpdateAsync(order);

            return true;
        }

        public async Task<bool> RemoveOrderAsync(Guid orderId)
        {
            var order = await this.orderRepository.GetByIdAsync(orderId);

            if (order == null)
            {
                return false;
            }

            return await this.orderRepository.DeleteAsync(order);
        }

        public async Task<bool> OrderExistsByIdAsync(Guid orderId)
        {
            return await this.orderRepository.FirstOrDefaultAsync(o => o.OrderId == orderId) != null;
        }

    }
}
