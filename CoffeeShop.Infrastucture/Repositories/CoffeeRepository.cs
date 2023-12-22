using CoffeeShop.Domain;

namespace CoffeeShop.Infrastucture.Repositories
{
    public class CoffeeRepository : ICoffeeRepository
    {
        //Simulates a table/collection in the database.
        private static List<Coffee> coffeeList = new List<Coffee>();

        public CoffeeRepository()
        {
            InitializeData();
        }


        public List<Coffee> GetAllCoffees()
        {
            return coffeeList;
        }

        public Coffee? GetCoffeeById(Guid coffeeId)
        {
            return coffeeList.FirstOrDefault(x => x.Id == coffeeId);
        }

        public void AddCoffee(Coffee coffee)
        {
            coffeeList.Add(coffee);
        }

        public void ModifyCoffee(Coffee coffee)
        {
            var existingCoffee = coffeeList.First(x => x.Id == coffee.Id);

            coffeeList.Remove(existingCoffee);
            coffeeList.Add(coffee);
        }

        public void RemoveCoffee(Guid coffeeId)
        {
            var existingCoffee = coffeeList.First(x => x.Id == coffeeId);
            coffeeList.Remove(existingCoffee);
        }

        public static void InitializeData()
        {
            coffeeList = new List<Coffee>
            {
                new Coffee(Guid.Parse("6d5afa38-8158-4eba-808b-1a751eb60759"), "Espresso", 1, "https://cdn2.vectorstock.com/i/1000x1000/14/21/chalk-drawn-sketch-of-espresso-coffee-vector-22361421.jpg", 7, 35),
                new Coffee(Guid.Parse("ab68ed76-c2d6-4a8f-83be-00bc8305b4aa"), "Espresso doppio", 2, "https://www.shutterstock.com/shutterstock/photos/2070794789/display_1500/stock-vector-doppio-or-double-espresso-coffee-drink-icon-or-symbol-in-cartoon-line-vector-illustration-isolated-2070794789.jpg", 14, 45),
                new Coffee(Guid.Parse("f5249851-d4e2-403d-a7af-e6182b17b61b"), "Cappuccino", 2.5m, "https://cdn.arteza.com/blogArticle/21/03/ZdgL25K-Fil9_yGg.jpg", 7, 60)
            };
        }
    }
}
