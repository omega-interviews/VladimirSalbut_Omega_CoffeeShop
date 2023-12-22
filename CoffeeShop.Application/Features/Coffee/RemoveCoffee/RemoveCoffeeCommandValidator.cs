using FluentValidation;

namespace CoffeeShop.Application.Features.Coffee.RemoveCoffee
{
    public class RemoveCoffeeCommandValidator : AbstractValidator<RemoveCoffeeCommand>
    {
        public RemoveCoffeeCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
