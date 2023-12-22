using CoffeeShop.Infrastucture.Repositories;
using CoffeeShop.Infrastucture.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeShop.BddSpec
{
    public class CoffeeShopApiFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services => 
            {
                var descriptor = services.Single(s => s.ImplementationType == typeof(ProcessOrderService));
                services.Remove(descriptor);

                //var coffeeRepository = services.BuildServiceProvider().GetService<ICoffeeRepository>();
                //coffeeRepository?.InitializeData();
                //var orderRepository = services.BuildServiceProvider().GetService<IOrderRepository>();
                //orderRepository?.InitializeData();
            });
        }
    }
}
