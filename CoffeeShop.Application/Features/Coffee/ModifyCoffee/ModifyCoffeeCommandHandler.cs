using CoffeeShop.Domain.Exceptions;
using CoffeeShop.Infrastucture.Repositories;
using MediatR;

namespace CoffeeShop.Application.Features.Coffee.ModifyCoffee
{
    public sealed class ModifyCoffeeCommandHandler : IRequestHandler<ModifyCoffeeCommand, Unit>
    {
        private readonly ICoffeeRepository _coffeeRepository;
        public ModifyCoffeeCommandHandler(ICoffeeRepository coffeeRepository)
        {
            _coffeeRepository = coffeeRepository;
        }

        public Task<Unit> Handle(ModifyCoffeeCommand request, CancellationToken cancellationToken)
        {
            var coffee = _coffeeRepository.GetCoffeeById(request.Id);
            if (coffee == null)
            {
                throw new NotFoundException("Coffee not found");
            }

            coffee.Update(request.Type, request.Price, request.Picture, request.AmountOfCoffee, request.TimeToBrew);
            _coffeeRepository.ModifyCoffee(coffee);

            return Task.FromResult(Unit.Value);
        }
    }
}
