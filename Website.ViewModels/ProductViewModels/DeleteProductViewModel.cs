using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.ViewModels.ProductViewModels
{
    public class DeleteProductViewModel
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string AdminId { get; set; } = null!;
        public string AdminName { get; set; } = null!;
    }
}
