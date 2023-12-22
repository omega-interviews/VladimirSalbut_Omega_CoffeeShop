using FluentValidation;

namespace CoffeeShop.Application.Features.Coffee.ModifyCoffee
{
    public class ModifyCoffeeCommandValidator : AbstractValidator<ModifyCoffeeCommand>
    {
        public ModifyCoffeeCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.AmountOfCoffee).GreaterThan(0);
            RuleFor(x => x.TimeToBrew).GreaterThan(0);
        }
    }
}
