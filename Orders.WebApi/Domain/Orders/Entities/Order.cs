using Orders.WebApi.Abstractions;
using Orders.WebApi.Domain.Orders.Enums;
using Orders.WebApi.Domain.Products.Entities;
using System.ComponentModel.DataAnnotations;

namespace Orders.WebApi.Domain.Orders.Entities
{
    public class Order : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public OrderStatus Status { get; set; }
            
        public DateTime Created { get; set; }

        public IEnumerable<Product> Lines { get; set; }
    }
}
