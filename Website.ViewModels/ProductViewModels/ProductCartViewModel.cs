using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.ViewModels.ProductViewModels
{
    public class ProductCartViewModel
    {
        public Guid Id { get; set; }

        public string? ImageUrl { get; set; }

        public required string ProductName { get; set; }

        public decimal Price { get; set; }
    }
}
