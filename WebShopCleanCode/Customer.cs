using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode
{
    public class Customer
    {
        public string Username { get; set; }
        private string password;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int Funds { get; set; }
        public List<Order> Orders { get; set; }
        public Customer(string username, string password, string firstName, string lastName, string email, int age, string address, string phoneNumber)
        {
            Username = username;
            this.password = password;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Age = age;
            Address = address;
            PhoneNumber = phoneNumber;
            Orders = new List<Order>();
            Funds = 0;
        }

        public bool CanAfford(int price)
        {
            return Funds >= price;
        }

        public bool CheckPassword(string password)
        {
            if (password == null)
            {
                return true;
            }
            return password.Equals(this.password);
        }

        public void PrintInfo()
        {
            Console.WriteLine();
            Console.Write("Username: " + Username + "");
            if (password != null)
            {
                Console.Write(", Password: " + password);
            }
            if (FirstName != null)
            {
                Console.Write(", First Name: " + FirstName);
            }
            if (LastName != null)
            {
                Console.Write(", Last Name: " + LastName);
            }
            if (Email != null)
            {
                Console.Write(", Email: " + Email);
            }
            if (Age != -1)
            {
                Console.Write(", Age: " + Age);
            }
            if (Address != null)
            {
                Console.Write(", Address: " + Address);
            }
            if (PhoneNumber != null)
            {
                Console.Write(", Phone Number: " + PhoneNumber);
            }
            Console.WriteLine(", Funds: " + Funds);
            Console.WriteLine();
        }

        public void PrintOrders()
        {
            Console.WriteLine();
            foreach (Order order in Orders)
            {
                order.PrintInfo();
            }
            Console.WriteLine();
        }
    }
}
