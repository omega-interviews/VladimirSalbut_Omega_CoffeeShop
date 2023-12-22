using CoffeeShop.Application.Features.Coffee;
using CoffeeShop.Application.Features.Coffee.AddCoffee;
using CoffeeShop.Application.Features.Coffee.ModifyCoffee;
using System.Net;

namespace CoffeeShop.BddSpec
{
    public class CoffeeTests : BaseIntegrationTest
    {
        public CoffeeTests(CoffeeShopApiFactory factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetAllCoffeesEndpoint_ShouldReturnListOfCoffees()
        {
            // Arrange:

            // Act:
            var response = await _httpClient.GetAsync("/api/coffees");

            // Assert:
            response.EnsureSuccessStatusCode(); // Ensure a successful HTTP status code.

            var coffeeResponseList = Deserialize<List<CoffeeResponse>>(response);
            Assert.True(coffeeResponseList?.Count == 3); // Ensure the response has 3 items
        }

        [Fact]
        public async Task GetCoffeeByIdEndpoint_ShouldReturnCoffeeResponse_WhenCoffeExists()
        {
            // Arrange: 
            var coffeId = "6d5afa38-8158-4eba-808b-1a751eb60759";

            // Act:
            var coffeeResponse = await _httpClient.GetAsync($"/api/coffees/{coffeId}");

            // Assert:
            coffeeResponse.EnsureSuccessStatusCode(); // Ensure a successful HTTP status code.

            var coffee = Deserialize<CoffeeResponse>(coffeeResponse);
            Assert.NotNull(coffee); // Ensure not null
        }

        [Fact]
        public async Task GetCoffeeByIdEndpoint_ShouldReturnNotFound_WhenCoffeNotExists()
        {
            // Arrange: 
            var coffeId = "91797590-e566-4a36-9329-e6820559458c";

            // Act:
            var coffeeResponse = await _httpClient.GetAsync($"/api/coffees/{coffeId}");

            // Assert:
            Assert.Equal(HttpStatusCode.NotFound, coffeeResponse.StatusCode); // Ensure 404
        }

        [Fact]
        public async Task AddCoffeeEndpoint_ShouldCreateNewCoffee()
        {
            // Arrange: 
            var addCoffeeCommand = new AddCoffeeCommand("Mokka", 3, 
                "https://www.mokka.in/cdn/shop/products/Arabica-Chicory-20__1_083639ce-4c53-40e1-8512-91608c4738db_1024x1024@2x.jpg?v=1602047797", 10, 60);

            // Act:
            var response = await _httpClient.PostAsync($"/api/coffees", Serialize(addCoffeeCommand));

            // Assert:
            response.EnsureSuccessStatusCode(); // Ensure a successful HTTP status code.

            var result = Deserialize<CoffeeResult>(response);
            Assert.NotNull(result?.CoffeeId);

            var coffeeResponse = await _httpClient.GetAsync($"/api/coffees/{result?.CoffeeId}");
            var coffee = Deserialize<CoffeeResponse>(coffeeResponse);

            //Ensure all properties are saved
            Assert.Equal(coffee?.Type, addCoffeeCommand.Type);
            Assert.Equal(coffee?.Price, addCoffeeCommand.Price);
            Assert.Equal(coffee?.Picture, addCoffeeCommand.Picture);
            Assert.Equal(coffee?.TimeToBrew, addCoffeeCommand.TimeToBrew);
            Assert.Equal(coffee?.AmountOfCoffee, addCoffeeCommand.AmountOfCoffee);
        }

        [Fact]
        public async Task AddCoffeeEndpoint_ShouldReturnBadRequest_WhenPriceIsZero()
        {
            // Arrange: 
            var addCoffeeCommand = new AddCoffeeCommand("Mokka", 0,
                "https://www.mokka.in/cdn/shop/products/Arabica-Chicory-20__1_083639ce-4c53-40e1-8512-91608c4738db_1024x1024@2x.jpg?v=1602047797", 10, 60);

            // Act:
            var response = await _httpClient.PostAsync($"/api/coffees", Serialize(addCoffeeCommand));

            // Assert:
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode); // Ensure 400
        }

        [Fact]
        public async Task ModifyCoffeeEndpoint_ShouldModifyCoffee()
        {
            // Arrange:            
            var coffeeId = "6d5afa38-8158-4eba-808b-1a751eb60759";
            var modifyCoffeeRequest = new ModifyCoffeeRequest("Mokka", 3,
                "https://www.mokka.in/cdn/shop/products/Arabica-Chicory-20__1_083639ce-4c53-40e1-8512-91608c4738db_1024x1024@2x.jpg?v=1602047797", 10, 60);

            // Act:
            var response = await _httpClient.PutAsync($"/api/coffees/{coffeeId}", Serialize(modifyCoffeeRequest));

            // Assert:
            response.EnsureSuccessStatusCode(); // Ensure a successful HTTP status code.

            var coffeeResponse = await _httpClient.GetAsync($"/api/coffees/{coffeeId}");
            var coffee = Deserialize<CoffeeResponse>(coffeeResponse);

            //Ensure all properties are saved
            Assert.Equal(coffee?.Type, modifyCoffeeRequest.Type);
            Assert.Equal(coffee?.Price, modifyCoffeeRequest.Price);
            Assert.Equal(coffee?.Picture, modifyCoffeeRequest.Picture);
            Assert.Equal(coffee?.TimeToBrew, modifyCoffeeRequest.TimeToBrew);
            Assert.Equal(coffee?.AmountOfCoffee, modifyCoffeeRequest.AmountOfCoffee);
        }

        [Fact]
        public async Task ModifyCoffeeEndpoint_ShouldReturnBadRequest_WhenPriceIsZero()
        {
            // Arrange: 
            var coffeeId = "6d5afa38-8158-4eba-808b-1a751eb60759";
            var modifyCoffeeRequest = new ModifyCoffeeRequest("Mokka", 0,
                "https://www.mokka.in/cdn/shop/products/Arabica-Chicory-20__1_083639ce-4c53-40e1-8512-91608c4738db_1024x1024@2x.jpg?v=1602047797", 10, 60);

            // Act:
            var response = await _httpClient.PutAsync($"/api/coffees/{coffeeId}", Serialize(modifyCoffeeRequest));

            // Assert:
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode); // Ensure 400
        }

        [Fact]
        public async Task RemoveCoffeeEndpoint_ShouldDeleteCoffee()
        {
            // Arrange:
            var coffeeListResponse = await _httpClient.GetAsync("/api/coffees");
            var coffeeList = Deserialize<List<CoffeeResponse>>(coffeeListResponse);

            var coffeeId = "ab68ed76-c2d6-4a8f-83be-00bc8305b4aa";

            // Act:
            var response = await _httpClient.DeleteAsync($"/api/coffees/{coffeeId}");

            // Assert:
            response.EnsureSuccessStatusCode(); // Ensure a successful HTTP status code.

            var coffeeListAfterDeleteResponse = await _httpClient.GetAsync("/api/coffees");
            var coffeeListAfterDelete = Deserialize<List<CoffeeResponse>>(coffeeListAfterDeleteResponse);

            Assert.Equal(coffeeListAfterDelete?.Count, coffeeList?.Count - 1); // Ensure one item is deleted
        }

        [Fact]
        public async Task RemoveCoffeeEndpoint_ShouldReturnNotFound_WhenCoffeNotExists()
        {
            // Arrange: 
            var coffeId = "91797590-e566-4a36-9329-e6820559458c";

            // Act:
            var coffeeResponse = await _httpClient.DeleteAsync($"/api/coffees/{coffeId}");

            // Assert:
            Assert.Equal(HttpStatusCode.NotFound, coffeeResponse.StatusCode); // Ensure 404
        }
    }
}