using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Data.Models.Enums;
using Website.Data.Models;

namespace Website.ViewModels.ProductViewModels
{
    public class ProductViewModel
    {
        public string ProductName { get; set; } = null!;

        public decimal ProductPrice { get; set; }

        public string? ProductDescription { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public List<Category> Categories { get; set; } = new List<Category>();

    }
}
