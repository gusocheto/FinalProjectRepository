using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Website.Data.Models.Enums;

namespace Website.Data.Models
{
    public class Status
    {
        [Key]
        [Comment("Id of the status type")]
        public int StatusId { get; set; }

        [Comment("Status type of the order")]
        [Required]
        public StatusEnumaration StatusType { get; set; }
    }
}
