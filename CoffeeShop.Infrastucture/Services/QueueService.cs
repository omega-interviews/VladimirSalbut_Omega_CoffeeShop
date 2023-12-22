using CoffeeShop.Domain;
using CoffeeShop.Domain.Enumerations;

namespace CoffeeShop.Infrastucture.Services
{
    public static class QueueService
    {
        public static PriorityQueue<Order, (OrderType, DateTime)> orderQueue = new PriorityQueue<Order, (OrderType, DateTime)>();
    }
}
