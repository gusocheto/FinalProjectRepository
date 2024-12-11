using MockQueryable;
using Moq;
using NUnit.Framework;
using Website.Data.Models;
using Website.Data.Models.Enums;
using Website.Data.Repository.Interfaces;
using Website.Services.Data;
using Website.ViewModels.ProductViewModels;

namespace Website.Services.Tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<IRepository<Product, Guid>> productRepositoryMock;
        private ProductService productService;
        private List<Product> productData;

        [SetUp]
        public void Setup()
        {
            productData = new List<Product>
            {
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Toy",
                    ProductPrice = 100,
                    ImageUrl = "https://example.com/product-a.jpg",
                    IsAvailable = true,
                    ProductType = new ProductType { ProductTypeName = ProductCategorizationEnumaration.Kids },
                    Category = new Category { CategoryType = CategoryEnumaration.Toys },
                    StockQuantity = 50
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "T-Shirt",
                    ProductPrice = 200,
                    ImageUrl = "https://example.com/product-b.jpg",
                    IsAvailable = false,
                    ProductType = new ProductType { ProductTypeName = ProductCategorizationEnumaration.Male },
                    Category = new Category { CategoryType = CategoryEnumaration.TShirts },
                    StockQuantity = 30
                }
            };

            productRepositoryMock = new Mock<IRepository<Product, Guid>>();
            productService = new ProductService(productRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllProductsAsync_ShouldReturnAvailableProducts()
        {
            var productsMock = productData.AsQueryable().BuildMock();
            productRepositoryMock.Setup(repo => repo.GetAllAttached()).Returns(productsMock);

            var result = await productService.GetAllProductsAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().ProductName, Is.EqualTo("Toy"));
        }

        [Test]
        public async Task GetProductDetailsAsync_ShouldReturnCorrectProductDetails()
        {
            var productId = productData.First().ProductId;
            var productsMock = productData.AsQueryable().BuildMock();
            productRepositoryMock.Setup(repo => repo.GetAllAttached()).Returns(productsMock);

            var result = await productService.GetProductDetailsAsync(productId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(productId));
            Assert.That(result.ProductName, Is.EqualTo("Toy"));
        }

        [Test]
        public async Task AddProductAsync_ShouldAddProductSuccessfully()
        {
            var model = new ProductViewModel
            {
                ProductName = "Product C",
                ProductPrice = 150,
                ProductDescription = "New Product",
                ImageUrl = "https://example.com/product-c.jpg",
                ProductQuantity = 40,
                CategoryId = 16,
                ProductTypeId = 1
            };

            productRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);

            var result = await productService.AddProductAsync(model);

            Assert.That(result, Is.True);
            productRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Once);
        }

        [Test]
        public async Task EditProductAsync_ShouldEditExistingProduct()
        {
            var productId = productData.First().ProductId;
            var model = new ProductViewModel
            {
                ProductName = "Updated Toy",
                ProductPrice = 120,
                ProductDescription = "Updated Description",
                ImageUrl = "https://example.com/updated-product-a.jpg",
                CategoryId = 12,
                ProductTypeId = 1
            };

            productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(productData.First);
            productRepositoryMock
                 .Setup(repo => repo.UpdateAsync(It.IsAny<Product>()))
                 .Returns(Task.FromResult(true));


            var result = await productService.EditProductAsync(productId, model);

            Assert.That(result, Is.True);
            productRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Product>()), Times.Once);
        }

        [Test]
        public async Task DeleteProductAsync_ShouldMarkProductAsUnavailable()
        {
            var productId = productData.First().ProductId;

            productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(productData.First);
            productRepositoryMock
                 .Setup(repo => repo.UpdateAsync(It.IsAny<Product>()))
                 .Returns(Task.FromResult(true));


            var result = await productService.DeleteProductAsync(productId);

            Assert.That(result, Is.True);
            productRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Product>()), Times.Once);
        }
    }
}
