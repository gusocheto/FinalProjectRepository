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
    public class Order
    {
        [Key]
        [Comment("Id of the order")]
        public Guid OrderId { get; set; }

        [Required]
        [Comment("The id of the user who made the order")]
        public Guid UserId { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public CustomerUser User { get; set; } = null!;

        [Required]
        [Comment("The date on which the order was created")]
        public DateTime DateOnOrderCreation { get; set; }

        [Required]
        [Comment("The ID of the order details")]
        public int OrderDetailsId { get; set; }

        [Required]
        [ForeignKey(nameof(OrderDetailsId))]
        public OrderDetails OrderDetails { get; set; } = null!;

        [Required]
        [Comment("The ID of the status for the order")]
        public int StatusId { get; set; }

        [Required]
        [ForeignKey(nameof(StatusId))]
        public virtual Status Status { get; set; } = null!;
    }
}
