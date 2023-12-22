using CoffeeShop.Domain;

namespace CoffeeShop.Infrastucture.Repositories
{
    public interface IOrderRepository
    {
        List<Order> GetAllUnprocessedOrder();
        Order? GetOrderById(Guid orderId);
        void AddOrder(Order order);
        void ModifyOrder(Order order);
    }
}
