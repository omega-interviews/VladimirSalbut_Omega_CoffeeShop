using CoffeeShop.Domain;

namespace CoffeeShop.Infrastucture.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        //Simulates a table/document in the database.
        private static List<Order> orderList = new List<Order>();

        public OrderRepository() 
        {
            InitializeData();
        }

        public List<Order> GetAllUnprocessedOrder()
        {
            return orderList.Where(x => x.StartProcessingAt == null).ToList();
        }

        public Order? GetOrderById(Guid orderId)
        {
            return orderList.FirstOrDefault(x => x.Id == orderId);
        }

        public void AddOrder(Order order)
        {
            orderList.Add(order);
        }

        public void ModifyOrder(Order order)
        {
            var existingOrder = orderList.First(x => x.Id == order.Id);

            orderList.Remove(existingOrder);
            orderList.Add(order);
        }

        public static void InitializeData() 
        {
            orderList = new List<Order>();
        }
    }
}
