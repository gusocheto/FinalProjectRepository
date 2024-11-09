using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Data.Models
{
    using static Website.Common.EntityValidationConstants.Product;
    public class Product
    {
        [Key]
        [Comment("The id of the product")]
        public Guid ProductId { get; set; }

        [Required]
        [Comment("The name of the product")]
        [MinLength(ProductNameMinLength)]
        [MaxLength(ProductNameMaxLength)]
        public string ProductName { get; set; } = null!;

        [Required]
        [Comment("The price of the product")]
        public decimal ProductPrice { get; set; }

        [Comment("The product details")]
        [MinLength(ProductDetailsMinLength)]
        [MaxLength(ProductDetailsMaxLength)]
        public string? ProductDescription { get; set; }

        [Comment("The url of the product image")]
        public string? ImageUrl { get; set; }

        [Comment("The quantity of the product")]
        public int StockQuantity { get; set; }

        [Required]
        public int ProductTypeId { get; set; }

        [Required]
        [Comment("The categorization type of the product definition")]
        [ForeignKey(nameof(ProductTypeId))]
        public ProductType ProductType { get; set; } = null!;

        [Required]
        public int CategoryTypeId { get; set; }

        [Required]
        [Comment("The category of the product")]
        [ForeignKey(nameof(CategoryTypeId))]
        public Category Category { get; set; } = null!;

        [Comment("The true/false statement for the product avaliability")]
        private bool IsAvaliable { get; set; }

        // Discount
    }
}
