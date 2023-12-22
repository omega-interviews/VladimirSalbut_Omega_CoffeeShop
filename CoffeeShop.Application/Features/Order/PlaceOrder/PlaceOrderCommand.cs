using MediatR;

namespace CoffeeShop.Application.Features.Order.PlaceOrder
{
    public record PlaceOrderCommand(Guid CoffeeId, string OrderType) : IRequest<OrderResult>;
}
