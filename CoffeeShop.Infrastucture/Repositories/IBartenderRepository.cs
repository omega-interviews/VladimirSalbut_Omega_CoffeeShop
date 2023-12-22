using CoffeeShop.Domain;

namespace CoffeeShop.Infrastucture.Repositories
{
    public interface IBartenderRepository
    {
        List<Bartender> GetAllBartenders();
    }
}
