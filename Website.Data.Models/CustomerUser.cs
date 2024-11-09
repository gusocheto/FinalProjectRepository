using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Data.Models
{
    using static Website.Common.EntityValidationConstants.CustomerUser;

    public class CustomerUser
    {
        [Key]
        [Comment("The id of the customer")]
        public Guid CustomerId { get; set; }

        [Required]
        [MinLength(CustomerNameMinLength)]
        [MaxLength(CustomerNameMaxLength)]
        [Comment("Username of the customer")]
        public string Username { get; set; } = null!;

        [Required]
        [Comment("The password of the customer")]
        public string Password { get; set; } = null!;

        [Required]
        [Comment("The email of the user")]
        //[RegularExpression] potential
        public string Email { get; set; } = null!;

        [Required]
        [Comment("The address of the customer")]
        public string Address { get; set; } = null!;

        //Add CartId
        [Required]
        [Comment("The id of the user's cart")]
        public int CartId { get; set; }

        [Required]
        [ForeignKey(nameof(CartId))]
        public Cart Cart { get; set; } = null!;

        public virtual IEnumerable<Order> Orders { get; set; } 
                = new HashSet<Order>();
    }
}
