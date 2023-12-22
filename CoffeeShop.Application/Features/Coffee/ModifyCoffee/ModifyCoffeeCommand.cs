using MediatR;

namespace CoffeeShop.Application.Features.Coffee.ModifyCoffee
{
    public record ModifyCoffeeCommand(Guid Id, string Type, decimal Price, string Picture, int AmountOfCoffee, int TimeToBrew) : IRequest<Unit>;

    public record ModifyCoffeeRequest(string Type, decimal Price, string Picture, int AmountOfCoffee, int TimeToBrew);

}
