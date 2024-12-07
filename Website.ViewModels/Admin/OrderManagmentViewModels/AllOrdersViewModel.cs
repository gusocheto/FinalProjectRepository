using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Data.Models;

namespace Website.ViewModels.Admin.OrderManagmentViewModels
{
    public class AllOrdersViewModel
    {
        public string OrderId { get; set; } = null!;

        public string NameOfClient { get; set; } = null!;

        public IEnumerable<string> Statuses { get; set; } = null!;
    }
}
