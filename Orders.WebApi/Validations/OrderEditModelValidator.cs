using FluentValidation;
using Orders.WebApi.Domain.Orders.Enums;
using Orders.WebApi.Domain.Orders.Models;

namespace Orders.WebApi.Validations
{
    public class OrderEditModelValidator : AbstractValidator<OrderEditModel>
    {
        public OrderEditModelValidator() 
        {
            RuleFor(x => x).Must(x => Enum.TryParse(x.Status, out OrderStatus status))
                .WithMessage("Wrong status text");

            RuleFor(x => x.Lines)
                .Must(x => x?.Any() ?? false)
                .WithMessage("Unable to save order without lines");

            RuleForEach(x => x.Lines)
                .Must(x => x.Quantity > 0)
                .WithMessage("Quantity must be greater than 0");
        }
    }
}
