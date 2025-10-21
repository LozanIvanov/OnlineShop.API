using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.DTOs
{
    public class OrderItemDTO
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemDTO> Items { get; set; } = new();
        public decimal TotalAmount { get; set; }
    }

    public class CreateOrderDTO
    {
        public Guid UserId { get; set; }
        public List<CreateOrderItemDTO> Items { get; set; } = new();
    }

    public class CreateOrderItemDTO
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
