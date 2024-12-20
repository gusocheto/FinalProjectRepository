﻿using System;
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
        public Guid Id { get; set; }
        public string ProductName { get; set; } = null!;

        public decimal ProductPrice { get; set; }

        public string? ProductDescription { get; set; }

        public string? ImageUrl { get; set; }

        public int ProductQuantity { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public List<Category> Categories { get; set; } = new List<Category>();

        [Required]
        public int ProductTypeId { get; set; }

        public List<ProductType> ProductTypes { get; set; } = new List<ProductType>();

    }
}
