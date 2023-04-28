using Orders.WebApi.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Orders.WebApi.Domain.Products.Entities
{
    public class Product : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public int Quantity { get; set; }
    }
}
