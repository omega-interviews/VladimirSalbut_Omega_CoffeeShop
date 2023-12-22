using MediatR;

namespace CoffeeShop.Application.Features.Order.GetOrderById
{
    public record GetOrderByIdQuery(Guid Id) : IRequest<OrderResponse>;
}