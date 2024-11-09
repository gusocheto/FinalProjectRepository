using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Website.Data.Models.Enums;

namespace Website.Data.Models
{
    public class ProductType
    {

        [Key]
        [Comment("Id of the gender type")]
        public int GenderId { get; set; }

        [Comment("The gender declaration for a product")]
        public ProductCategorizationEnumaration ProductTypeName { get; set; }
    }
}
