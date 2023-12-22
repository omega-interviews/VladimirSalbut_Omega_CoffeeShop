using CoffeeShop.Domain.Exceptions;
using CoffeeShop.Infrastucture.Repositories;
using MediatR;

namespace CoffeeShop.Application.Features.Coffee.RemoveCoffee
{
    public sealed class RemoveCoffeeCommandHandler : IRequestHandler<RemoveCoffeeCommand, Unit>
    {
        private readonly ICoffeeRepository _coffeeRepository;
        public RemoveCoffeeCommandHandler(ICoffeeRepository coffeeRepository)
        {
            _coffeeRepository = coffeeRepository;
        }

        public Task<Unit> Handle(RemoveCoffeeCommand request, CancellationToken cancellationToken)
        {
            var coffee = _coffeeRepository.GetCoffeeById(request.Id);
            if (coffee == null)
            {
                throw new NotFoundException("Coffee not found");
            }

            _coffeeRepository.RemoveCoffee(coffee.Id);
            return Task.FromResult(Unit.Value);
        }
    }
}
