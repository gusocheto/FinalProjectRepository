﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Data.Models
{
    using static Website.Common.EntityValidationConstants.Product;
    public class Product
    {
        [Key]
        [Comment("The ID of the product")]
        public Guid ProductId { get; set; } = Guid.NewGuid();

        [Required]
        [Comment("The name of the product")]
        [MinLength(ProductNameMinLength)]
        [MaxLength(ProductNameMaxLength)]
        public string ProductName { get; set; } = null!;

        [Required]
        [Comment("The price of the product")]
        public decimal ProductPrice { get; set; }

        [Comment("The product description")]
        [MinLength(ProductDetailsMinLength)]
        [MaxLength(ProductDetailsMaxLength)]
        public string? ProductDescription { get; set; }

        [Comment("The URL of the product image")]
        public string? ImageUrl { get; set; }

        [Comment("The stock quantity of the product")]
        public int StockQuantity { get; set; }

        [Required]
        [Comment("The type ID of the product")]
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

        [Comment("Indicates if the product is available")]
        public bool IsAvailable { get; set; }

        public int TimesOrdered { get; set; }

        public ICollection<CartProducts> CartProducts { get; set; } =
             new List<CartProducts>();
    }
}
