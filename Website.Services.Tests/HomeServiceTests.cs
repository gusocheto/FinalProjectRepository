using MockQueryable;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Website.Data.Models;
using Website.Data.Repository.Interfaces;
using Website.Services.Data;
using Website.ViewModels.ProductViewModels;

namespace Website.Services.Tests
{
    [TestFixture]
    public class HomeServiceTests
    {
        private Mock<IRepository<CartProducts, Guid>> cartProductRepositoryMock;
        private Mock<IRepository<Product, Guid>> productRepositoryMock;
        private HomeService homeService;

        [SetUp]
        public void Setup()
        {
            cartProductRepositoryMock = new Mock<IRepository<CartProducts, Guid>>();
            productRepositoryMock = new Mock<IRepository<Product, Guid>>();
            homeService = new HomeService(cartProductRepositoryMock.Object, productRepositoryMock.Object);
        }

        [Test]
        public async Task GetCartProductsAsync_ShouldReturnCartProductsForUser()
        {
            var userId = Guid.NewGuid();
            var cartProducts = new List<CartProducts>
            {
                new CartProducts
                {
                    ApplicationUserId = userId,
                    Product = new Product
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Product A",
                        ProductPrice = 100,
                        ImageUrl = "https://example.com/product-a.jpg"
                    }
                },
                new CartProducts
                {
                    ApplicationUserId = userId,
                    Product = new Product
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Product B",
                        ProductPrice = 200,
                        ImageUrl = "https://example.com/product-b.jpg"
                    }
                }
            };

            cartProductRepositoryMock
                .Setup(repo => repo.GetAllAttached())
                .Returns(cartProducts.AsQueryable().BuildMock());

            var result = await homeService.GetCartProductsAsync(userId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().ProductName, Is.EqualTo("Product A"));
        }

        [Test]
        public async Task AddToCartAsync_ShouldAddProductToCart_WhenProductIsAvailableAndNotInCart()
        {
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var product = new Product
            {
                ProductId = productId,
                IsAvailable = true
            };

            productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(product);
            cartProductRepositoryMock
                .Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<Expression<Func<CartProducts, bool>>>()))
                .ReturnsAsync((CartProducts)null);

            cartProductRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<CartProducts>()))
                .Returns(Task.CompletedTask);

            var result = await homeService.AddToCartAsync(userId, productId);

            Assert.That(result, Is.True);
            cartProductRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<CartProducts>()), Times.Once);
        }

        [Test]
        public async Task AddToCartAsync_ShouldNotAddProductToCart_WhenProductIsUnavailable()
        {
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var product = new Product
            {
                ProductId = productId,
                IsAvailable = false
            };

            productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(product);

            var result = await homeService.AddToCartAsync(userId, productId);

            Assert.That(result, Is.False);
            cartProductRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<CartProducts>()), Times.Never);
        }

        [Test]
        public async Task AddToCartAsync_ShouldNotAddProductToCart_WhenProductIsAlreadyInCart()
        {
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var cartProduct = new CartProducts
            {
                ApplicationUserId = userId,
                ProductId = productId
            };

            productRepositoryMock
                .Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync(new Product { IsAvailable = true });

            cartProductRepositoryMock
                .Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<Expression<Func<CartProducts, bool>>>()))
                .ReturnsAsync((CartProducts)null);

            var result = await homeService.AddToCartAsync(userId, productId);

            Assert.That(result, Is.False);
            cartProductRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<CartProducts>()), Times.Never);
        }

        [Test]
        public async Task RemoveFromCartAsync_ShouldRemoveProductFromCart_WhenProductExists()
        {
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var cartProduct = new CartProducts
            {
                ApplicationUserId = userId,
                ProductId = productId
            };

            cartProductRepositoryMock
         .Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<Expression<Func<CartProducts, bool>>>()))
         .ReturnsAsync((CartProducts)null);

            cartProductRepositoryMock
                .Setup(repo => repo.DeleteAsync(cartProduct))
                .ReturnsAsync(true);

            var result = await homeService.RemoveFromCartAsync(userId, productId);

            Assert.That(result, Is.True);
            cartProductRepositoryMock.Verify(repo => repo.DeleteAsync(cartProduct), Times.Once);
        }

        [Test]
        public async Task RemoveFromCartAsync_ShouldNotRemoveProductFromCart_WhenProductDoesNotExist()
        {
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            cartProductRepositoryMock
                .Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<Expression<Func<CartProducts, bool>>>()))
                .ReturnsAsync((CartProducts)null);

            var result = await homeService.RemoveFromCartAsync(userId, productId);

            Assert.That(result, Is.False);
            cartProductRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<CartProducts>()), Times.Never);
        }
    }
}
