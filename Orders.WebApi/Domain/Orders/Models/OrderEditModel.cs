using Orders.WebApi.Domain.Orders.Entities;
using Orders.WebApi.Domain.Orders.Enums;
using Orders.WebApi.Domain.Products.Entities;
using System.ComponentModel.DataAnnotations;

namespace Orders.WebApi.Domain.Orders.Models
{
    public class OrderEditModel
    {
        public OrderStatus Status { get; set; }

        [Required]
        public IEnumerable<Product>? Lines { get; set; }

        public void ApplyToEntity(Order order)
        {
            order.Status = Status;
            order.Lines = Lines;
        }
    }
}
