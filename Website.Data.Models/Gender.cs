using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Website.Data.Models.Enums;

namespace Website.Data.Models
{
    public class Gender
    {

        [Key]
        [Comment("Id of the gender type")]
        public int GenderId { get; set; }

        [Comment("The gender declaration for a product")]
        public GenderEnumaration GenderType { get; set; }
    }
}
