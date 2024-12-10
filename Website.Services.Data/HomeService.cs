using E_commerceSite.Web.Application.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Data.Models;
using Website.Data.Repository.Interfaces;
using Website.Services.Data.Interfaces;
using Website.ViewModels.ProductViewModels;

namespace Website.Services.Data
{
    public class HomeService : BaseService, IHomeService
    {
        private readonly IRepository<CartProducts, Guid> cartProductRepository;
        private readonly IRepository<Product, Guid> productRepository;

        public HomeService(
            IRepository<CartProducts, Guid> cartProductRepository,
            IRepository<Product, Guid> productRepository)
        { 
            this.cartProductRepository = cartProductRepository;
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductCartViewModel>> GetCartProductsAsync(Guid userId)
        {
            var cartProducts = await this.cartProductRepository
                .GetAllAttached()
                .Where(cp => cp.ApplicationUserId == userId)
                .Select(cp => new ProductCartViewModel
                {
                    Id = cp.Product.ProductId,
                    ImageUrl = cp.Product.ImageUrl,
                    ProductName = cp.Product.ProductName,
                    Price = cp.Product.ProductPrice,
                })
                .ToListAsync();

            return cartProducts;
        }

        public async Task<bool> AddToCartAsync(Guid userId, Guid productId)
        {
            Product? product = await this.productRepository.GetByIdAsync(productId);
            if (product == null || !product.IsAvailable)
                return false;

            CartProducts? existingCartProduct = await this.cartProductRepository
                .FirstOrDefaultAsync(cp => cp.ProductId == productId && cp.ApplicationUserId == userId);

            if (existingCartProduct != null)
                return false;

            var newCartProduct = new CartProducts
            {
                ApplicationUserId = userId,
                ProductId = productId,
            };

            await this.cartProductRepository.AddAsync(newCartProduct);
            return true;
        }

        public async Task<bool> RemoveFromCartAsync(Guid userId, Guid productId)
        {
            CartProducts? cartProduct = await this.cartProductRepository
                .FirstOrDefaultAsync(cp => cp.ApplicationUserId == userId && cp.ProductId == productId);

            if (cartProduct == null)
                return false;

            return await this.cartProductRepository.DeleteAsync(cartProduct);
        }
    }
}
