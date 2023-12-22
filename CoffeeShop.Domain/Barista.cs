namespace CoffeeShop.Domain
{
    public class Barista
    {
        private int _coffeeAmountInGrams;
        public Barista(Guid id) 
        {
            Id = id;
            _coffeeAmountInGrams = 300;
        }

        public Guid Id { get; set; }

        public bool HasEnoughCoffeeInGrinder(int coffeeConsumed)
        {
            return _coffeeAmountInGrams >= coffeeConsumed;
        }

        public async Task BrewCoffee(int seconds, int coffeeConsumed)
        {
            await Task.Delay(TimeSpan.FromSeconds(seconds));
            _coffeeAmountInGrams -= coffeeConsumed;
        }

        public async Task RefillCoffeeGrinder()
        {
            await Task.Delay(TimeSpan.FromSeconds(120));// 2 minutes
            _coffeeAmountInGrams = 300;
        }
    }
}
