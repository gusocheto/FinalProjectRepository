using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Data.Models
{
    public class OrderProducts
    {
        [Key]
        public Guid OrderProductsId { get; set; } = Guid.NewGuid();

        [Required]
        [Comment("The ID of the order")]
        public Guid OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; } = null!;

        [Required]
        [Comment("The ID of the product")]
        public Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

        //[Required]
        //[Comment("The quantity of the product in the order")]
        //public int Quantity { get; set; }
    }
}
