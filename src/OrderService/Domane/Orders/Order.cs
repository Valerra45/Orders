using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Orders
{
    public class Order : BaseEntity
    {
        public virtual List<Product> Products { get; set; } = new();
    }
}
