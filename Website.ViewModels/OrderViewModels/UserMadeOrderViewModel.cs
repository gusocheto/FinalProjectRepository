using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Data.Models;

namespace Website.ViewModels.OrderViewModels
{
    public class UserMadeOrderViewModel
    {
        public Guid OrderId { get; set; }

        public DateTime DateOnOrderMade { get; set; }

        public DateTime ApproximetlyArrivalDate { get; set; }

        public string StatusType { get; set; } = null!;

        public int CountOfItems { get; set; }
    }
}
