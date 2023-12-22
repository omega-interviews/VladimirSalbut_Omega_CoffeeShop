using FluentValidation;

namespace CoffeeShop.Application.Features.Coffee.GetCoffeeById
{
    public class GetCoffeeByIdQueryValidator : AbstractValidator<GetCoffeeByIdQuery>
    {
        public GetCoffeeByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
