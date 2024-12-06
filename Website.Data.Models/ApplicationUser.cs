using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Website.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
        }

        public virtual ICollection<OrderUser> UserOrders { get; set; } 
            = new HashSet<OrderUser>();

        public ICollection<CartProducts> ProductCarts { get; set; } =
             new List<CartProducts>();

        public virtual ICollection<CustomerUser> CustomerUsers { get; set; }
             = new HashSet<CustomerUser>();
    }
}
