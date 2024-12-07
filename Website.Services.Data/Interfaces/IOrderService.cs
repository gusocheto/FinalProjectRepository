using Website.ViewModels.Admin.OrderManagmentViewModels;

namespace Website.Services.Data.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<AllOrdersViewModel>> GetAllOrdersAsync();

        Task<bool> OrderExistsByIdAsync(Guid userId);

        Task<bool> AssignOrderToStatusAsync(Guid userId, string roleName);

        Task<bool> RemoveOrderAsync(Guid userId);
    }
}
