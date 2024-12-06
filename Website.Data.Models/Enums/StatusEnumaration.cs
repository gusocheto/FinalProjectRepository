using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Data.Models.Enums
{
    public enum StatusEnumaration
    {
        Pending = 2,
        Processing = 4,
        Shipped = 6,
        Delivered = 8,
        Cancelled = 10,
        Returned = 12,

    }
}
