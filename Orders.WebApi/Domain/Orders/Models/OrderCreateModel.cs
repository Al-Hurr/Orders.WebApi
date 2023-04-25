using Orders.WebApi.Domain.Orders.Entities;
using Orders.WebApi.Domain.Products.Entities;
using System.ComponentModel.DataAnnotations;

namespace Orders.WebApi.Domain.Orders.Models
{
    public class OrderCreateModel
    {
        public Guid Id { get; set; }

        [Required]
        public IEnumerable<Product>? Lines { get; set; }

        public void ApplyToEntity(Order order)
        {
            order.Id = Id;
            order.Lines = Lines;
        }
    }
}
