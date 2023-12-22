namespace CoffeeShop.Application.Features.Order
{
    public record OrderResponse
    {
        public Guid Id { get; init; }
        public required string CoffeeType { get; init; }
        public decimal Price { get; init; }
        public DateTime? StartProcessingAt { get; init; }
        public DateTime? CompletedAt { get; init; }
        public bool IsCompleted => CompletedAt != null;
    }
}
