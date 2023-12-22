using MediatR;

namespace CoffeeShop.Application.Features.Coffee.RemoveCoffee
{
    public record RemoveCoffeeCommand(Guid Id) : IRequest<Unit>;
}
