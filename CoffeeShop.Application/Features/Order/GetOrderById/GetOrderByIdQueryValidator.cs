using CoffeeShop.Application.Features.Coffee.GetCoffeeById;
using FluentValidation;

namespace CoffeeShop.Application.Features.Order.GetOrderById
{
    public class GetOrderByIdQueryValidator : AbstractValidator<GetCoffeeByIdQuery>
    {
        public GetOrderByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
