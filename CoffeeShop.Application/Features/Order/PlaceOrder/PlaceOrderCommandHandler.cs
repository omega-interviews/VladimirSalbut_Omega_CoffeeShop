using CoffeeShop.Domain.Enumerations;
using CoffeeShop.Domain.Exceptions;
using CoffeeShop.Infrastucture.Repositories;
using CoffeeShop.Infrastucture.Services;
using MediatR;

namespace CoffeeShop.Application.Features.Order.PlaceOrder
{
    public sealed class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, OrderResult>
    {
        private readonly ICoffeeRepository _coffeeRepository;
        private readonly IBartenderRepository _bartenderRepository;
        private readonly IOrderRepository _orderRepository;
        public PlaceOrderCommandHandler(ICoffeeRepository coffeeRepository, IOrderRepository orderRepository, IBartenderRepository bartenderRepository)
        {
            _coffeeRepository = coffeeRepository;
            _orderRepository = orderRepository;
            _bartenderRepository = bartenderRepository;
        }

        public Task<OrderResult> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            var coffee = _coffeeRepository.GetCoffeeById(request.CoffeeId);
            if (coffee == null)
            {
                throw new NotFoundException("Coffee not found");
            }

            var order = new Domain.Order(Guid.NewGuid(), startProcessingAt: null, completedAt: null, 
                bartenderId: null, coffee.Type, coffee.Price, coffee.AmountOfCoffee, coffee.TimeToBrew);

            OrderType orderType;
            Enum.TryParse(request.OrderType, out orderType);

            if (orderType == OrderType.ToGo)
            {
                //Find available barista
                var bartenders = _bartenderRepository.GetAllBartenders();                
                var ordersByBatender = _orderRepository.GetAllUnprocessedOrder()
                    .Where(x => x.BartenderId != null)
                    .GroupBy(x => x.BartenderId);
                foreach( var bartender in bartenders)
                {
                    if (ordersByBatender.Any(x => x.Key == bartender.Id))
                    {
                        bartender.UpdateUnprocessedOrders(ordersByBatender.First(x => x.Key == bartender.Id).ToList());
                    }                    
                }

                var availableBartender = bartenders.FirstOrDefault(x => x.UnprocessedOrders.Count < 5);
                //if barista has 5 unprocessed orders, throw exception
                if (availableBartender == null)
                {
                    throw new ValidatingException("Ordering is not possible at the moment");
                }
                order = new Domain.Order(Guid.NewGuid(), startProcessingAt: null, completedAt: null, 
                    bartenderId: availableBartender.Id, coffee.Type, coffee.Price, coffee.AmountOfCoffee, coffee.TimeToBrew);
            }

            //In a real-life application, the outbox pattern should be implemented here to ensure that both
            //changes are persisted in a single transaction.
            _orderRepository.AddOrder(order);
            QueueService.orderQueue.Enqueue(order, (orderType, DateTime.Now));

            return Task.FromResult(new OrderResult(order.Id));
        }
    }
}
