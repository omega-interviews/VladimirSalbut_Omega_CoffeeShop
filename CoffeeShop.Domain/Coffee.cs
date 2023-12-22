namespace CoffeeShop.Domain
{
    public class Coffee
    {
        public Coffee(Guid id, string type, decimal price, string picture, int amountOfCoffee, int timeToBrew)
        {
            Id = id;
            Type = type;
            Price = price;
            Picture = picture;
            AmountOfCoffee = amountOfCoffee;
            TimeToBrew = timeToBrew;
        }

        public Guid Id { get; private set; }
        public string Type { get; private set; }
        public decimal Price { get; private set; }
        public string Picture { get; private set; }
        public int AmountOfCoffee { get; private set; }
        public int TimeToBrew { get; private set; }

        public void Update(string type, decimal price, string picture, int amountOfCoffee, int timeToBrew)
        {
            Type = type;
            Price = price;
            Picture = picture;
            AmountOfCoffee = amountOfCoffee;
            TimeToBrew = timeToBrew;
        }
    }
}
