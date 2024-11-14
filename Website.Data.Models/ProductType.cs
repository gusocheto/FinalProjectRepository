using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Website.Data.Models.Enums;

namespace Website.Data.Models
{
    public class ProductType
    {
        [Key]
        [Comment("Id of the gender type")]
        public int ProductTypeId { get; set; }

        [Comment("The gender declaration for a product")]
        [Required]
        public ProductCategorizationEnumaration ProductTypeName { get; set; }
    }
}
