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
		public string Phone { get; set; }
		public int Funds { get; set; }
		public List<Order> Orders { get; set; }

		public Customer(string username, string password, string firstName, string lastName, string email,
						int age, string address, string phone, int funds, List<Order> orders)
		{
			Username = username;
			this.password = password;
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			Age = age;
			Address = address;
			Phone = phone;
			Funds = funds;
			Orders = orders;
		}
		public bool CanAfford(int price)
		{
			return Funds >= price;
		}
		public bool CheckPassword(string password)
		{
			if (password != string.Empty)
			{
				return true;
			}
			return password.Equals(this.password);
		}
		public void PrintInfo()
		{
			Console.WriteLine();
			Console.Write("Username: " + Username + "");
			if (this.password != string.Empty)
			{
				Console.Write(", Password: " + this.password);
			}
			if (FirstName != string.Empty)
			{
				Console.Write(", First Name: " + FirstName);
			}
			if (LastName != string.Empty)
			{
				Console.Write(", Last Name: " + LastName);
			}
			if (Email != string.Empty)
			{
				Console.Write(", Email: " + Email);
			}
			if (Age != -1)
			{
				Console.Write(", Age: " + Age);
			}
			if (Address != string.Empty)
			{
				Console.Write(", Address: " + Address);
			}
			if (Phone != string.Empty)
			{
				Console.Write(", Phone Number: " + Phone);
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
