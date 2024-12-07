using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.ViewModels.ProductViewModels;

namespace Website.ViewModels.OrderViewModels
{
    public class UserOrderDetailsViewModel
    {
        public Guid OrderId { get; set; }
        public Guid OrderDetailsId { get; set; }

        public string ShippingAddress { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string ZipCode { get; set; } = null!;

        public decimal AmountPaid { get; set; }

        public IEnumerable<ProductCartViewModel> productCartViewModels { get; set; }
            = new List<ProductCartViewModel>();
    }
}
