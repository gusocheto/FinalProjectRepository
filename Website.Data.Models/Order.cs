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
        [Comment("The date on which the order was made on")]
        public DateTime DateOnOrderCreation { get; set; }

        // OrderDetailsId
        [Required]
        [Comment("The id of the order details")]
        public Guid OrderDetailsId { get; set; }

        [Required]
        [ForeignKey(nameof(OrderDetailsId))]
        public OrderDetails OrderDetails { get; set; } = null!;


        [Required]
        [Comment("The id of the order of the status")]
        public int StatusId { get; set; }

        [Required]
        [ForeignKey(nameof(StatusId))]
        public Status Status { get; set; } = null!;
    }
}
