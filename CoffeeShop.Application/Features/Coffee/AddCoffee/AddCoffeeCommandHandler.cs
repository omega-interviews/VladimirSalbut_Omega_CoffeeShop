using CoffeeShop.Infrastucture.Repositories;
using MediatR;

namespace CoffeeShop.Application.Features.Coffee.AddCoffee
{

    public sealed class AddCoffeeCommandHandler : IRequestHandler<AddCoffeeCommand, CoffeeResult>
    {
        private readonly ICoffeeRepository _coffeeRepository;
        public AddCoffeeCommandHandler(ICoffeeRepository coffeeRepository)
        {
            _coffeeRepository = coffeeRepository;
        }

        public Task<CoffeeResult> Handle(AddCoffeeCommand request, CancellationToken cancellationToken)
        {
            var coffee = new Domain.Coffee(Guid.NewGuid(), request.Type, request.Price, request.Picture, 
                request.AmountOfCoffee, request.TimeToBrew);
            _coffeeRepository.AddCoffee(coffee);

            return Task.FromResult(new CoffeeResult(coffee.Id));
        }
    }
}
