using CoffeeShop.Infrastucture.Repositories;
using MediatR;

namespace CoffeeShop.Application.Features.Coffee.GetAllCoffees
{
    public class GetAllCoffeesQueryHandler : IRequestHandler<GetAllCoffeesQuery, List<CoffeeResponse>>
    {
        private readonly ICoffeeRepository _coffeeRepository;

        public GetAllCoffeesQueryHandler(ICoffeeRepository coffeeRepository)
        {
            _coffeeRepository = coffeeRepository;
        }

        public Task<List<CoffeeResponse>> Handle(GetAllCoffeesQuery request, CancellationToken cancellationToken)
        {
            var coffees = _coffeeRepository.GetAllCoffees().Select(c => new CoffeeResponse
            {
                Id = c.Id,
                Type = c.Type,
                Price = c.Price,
                Picture = c.Picture,
                AmountOfCoffee = c.AmountOfCoffee,
                TimeToBrew = c.TimeToBrew
            }).ToList();

            return Task.FromResult(coffees);
        }
    }
}
