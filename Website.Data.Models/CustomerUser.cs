using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Website.Common;

namespace Website.Data.Models
{
    using static Website.Common.EntityValidationConstants.CustomerUser;

    public class CustomerUser
    {
        [Key]
        [Comment("The ID of the customer")]
        public Guid CustomerId { get; set; }

        [Required]
        [MinLength(CustomerNameMinLength)]
        [MaxLength(CustomerNameMaxLength)]
        [Comment("Username of the customer")]
        public string Username { get; set; } = null!;

        [Required]
        [Comment("The password of the customer (hashed)")]
        public string Password { get; set; } = null!; // Store as a hash in production

        [Required]
        [Comment("The email of the customer")]
        [RegularExpression(EmailValidation, ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = null!;

        [Required]
        [Comment("The address of the customer")]
        public string Address { get; set; } = null!;

        [Required]
        [Comment("The ID of the user's cart")]
        public Guid CartId { get; set; }

        [Required]
        [ForeignKey(nameof(CartId))]
        public Cart Cart { get; set; } = null!;

        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;

        [Comment("The collection of orders made by the customer")]
        public virtual IEnumerable<Order> Orders { get; set; } 
            = new HashSet<Order>();
    }
}
