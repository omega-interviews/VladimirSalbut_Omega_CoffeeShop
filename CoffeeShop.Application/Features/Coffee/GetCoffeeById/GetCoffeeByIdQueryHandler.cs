using CoffeeShop.Domain.Exceptions;
using CoffeeShop.Infrastucture.Repositories;
using MediatR;

namespace CoffeeShop.Application.Features.Coffee.GetCoffeeById
{
    public class GetCoffeeByIdQueryHandler : IRequestHandler<GetCoffeeByIdQuery, CoffeeResponse>
    {
        private readonly ICoffeeRepository _coffeeRepository;

        public GetCoffeeByIdQueryHandler(ICoffeeRepository coffeeRepository)
        {
            _coffeeRepository = coffeeRepository;
        }

        public Task<CoffeeResponse> Handle(GetCoffeeByIdQuery request, CancellationToken cancellationToken)
        {
            var coffee = _coffeeRepository.GetCoffeeById(request.Id);
            if (coffee == null)
            {
                throw new NotFoundException("Coffee not found");
            }
            return Task.FromResult(new CoffeeResponse
            {
                Id = coffee.Id,
                Type = coffee.Type,
                Price = coffee.Price,
                Picture = coffee.Picture,
                AmountOfCoffee = coffee.AmountOfCoffee,
                TimeToBrew = coffee.TimeToBrew
            });
        }
    }
}
