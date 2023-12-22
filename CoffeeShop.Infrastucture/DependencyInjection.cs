using CoffeeShop.Infrastucture.Repositories;
using CoffeeShop.Infrastucture.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeShop.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<ICoffeeRepository, CoffeeRepository>();
            services.AddSingleton<IBartenderRepository, BartenderRepository>();
            services.AddSingleton<IBaristaRepository, BaristaRepository>();
            services.AddSingleton<IOrderRepository, OrderRepository>();

            services.AddHostedService<ProcessOrderService>();

            return services;
        }
    }
}
