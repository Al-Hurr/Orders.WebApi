using Orders.WebApi.Domain.Orders.Entities;
using Orders.WebApi.Domain.Orders.Enums;
using Orders.WebApi.Domain.Products.Entities;

namespace Orders.WebApi.Domain.Orders.Models
{
    public class OrderGetModel
    {
        public Guid Id { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime Created { get; set; }

        public IEnumerable<Product>? Lines { get; set; }

        public static OrderGetModel FromEntity(Order order)
        {
            return new OrderGetModel
            {
                Id = order.Id,
                Status = order.Status,
                Created = order.Created,
                Lines = order.Lines
            };
        }
    }
}
