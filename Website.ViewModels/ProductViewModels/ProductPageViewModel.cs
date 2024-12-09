using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.ViewModels.ProductViewModels
{
    public class ProductPageViewModel
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = null!;

        public string ProductImageUrl { get; set; } = null!;

        public decimal ProductPrice { get; set; }

        public string ProductType { get; set; } = null!;

        public bool IsAvailable { get; set; }
    }
}
