using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.ViewModels.ProductViewModels;

namespace Website.Services.Data.Interfaces
{
    public interface IHomeService
    {
        Task<IEnumerable<ProductCartViewModel>> GetCartProductsAsync(Guid userId);
        Task<bool> AddToCartAsync(Guid userId, Guid productId);
        Task<bool> RemoveFromCartAsync(Guid userId, Guid productId);
    }
}
