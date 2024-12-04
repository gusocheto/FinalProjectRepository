using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Data.Models
{
    public class Cart
    {
        [Key]
        [Comment("The ID of the cart")]
        public Guid CartID { get; set; }

        //[Required]
        //[Comment("The collection of items in the cart")]
        //public virtual ICollection<CartProducts> CartItems { get; set; } = new List<CartProducts>();
    }
}
