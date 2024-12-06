using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.ViewModels.ProductViewModels;

namespace Website.ViewModels.OrderViewModels
{
    public class OrderFormViewModel
    {
        public Guid OrderDetailsId { get; set; }

        public Guid ClientUser { get; set; }

        public string Name { get; set; } = null!;

        public string ShippingAddress { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string ZipCode { get; set; } = null!;

        public decimal AmountPaid { get; set; }
        public int StatusId { get; set; }

        public IEnumerable<ProductCartViewModel> productCartViewModels { get; set; } 
            = new List<ProductCartViewModel>();
    }
}
