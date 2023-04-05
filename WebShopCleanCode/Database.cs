using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode
{
    public class Database
    {
        // We just pretend this accesses a real database.
        private List<Product> productsInDatabase;
        private List<Customer> customersInDatabase;
        public Database()
        {
            productsInDatabase = new List<Product>();
            productsInDatabase.Add(new Product("Mirror", 300, 2));
            productsInDatabase.Add(new Product("Car", 2000000, 2));
            productsInDatabase.Add(new Product("Candle", 50, 2));
            productsInDatabase.Add(new Product("Computer", 100000, 2));
            productsInDatabase.Add(new Product("Game", 599, 2));
            productsInDatabase.Add(new Product("Painting", 399, 2));
            productsInDatabase.Add(new Product("Chair", 500, 2));
            productsInDatabase.Add(new Product("Table", 1000, 2));
            productsInDatabase.Add(new Product("Bed", 20000, 2));

            customersInDatabase = new List<Customer>();
			customersInDatabase.Add(new CustomerBuilder().Username("jimmy").Password("jimisthebest").FirstName("Jimmy").LastName("Jamesson").Age(22).Address("Big Street 5").Phone("123456789").Build());
			customersInDatabase.Add(new CustomerBuilder().Username("jake").Password("jake123").Build());
		}

        public List<Product> GetProducts()
        {
            return productsInDatabase;
        }

        public List<Customer> GetCustomers()
        {
            return customersInDatabase;
        }
        public Product GetProductByName(string name)
        {
            return productsInDatabase.Find(x => x.Name == name);
        }
		public List<ProductProxy> GetProductProxies()
		{
			List<ProductProxy> productProxies = new List<ProductProxy>();
            foreach(Product product in productsInDatabase)
            {
                productProxies.Add(new ProductProxy(product.Name, this));
            }
			return productProxies;
		}
	}
}
