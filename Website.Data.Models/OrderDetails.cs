using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Common;

namespace Website.Data.Models
{
    using static Website.Common.EntityValidationConstants.OderDetails;
    public class OrderDetails
    {
        [Key]
        [Comment("The ID of the order details")]
        public int OrderDetailsID { get; set; }

        [Required]
        [Comment("The ID of the customer associated with the order")]
        public Guid CustomerUserID { get; set; }

        [Required]
        [ForeignKey(nameof(CustomerUserID))]
        [Comment("The customer associated with the order")]
        public CustomerUser CustomerUser { get; set; } = null!;

        [Required]
        [Comment("The shipping address for the order")]
        public string ShippingAddress { get; set; } = null!;

        [Required]
        [Comment("The city for the shipping address")]
        [MinLength(CityNameMinLength)]
        [MaxLength(CityNameMaxLength)]
        public string City { get; set; } = null!;

        [Required]
        [Comment("The country for the shipping address")]
        [MinLength(CountryNameMinLength)]
        [MaxLength(CountryNameMaxLength)]
        public string Country { get; set; } = null!;

        [Required]
        [Comment("The zip code for the shipping address")]
        public string ZipCode { get; set; } = null!;

        [Required]
        [Comment("The total amount paid for the order")]
        public decimal AmountPaid { get; set; }
    }
}
