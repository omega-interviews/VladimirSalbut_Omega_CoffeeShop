using CoffeeShop.Domain.Exceptions;
using CoffeeShop.Infrastucture.Repositories;
using MediatR;

namespace CoffeeShop.Application.Features.Order.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<OrderResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = _orderRepository.GetOrderById(request.Id);
            if (order == null)
            {
                throw new NotFoundException("Order not found");
            }
            return Task.FromResult(new OrderResponse
            {
                Id = order.Id,
                CoffeeType = order.CoffeeType,
                Price = order.Price,
                StartProcessingAt = order.StartProcessingAt,
                CompletedAt = order.CompletedAt
            });
        }
    }
}
