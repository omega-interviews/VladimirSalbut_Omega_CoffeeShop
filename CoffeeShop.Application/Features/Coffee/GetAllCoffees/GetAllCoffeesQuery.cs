using MediatR;

namespace CoffeeShop.Application.Features.Coffee.GetAllCoffees
{
    public record GetAllCoffeesQuery : IRequest<List<CoffeeResponse>>;
}
