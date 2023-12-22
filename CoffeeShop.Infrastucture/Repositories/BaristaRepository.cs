using CoffeeShop.Domain;

namespace CoffeeShop.Infrastucture.Repositories
{
    public class BaristaRepository : IBaristaRepository
    {
        //Simulates a table/collection in the database.
        private static List<Barista> baristaList = new List<Barista>();

        public BaristaRepository()
        {
            InitializeData();
        }

        public List<Barista> GetAllBaristas()
        {
            return baristaList;
        }

        public static void InitializeData()
        {
            baristaList = new List<Barista>
            {
                new Barista(Guid.NewGuid()),
                new Barista(Guid.NewGuid()),
                new Barista(Guid.NewGuid())
            };
        }
    }
}
