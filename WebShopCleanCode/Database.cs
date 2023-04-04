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
            customersInDatabase.Add(new Customer("jimmy", "jimisthebest", "Jimmy", "Jamesson", "jj@mail.com", 22, "Big Street 5", "123456789"));
            customersInDatabase.Add(new Customer("jake", "jake123", "Jake", null, null, 0, null, null));
        }

        public List<Product> GetProducts()
        {
            return productsInDatabase;
        }

        public List<Customer> GetCustomers()
        {
            return customersInDatabase;
        }
    }
}
