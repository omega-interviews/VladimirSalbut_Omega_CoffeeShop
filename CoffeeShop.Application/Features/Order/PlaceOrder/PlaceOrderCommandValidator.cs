using CoffeeShop.Domain.Enumerations;
using FluentValidation;

namespace CoffeeShop.Application.Features.Order.PlaceOrder
{
    public class PlaceOrderCommandValidator : AbstractValidator<PlaceOrderCommand>
    {
        public PlaceOrderCommandValidator()
        {
            RuleFor(x => x.CoffeeId).NotEmpty();
            RuleFor(x => x.OrderType).NotEmpty().Must(o => Enum.IsDefined(typeof(OrderType), o));
        }
    }
}
