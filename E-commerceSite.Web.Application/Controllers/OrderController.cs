using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Data.Models.Enums;
using Website.Data.Models;
using Website.ViewModels.OrderViewModels;
using Website.ViewModels.ProductViewModels;
using E_commerceSite.Web.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace E_commerceSite.Web.Application.Controllers
{
    public class OrderController : BaseController
    {
        private readonly ILogger<OrderController> _logger;
        private readonly ApplicationDbContext context;

        public OrderController(ILogger<OrderController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult SuccsesfullOrder()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Order()
        {
            string currentUserId = GetCurrentUserId() ?? string.Empty;

            var model = await context.Products
                .Where(p => p.IsAvailable == true)
                .Where(p => p.CartProducts.Any(pc => pc.ApplicationUserId.ToString() == currentUserId))
                .Select(p => new ProductCartViewModel()
                {
                    Id = p.ProductId,
                    ImageUrl = p.ImageUrl,
                    ProductName = p.ProductName,
                    Price = p.ProductPrice,
                })
                .AsNoTracking()
                .ToListAsync();

            OrderFormViewModel orderModel = new OrderFormViewModel();
            orderModel.productCartViewModels = model;

            orderModel.AmountPaid = model.Sum(p => p.Price);

            return View(orderModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Order(OrderFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;

            if (!Guid.TryParse(currentUserId, out Guid currGuid))
            {
                throw new ArgumentException("Invalid user ID");
            }

            var cartProducts = await context.CartsProducts
                .Where(cp => cp.ApplicationUserId == currGuid)
                .Include(cp => cp.Product)
                .ToListAsync();

            Order order = new Order
            {
                DateOnOrderCreation = DateTime.Now,
                OrderDetails = new OrderDetails
                {
                    ShippingAddress = model.ShippingAddress,
                    City = model.City,
                    Country = model.Country,
                    ZipCode = model.ZipCode,
                    AmountPaid = cartProducts.Sum(x => x.Product.ProductPrice)
                },
                StatusId = (int)StatusEnumaration.Pending / 2,
                OrderCartProducts = cartProducts.Select(cp => new OrderProducts
                {
                    ProductId = cp.ProductId,
                }).ToList()
            };

            order.OrderUsers.Add(new OrderUser
            {
                ApplicationUserId = currGuid,
                OrderId = order.OrderId,
            });

            foreach (var product in cartProducts)
            {
                product.Product.StockQuantity--;
                product.Product.TimesOrdered++;
                if(product.Product.StockQuantity <= 0)
                {
                    product.Product.StockQuantity = 0;
                    product.Product.IsAvailable = false;
                }
            }

            ApplicationUser? currAppUser = await context.ApplicationUsers
                .Include(u => u.ProductCarts)
                .FirstOrDefaultAsync(x => x.Id == currGuid);

            if (currAppUser == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            currAppUser.ProductCarts.Clear();

            var cartProductsToRemove = await context.CartsProducts
                .Where(cp => cp.ApplicationUserId == currGuid)
                .ToListAsync();


            context.CartsProducts.RemoveRange(cartProductsToRemove);

            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(SuccsesfullOrder));
        }


        [HttpGet]
        public async Task<IActionResult> SeeOrders()
        {
            string currentUserId = GetCurrentUserId() ?? string.Empty;

            if (!Guid.TryParse(currentUserId, out Guid currGuid))
            {
                throw new ArgumentException("Invalid user ID");
            }

            var statuts = GetStatusTypes();

            var model = await context.Orders
                .Where(cp => cp.OrderUsers.Any(x => x.ApplicationUserId == currGuid))
                .Select(cp => new UserMadeOrderViewModel
                {
                    OrderId = cp.OrderId,
                    DateOnOrderMade = cp.DateOnOrderCreation,
                    ApproximetlyArrivalDate = cp.DateOnOrderCreation.AddDays(7),
                    CountOfItems = cp.OrderCartProducts.Count,
                    StatusType = cp.Status.StatusType.ToString() ?? string.Empty,
                })
                .AsNoTracking()
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetails(Guid orderId)
        {

            var currentOrder = await context.Orders
                       .Where(o => o.OrderId == orderId)
                       .Include(o => o.OrderDetails)
                       .Include(o => o.OrderCartProducts)
                           .ThenInclude(op => op.Product)
                       .FirstOrDefaultAsync();

            if (currentOrder == null)
            {
                return NotFound();
            }

            var model = new UserOrderDetailsViewModel
            {
                OrderDetailsId = currentOrder.OrderDetails.OrderDetailsID,
                ShippingAddress = currentOrder.OrderDetails.ShippingAddress,
                City = currentOrder.OrderDetails.City,
                Country = currentOrder.OrderDetails.Country,
                ZipCode = currentOrder.OrderDetails.ZipCode,
                AmountPaid = currentOrder.OrderDetails.AmountPaid,
                productCartViewModels = currentOrder.OrderCartProducts.Select(op => new ProductCartViewModel
                {
                    Id = op.ProductId,
                    ProductName = op.Product.ProductName,
                    Price = op.Product.ProductPrice,
                    ImageUrl = op.Product.ImageUrl
                }).ToList()
            };

            return View(model);
        }
    }
}
