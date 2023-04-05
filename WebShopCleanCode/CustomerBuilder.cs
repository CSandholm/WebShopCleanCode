using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode
{
	internal class CustomerBuilder
	{
		string username;
		string password;
		string firstName;
		string lastName;
		string email;
		int age;
		string address;
		string phone;
		int funds;
		List<Order> orders;

		//We want to be able to keep the constructors of customer simple. The builder design pattern helps us do that.
		public CustomerBuilder()
		{
			username = string.Empty;
			password = string.Empty;
			firstName = string.Empty;
			lastName = string.Empty;
			email = string.Empty;
			age = 0;
			address = string.Empty;
			phone = string.Empty;
			funds = 0;
			orders = new List<Order>();
		}

		//Set up default values and chain setters.
		public CustomerBuilder Username(string username) { this.username = username; return this; }
		public CustomerBuilder Password(string password) { this.password = password; return this; }
		public CustomerBuilder FirstName(string firstName) { this.firstName = firstName; return this; }
		public CustomerBuilder LastName(string lastName) { this.lastName = lastName; return this; }
		public CustomerBuilder Email(string email) { this.email = email; return this; }
		public CustomerBuilder Age(int age) { this.age = age; return this; }
		public CustomerBuilder Address(string address) { this.address = address; return this; }
		public CustomerBuilder Phone(string phone) { this.phone = phone; return this; }
		public CustomerBuilder Funds(int funds) { this.funds = funds; return this; }
		public CustomerBuilder Order(List<Order> orders) { this.orders = orders; return this; }

		//Build the Customer object with set or default values.
		public Customer Build()
		{
			return new Customer(username, password, firstName, lastName, email, age, address, phone, funds, orders);
		}
	}
}
