using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Data.Models
{
    public class Cart
    {
        [Key]
        [Comment("The id of the cart")]
        public int CartID { get; set; }

        [Required]
        [Comment("The id of the current user")]
        public Guid CustomerUserID { get; set; }

        [ForeignKey(nameof(CustomerUserID))]
        public CustomerUser CustomerUser { get; set; } = null!;

        //Products
        public virtual ICollection<Product> Products { get; set; } 
            = new List<Product>();
    }
}
