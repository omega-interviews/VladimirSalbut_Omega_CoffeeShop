using FluentValidation;

namespace CoffeeShop.Application.Features.Coffee.AddCoffee
{
    public class AddCoffeeCommandValidator : AbstractValidator<AddCoffeeCommand>
    {
        public AddCoffeeCommandValidator()
        {
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.AmountOfCoffee).GreaterThan(0);
            RuleFor(x => x.TimeToBrew).GreaterThan(0);
        }
    }
}
