using CoffeeShop.Domain;

namespace CoffeeShop.Infrastucture.Repositories
{
    public interface IBaristaRepository
    {
        List<Barista> GetAllBaristas();
    }
}
