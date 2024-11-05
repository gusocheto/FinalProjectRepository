using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Data.Models.Enums;

namespace Website.Data.Models
{
    public class Category
    {
        [Key]
        [Comment("The id of the categoryType")]
        public int CategoryId { get; set; }

        [Comment("The name of the categoryType")]
        public CategoryEnumaration CategoryType { get; set; }
    }
}
