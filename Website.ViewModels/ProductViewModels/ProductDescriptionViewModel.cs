using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.ViewModels.ProductViewModels
{
    public class ProductDescriptionViewModel
    {
        public Guid Id { get; set; }

        public string? ImageUrl { get; set; }

        public required string ProductName { get; set; }

        public decimal Price { get; set; }

        public required string Description { get; set; }

        public required string CategoryName { get; set; }

        public required bool IsAvailable { get; set; }

        public required int Quantity { get; set; }

    }
}
