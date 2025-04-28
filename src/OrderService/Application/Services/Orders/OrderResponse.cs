using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Orders
{
    public class OrderResponse
    {
        public Guid Id { get; set; }

        public List<ProductOrderResponse> Products { get; set; } = new();
    }
}
