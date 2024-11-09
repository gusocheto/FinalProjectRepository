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

        public virtual ICollection<CartProducts> CartProducts { get; set; }
                = new List<CartProducts>();
    }
}
