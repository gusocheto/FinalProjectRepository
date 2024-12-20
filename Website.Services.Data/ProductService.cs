﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Website.Data.Models;
using Website.Data.Repository.Interfaces;
using Website.Services.Data.Interfaces;
using Website.ViewModels.ProductViewModels;

namespace Website.Services.Data
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IRepository<Product, Guid> productRepository;

        public ProductService(IRepository<Product, Guid> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductPageViewModel>> GetAllProductsAsync()
        {
            return await productRepository
                .GetAllAttached()
                .Where(p => p.IsAvailable)
                .Select(p => new ProductPageViewModel
                {
                    Id = p.ProductId,
                    ProductName = p.ProductName,
                    ProductImageUrl = p.ImageUrl ?? string.Empty,
                    ProductPrice = p.ProductPrice,
                    ProductType = p.ProductType.ProductTypeName.ToString(),
                    IsAvailable = p.IsAvailable,
                })
                .ToListAsync();
        }

        public async Task<ProductDescriptionViewModel?> GetProductDetailsAsync(Guid productId)
        {
            return await productRepository
                .GetAllAttached()
                .Where(p => p.ProductId == productId && p.IsAvailable)
                .Select(p => new ProductDescriptionViewModel
                {
                    Id = p.ProductId,
                    ImageUrl = p.ImageUrl,
                    ProductName = p.ProductName,
                    Description = p.ProductDescription ?? string.Empty,
                    Price = p.ProductPrice,
                    CategoryName = p.Category.CategoryType.ToString(),
                    Quantity = p.StockQuantity,
                    IsAvailable = p.IsAvailable,
                    ProductTypeId = p.ProductTypeId,
                    CategoryTypeId = p.CategoryTypeId,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> AddProductAsync(ProductViewModel model)
        {
            var product = new Product
            {
                ProductName = model.ProductName,
                ProductPrice = model.ProductPrice,
                ProductDescription = model.ProductDescription,
                ImageUrl = model.ImageUrl,
                StockQuantity = model.ProductQuantity,
                CategoryTypeId = model.CategoryId / 10,
                ProductTypeId = model.ProductTypeId,
                IsAvailable = true,
            };

            await productRepository.AddAsync(product);
            return true;
        }

        public async Task<bool> EditProductAsync(Guid productId, ProductViewModel model)
        {
            var product = await productRepository.GetByIdAsync(productId);
            if (product == null || !product.IsAvailable)
                return false;

            product.ProductName = model.ProductName;
            product.ProductDescription = model.ProductDescription;
            product.ProductPrice = model.ProductPrice;
            product.ImageUrl = model.ImageUrl;
            product.CategoryTypeId = model.CategoryId / 10;
            product.ProductTypeId = model.ProductTypeId;

            await productRepository.UpdateAsync(product);
            return true;
        }

        public async Task<bool> DeleteProductAsync(Guid productId)
        {
            var product = await productRepository.GetByIdAsync(productId);
            if (product == null || !product.IsAvailable)
                return false;

            product.IsAvailable = false;
            await productRepository.UpdateAsync(product);
            return true;
        }

        public async Task<PaginatedList<ProductPageViewModel>> GetPagedAndSearchedProductsAsync(
                string? searchQuery, int pageIndex, int pageSize)
        {
            var query = productRepository
                .GetAllAttached()
                .Where(p => p.IsAvailable);

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.ToLower();
                query = query.Where(p => p.ProductName.ToLower().Contains(searchQuery));
            }

            var result = query.Select(p => new ProductPageViewModel
            {
                Id = p.ProductId,
                ProductName = p.ProductName,
                ProductImageUrl = p.ImageUrl ?? string.Empty,
                ProductPrice = p.ProductPrice,
                ProductType = p.ProductType.ProductTypeName.ToString(),
                IsAvailable = p.IsAvailable,
            });

            return await PaginatedList<ProductPageViewModel>.CreateAsync(result, pageIndex, pageSize);
        }

        public async Task<IEnumerable<ProductPageViewModel>> GetTopBestSellersAsync(int count = 3)
        {
            return await productRepository
                .GetAllAttached()
                .OrderBy(p => p.TimesOrdered)
                .Take(count)
                .Select(p => new ProductPageViewModel
                {
                    Id = p.ProductId,
                    ProductName = p.ProductName,
                    ProductImageUrl = p.ImageUrl ?? string.Empty,
                    ProductPrice = p.ProductPrice,
                    ProductType = p.ProductType.ProductTypeName.ToString(),
                    IsAvailable = p.IsAvailable,
                })
                .ToListAsync();
        }
    }

    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }

    }
}
