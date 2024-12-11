using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Website.Data.Models;
using Website.Services.Data.Interfaces;
using Website.ViewModels.ProductViewModels;

namespace E_commerceSite.Web.Application.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IActionResult> Men()
        {
            var model = await productService.GetAllProductsAsync();

            return View(model);

            //model.Where(x => x.IsAvailable == true)
            //.Where(x => x.ProductName.StartsWith(search) || search == null);
        }

        public async Task<IActionResult> Women()
        {
            var model = await productService.GetAllProductsAsync();
            return View(model);
        }

        public async Task<IActionResult> Kids()
        {
            var model = await productService.GetAllProductsAsync();
            return View(model);
        }

        public async Task<IActionResult> Accessories()
        {
            var model = await productService.GetAllProductsAsync();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            var model = new ProductViewModel
            {
                Categories = GetCategories(),
                ProductTypes = GetProductTypes()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = GetCategories();
                model.ProductTypes = GetProductTypes();
                return View(model);
            }

            await productService.AddProductAsync(model);
            return RedirectToAction(nameof(Men));
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetails(Guid id)
        {
            var model = await productService.GetProductDetailsAsync(id);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProduct(Guid id)
        {
            var product = await productService.GetProductDetailsAsync(id);
            if (product == null)
                return NotFound();

            var model = new ProductViewModel
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ProductDescription = product.Description,
                ProductPrice = product.Price,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryTypeId,
                ProductTypeId = product.ProductTypeId,
                Categories = GetCategories(),
                ProductTypes = GetProductTypes()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProduct(Guid id, ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = GetCategories();
                model.ProductTypes = GetProductTypes();
                return View(model);
            }

            var success = await productService.EditProductAsync(id, model);
            if (!success)
                return NotFound();

            return RedirectToAction(nameof(ProductDetails), new { id });
        }

        [HttpGet]
        [Authorize]
        public IActionResult DeleteProduct(Guid id)
        {
            return View(new DeleteProductViewModel { ProductId = id });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(DeleteProductViewModel model)
        {
            var success = await productService.DeleteProductAsync(model.ProductId);
            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Men));
        }

        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            var model = await productService.GetAllProductsAsync();

            model.Where(x => x.IsAvailable == true)
                .Where(x => x.ProductName.StartsWith(search) || search == null);

            return View(model);
        }
    }
}
