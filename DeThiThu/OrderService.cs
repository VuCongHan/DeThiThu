using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeThiThu
{
    public class OrderService
    {
        public class Order
        {
            public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        }

        public class OrderItem
        {
            public string? ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }

        public decimal CalculateTotalAmount(Order order)
        {
            if (order == null || order.Items == null)
                throw new ArgumentNullException(nameof(order));

            return order.Items.Sum(item => item.Quantity * item.Price);
        }
    }
}
