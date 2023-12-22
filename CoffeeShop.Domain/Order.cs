namespace CoffeeShop.Domain
{
    public class Order
    {
        public Order(Guid id, DateTime? startProcessingAt, DateTime? completedAt, Guid? bartenderId, 
            string coffeeType, decimal price, int amountOfCoffee, int timeToBrew)
        {
            Id = id;
            StartProcessingAt = startProcessingAt;
            CompletedAt = completedAt;
            BartenderId = bartenderId;
            CoffeeType = coffeeType;
            Price = price;
            AmountOfCoffee = amountOfCoffee;
            TimeToBrew = timeToBrew;
        }

        public Guid Id { get; private set; }
        public DateTime? StartProcessingAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public Guid? BartenderId { get; private set; }
        public string CoffeeType { get; private set; }
        public decimal Price { get; private set; }
        public int AmountOfCoffee { get; private set; }
        public int TimeToBrew { get; private set; }

        public void UpdateStartProcessingAt(DateTime startProcessingAt)
        {
            StartProcessingAt = startProcessingAt;
        }
        public void UpdateCompletedAt(DateTime completedAt)
        {
            CompletedAt = completedAt;
        }
    }
}
