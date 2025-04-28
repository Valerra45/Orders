using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Orders
{
    public class OrderRequest
    {
        public List<ProductOrderRequest> Products { get; set; } = new();
    }
}
