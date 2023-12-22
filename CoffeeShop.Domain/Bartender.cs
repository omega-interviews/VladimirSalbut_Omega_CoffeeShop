namespace CoffeeShop.Domain
{
    public class Bartender
    {
        public Bartender(Guid id)
        {
            Id = id;
            UnprocessedOrders = new List<Order>();
        }
        public Guid Id { get; private set; }

        public List<Order> UnprocessedOrders { get; private set; }

        public void UpdateUnprocessedOrders(List<Order> unprocessedOrders)
        {
            UnprocessedOrders = unprocessedOrders;
        }
    }


}
