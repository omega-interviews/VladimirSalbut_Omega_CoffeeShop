using CoffeeShop.Domain;

namespace CoffeeShop.Infrastucture.Repositories
{
    public class BartenderRepository : IBartenderRepository
    {
        //Simulates a table/collection in the database.
        private static List<Bartender> bartenderList = new List<Bartender>();

        public BartenderRepository() 
        {
            InitializeData();
        }

        public List<Bartender> GetAllBartenders()
        {
            return bartenderList;
        }

        public static void InitializeData()
        {
            bartenderList = new List<Bartender>
            {
                new Bartender(Guid.NewGuid())
            };
        }
    }
}
