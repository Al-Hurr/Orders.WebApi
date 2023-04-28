using Orders.WebApi.Domain.Orders.Entities;
using Orders.WebApi.Domain.Orders.Enums;
using Orders.WebApi.Domain.Products.Entities;

namespace Orders.WebApi.Domain.Orders.Models
{
    public class OrderEditModel
    {
        public string Status { get; set; }

        public IEnumerable<Product>? Lines { get; set; }

        public void ApplyToEntity(Order order)
        {
            order.Status = Enum.Parse<OrderStatus>(Status);
            order.Lines = Lines;
        }
    }
}
