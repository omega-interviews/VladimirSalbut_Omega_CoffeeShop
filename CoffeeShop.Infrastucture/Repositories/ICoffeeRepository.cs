using CoffeeShop.Domain;

namespace CoffeeShop.Infrastucture.Repositories
{
    public interface ICoffeeRepository
    {
        List<Coffee> GetAllCoffees();
        Coffee? GetCoffeeById(Guid coffeeId);
        void AddCoffee(Coffee coffee);
        void RemoveCoffee(Guid coffeeId);
        void ModifyCoffee(Coffee coffee);
    }
}
