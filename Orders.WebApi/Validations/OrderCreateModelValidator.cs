using FluentValidation;
using Orders.WebApi.Domain.Orders.Models;

namespace Orders.WebApi.Validations
{
    public class OrderCreateModelValidator : AbstractValidator<OrderCreateModel>
    {
        public OrderCreateModelValidator() 
        {
            RuleFor(x => x.Lines)
                .Must(x => x?.Any() ?? false)
                .WithMessage("Unable to save order without lines");

            RuleForEach(x => x.Lines)
                .Must(x => x.Quantity > 0)
                .WithMessage("Quantity must be greater than 0");
        }
    }
}
