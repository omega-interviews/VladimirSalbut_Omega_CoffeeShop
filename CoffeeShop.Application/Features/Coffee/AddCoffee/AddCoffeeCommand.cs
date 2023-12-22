using MediatR;

namespace CoffeeShop.Application.Features.Coffee.AddCoffee
{
    public record AddCoffeeCommand(string Type, decimal Price, string Picture, int AmountOfCoffee, int TimeToBrew) 
        : IRequest<CoffeeResult>;
}
