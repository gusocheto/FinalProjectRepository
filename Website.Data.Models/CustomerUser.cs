﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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

        //Add OrderId

        //Add CartId
    }
}
