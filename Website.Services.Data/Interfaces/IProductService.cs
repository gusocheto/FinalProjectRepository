using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.ViewModels.ProductViewModels;

namespace Website.Services.Data.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductPageViewModel>> GetAllProductsAsync();
        Task<bool> AddProductAsync(ProductViewModel model);
        Task<ProductDescriptionViewModel?> GetProductDetailsAsync(Guid productId);
        Task<bool> EditProductAsync(Guid productId, ProductViewModel model);
        Task<bool> DeleteProductAsync(Guid productId);
        Task<PaginatedList<ProductPageViewModel>> GetPagedAndSearchedProductsAsync(string searchQuery, int pageIndex, int pageSize);
    }
}
