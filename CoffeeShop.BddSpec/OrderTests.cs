using CoffeeShop.Application.Features.Order;
using CoffeeShop.Application.Features.Order.PlaceOrder;
using CoffeeShop.Domain.Enumerations;
using System.Net;

namespace CoffeeShop.BddSpec
{
    public class OrderTests : BaseIntegrationTest
    {
        public OrderTests(CoffeeShopApiFactory factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetOrderByIdEndpoint_ShouldReturnOrderResponse_WhenOrderExists()
        {
            // Arrange:
            var coffeeId = "6d5afa38-8158-4eba-808b-1a751eb60759";
            var placeOrderCommand = new PlaceOrderCommand(Guid.Parse(coffeeId), nameof(OrderType.AtTable));
            var placeOrderResponse = await _httpClient.PostAsync($"/api/orders", Serialize(placeOrderCommand));
            var orderResult = Deserialize<OrderResult>(placeOrderResponse);

            // Act:
            var response = await _httpClient.GetAsync($"/api/orders/{orderResult?.OrderId}");

            // Assert:
            response.EnsureSuccessStatusCode(); // Ensure a successful HTTP status code.

            var order = Deserialize<OrderResponse>(response);
            Assert.NotNull(order); // Ensure not null
        }

        [Fact]
        public async Task GetOrderByIdEndpoint_ShouldReturnNotFound_WhenOrderNotExists()
        {
            // Arrange: 
            var orderId = "983bf5fa-1963-4452-995a-989ff75773f6";

            // Act:
            var response = await _httpClient.GetAsync($"/api/orders/{orderId}");

            // Assert:
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode); // Ensure 404
        }

        [Fact]
        public async Task PlaceOrderEndpoint_ShouldReturnOrderResult()
        {
            // Arrange:
            var coffeeId = "6d5afa38-8158-4eba-808b-1a751eb60759";
            var placeOrderCommand = new PlaceOrderCommand(Guid.Parse(coffeeId), nameof(OrderType.ToGo));

            // Act:
            var response = await _httpClient.PostAsync($"/api/orders", Serialize(placeOrderCommand));

            // Assert:
            response.EnsureSuccessStatusCode(); // Ensure a successful HTTP status code.

            var order = Deserialize<OrderResult>(response);
            Assert.NotNull(order); // Ensure not null
        }

        [Fact]
        public async Task PlaceOrderEndpoint_ShouldReturnNotFound_WhenCoffeeNotExists()
        {
            // Arrange: 
            var coffeeId = "1c1a1550-b5d4-453b-9722-f7d9d18ca10a";
            var placeOrderCommand = new PlaceOrderCommand(Guid.Parse(coffeeId), nameof(OrderType.AtTable));

            // Act:
            var response = await _httpClient.PostAsync($"/api/orders", Serialize(placeOrderCommand));

            // Assert:
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode); // Ensure 404
        }

        [Fact]
        public async Task PlaceOrderEndpoint_ShouldReturnBadRequest_WhenOrderTypeIsWrong()
        {
            // Arrange: 
            var coffeeId = "6d5afa38-8158-4eba-808b-1a751eb60759";
            var placeOrderCommand = new PlaceOrderCommand(Guid.Parse(coffeeId), "Test");

            // Act:
            var response = await _httpClient.PostAsync($"/api/orders", Serialize(placeOrderCommand));

            // Assert:
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode); // Ensure 400
        }

        [Fact]
        public async Task PlaceOrderEndpoint_ShouldReturnBadRequest_WhenBartenderHasFiceOrdersInQueue()
        {
            // Arrange: 
            var coffeeId = "6d5afa38-8158-4eba-808b-1a751eb60759";
            var placeOrderCommand = new PlaceOrderCommand(Guid.Parse(coffeeId), nameof(OrderType.ToGo));
            var order1response = await _httpClient.PostAsync($"/api/orders", Serialize(placeOrderCommand));
            var order2response = await _httpClient.PostAsync($"/api/orders", Serialize(placeOrderCommand));
            var order3response = await _httpClient.PostAsync($"/api/orders", Serialize(placeOrderCommand));
            var order4response = await _httpClient.PostAsync($"/api/orders", Serialize(placeOrderCommand));
            var order5response = await _httpClient.PostAsync($"/api/orders", Serialize(placeOrderCommand));

            // Act:
            var order6response = await _httpClient.PostAsync($"/api/orders", Serialize(placeOrderCommand));

            // Assert:
            order1response.EnsureSuccessStatusCode(); // Ensure a successful HTTP status code.
            order2response.EnsureSuccessStatusCode(); // Ensure a successful HTTP status code.
            order3response.EnsureSuccessStatusCode(); // Ensure a successful HTTP status code.
            order4response.EnsureSuccessStatusCode(); // Ensure a successful HTTP status code.
            order5response.EnsureSuccessStatusCode(); // Ensure a successful HTTP status code.
            Assert.Equal(HttpStatusCode.BadRequest, order6response.StatusCode); // Ensure 400
        }
    }
}
