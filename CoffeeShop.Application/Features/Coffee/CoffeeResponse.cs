namespace CoffeeShop.Application.Features.Coffee
{
    public record CoffeeResponse
    {
        public Guid Id { get; init; }
        public required string Type { get; init; }
        public decimal Price { get; init; }
        public required string Picture { get; init; }
        public int AmountOfCoffee { get; init; }        
        public int TimeToBrew { get; init; }
    }
}
