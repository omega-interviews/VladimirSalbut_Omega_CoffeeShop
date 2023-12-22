using CoffeeShop.Infrastucture.Repositories;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mime;
using System.Text;

namespace CoffeeShop.BddSpec
{
    public abstract class BaseIntegrationTest : IDisposable, IClassFixture<CoffeeShopApiFactory>
    {
        protected readonly HttpClient _httpClient;
        public BaseIntegrationTest(CoffeeShopApiFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        public StringContent Serialize<T>(T obj)
        {
            return new(
                System.Text.Json.JsonSerializer.Serialize(obj),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);
        }

        public T? Deserialize<T>(HttpResponseMessage response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
                return default;

            var content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(content);
        }

        public void Dispose()
        {
            CoffeeRepository.InitializeData();
            OrderRepository.InitializeData();
            BartenderRepository.InitializeData();
            BaristaRepository.InitializeData();
        }
    }
}
