using MediatR;

namespace CoffeeShop.Application.Features.Coffee.GetCoffeeById
{
    public record GetCoffeeByIdQuery(Guid Id) : IRequest<CoffeeResponse>;
}
