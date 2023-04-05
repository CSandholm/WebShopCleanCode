﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WebShopCleanCode
{
	public class WebShop
	{
		bool running = true;
		Database database = new Database();
		List<Product> products = new List<Product>();
		List<Customer> customers = new List<Customer>();

		string currentMenu = "main menu";
		int currentChoice = 1;
		int amountOfOptions = 3;
		string option1 = "See Wares";
		string option2 = "Customer Info";
		string option3 = "Login";
		string option4 = "";
		string info = "What would you like to do?";

		string username = null;
		string password = null;
		Customer currentCustomer;

		public WebShop()
		{
			products = database.GetProducts();
			customers = database.GetCustomers();
		}

		public void Run()
		{
			while (running)
			{
				TypeMainMenu();
				string choice = Console.ReadLine().ToLower();
				switch (choice)
				{
					case "left":
					case "l":
						if (currentChoice > 1)
						{
							currentChoice--;
						}
						break;
					case "right":
					case "r":
						if (currentChoice < amountOfOptions)
						{
							currentChoice++;
						}
						break;
					case "ok":
					case "k":
					case "o":
						if (currentMenu.Equals("main menu"))
						{
							MainMenu();
						}
						else if (currentMenu.Equals("customer menu"))
						{
							CustomerMenu();
						}
						else if (currentMenu.Equals("sort menu"))
						{
							SortMenu();
						}
						else if (currentMenu.Equals("wares menu"))
						{
							WaresMenu();
						}
						else if (currentMenu.Equals("login menu"))
						{
							LoginMenu(choice);
						}
						else if (currentMenu.Equals("purchase menu"))
						{
							PurchaseMenu();
						}
						break;
					case "back":
					case "b":
						if (currentMenu.Equals("main menu"))
						{
							Console.WriteLine();
							Console.WriteLine("You're already on the main menu.");
							Console.WriteLine();
						}
						else if (currentMenu.Equals("purchase menu"))
						{
							SetWareMenuOptions();
						}
						else
						{
							SetMainMenuOptions();
						}
						break;
					case "quit":
					case "q":
						Console.WriteLine("The console powers down. You are free to leave.");
						return;
					default:
						Console.WriteLine("That is not an applicable option.");
						break;
				}
			}
		}
		private void TypeMainMenu()
		{
			Console.WriteLine("Welcome to the WebShop!");
			Console.WriteLine(info);

			if (currentMenu.Equals("purchase menu"))
			{
				for (int i = 0; i < amountOfOptions; i++)
				{
					Console.WriteLine(i + 1 + ": " + products[i].Name + ", " + products[i].Price + "kr");
				}
				Console.WriteLine("Your funds: " + currentCustomer.Funds);
			}
			else
			{
				Console.WriteLine("1: " + option1);
				Console.WriteLine("2: " + option2);
				if (amountOfOptions > 2)
				{
					Console.WriteLine("3: " + option3);
				}
				if (amountOfOptions > 3)
				{
					Console.WriteLine("4: " + option4);
				}
			}

			for (int i = 0; i < amountOfOptions; i++)
			{
				Console.Write(i + 1 + "\t");
			}
			Console.WriteLine();
			for (int i = 1; i < currentChoice; i++)
			{
				Console.Write("\t");
			}
			Console.WriteLine("|");

			Console.WriteLine("Your buttons are Left, Right, OK, Back and Quit.");
			if (currentCustomer != null)
			{
				Console.WriteLine("Current user: " + currentCustomer.Username);
			}
			else
			{
				Console.WriteLine("Nobody logged in.");
			}
		}

		private void MainMenu()
		{
			switch (currentChoice)
			{
				case 1:
					SetWareMenuOptions();
					break;
				case 2:
					if (currentCustomer != null)
					{
						option1 = "See your orders";
						option2 = "Set your info";
						option3 = "Add funds";
						option4 = "";
						amountOfOptions = 3;
						currentChoice = 1;
						info = "What would you like to do?";
						currentMenu = "customer menu";
					}
					else
					{
						Console.WriteLine();
						Console.WriteLine("Nobody is logged in.");
						Console.WriteLine();
					}
					break;
				case 3:
					if (currentCustomer == null)
					{
						SetLoginMenuOptions();
						username = null;
						password = null;
					}
					else
					{
						option3 = "Login";
						Console.WriteLine();
						Console.WriteLine(currentCustomer.Username + " logged out.");
						Console.WriteLine();
						currentChoice = 1;
						currentCustomer = null;
					}
					break;
				default:
					Console.WriteLine();
					Console.WriteLine("Not an option.");
					Console.WriteLine();
					break;
			}
		}

		private void CustomerMenu()
		{
			switch (currentChoice)
			{
				case 1:
					currentCustomer.PrintOrders();
					break;
				case 2:
					currentCustomer.PrintInfo();
					break;
				case 3:
					Console.WriteLine("How many funds would you like to add?");
					string amountString = Console.ReadLine();
					try
					{
						int amount = int.Parse(amountString);
						if (amount < 0)
						{
							Console.WriteLine();
							Console.WriteLine("Don't add negative amounts.");
							Console.WriteLine();
						}
						else
						{
							currentCustomer.Funds += amount;
							Console.WriteLine();
							Console.WriteLine(amount + " added to your profile.");
							Console.WriteLine();
						}
					}
					catch (FormatException e)
					{
						Console.WriteLine();
						Console.WriteLine("Please write a number next time.");
						Console.WriteLine();
					}
					break;
				default:
					Console.WriteLine();
					Console.WriteLine("Not an option.");
					Console.WriteLine();
					break;
			}
		}

		private void SortMenu()
		{
			bool back = true;
			switch (currentChoice)
			{
				case 1:
					BubbleSort("name", false);
					Console.WriteLine();
					Console.WriteLine("Wares sorted.");
					Console.WriteLine();
					break;
				case 2:
					BubbleSort("name", true);
					Console.WriteLine();
					Console.WriteLine("Wares sorted.");
					Console.WriteLine();
					break;
				case 3:
					BubbleSort("price", false);
					Console.WriteLine();
					Console.WriteLine("Wares sorted.");
					Console.WriteLine();
					break;
				case 4:
					BubbleSort("price", true);
					Console.WriteLine();
					Console.WriteLine("Wares sorted.");
					Console.WriteLine();
					break;
				default:
					back = false;
					Console.WriteLine();
					Console.WriteLine("Not an option.");
					Console.WriteLine();
					break;
			}
			if (back)
			{
				SetWareMenuOptions();
			}
		}

		private void WaresMenu()
		{
			switch (currentChoice)
			{
				case 1:
					Console.WriteLine();
					foreach (Product product in products)
					{
						product.PrintInfo();
					}
					Console.WriteLine();
					break;
				case 2:
					if (currentCustomer != null)
					{
						currentMenu = "purchase menu";
						info = "What would you like to purchase?";
						currentChoice = 1;
						amountOfOptions = products.Count;
					}
					else
					{
						Console.WriteLine();
						Console.WriteLine("You must be logged in to purchase wares.");
						Console.WriteLine();
						currentChoice = 1;
					}
					break;
				case 3:
					option1 = "Sort by name, descending";
					option2 = "Sort by name, ascending";
					option3 = "Sort by price, descending";
					option4 = "Sort by price, ascending";
					info = "How would you like to sort them?";
					currentMenu = "sort menu";
					currentChoice = 1;
					amountOfOptions = 4;
					break;
				case 4:
					if (currentCustomer == null)
					{
						SetLoginMenuOptions();
					}
					else
					{
						option4 = "Login";
						Console.WriteLine();
						Console.WriteLine(currentCustomer.Username + " logged out.");
						Console.WriteLine();
						currentCustomer = null;
						currentChoice = 1;
					}
					break;
				case 5:
					break;
				default:
					Console.WriteLine();
					Console.WriteLine("Not an option.");
					Console.WriteLine();
					break;
			}
		}

		private void LoginMenu(string choice)
		{
			switch (currentChoice)
			{
				case 1:
					Console.WriteLine("A keyboard appears.");
					Console.WriteLine("Please input your username.");
					username = Console.ReadLine();
					Console.WriteLine();
					break;
				case 2:
					Console.WriteLine("A keyboard appears.");
					Console.WriteLine("Please input your password.");
					password = Console.ReadLine();
					Console.WriteLine();
					break;
				case 3:
					if (username == null || password == null)
					{
						Console.WriteLine();
						Console.WriteLine("Incomplete data.");
						Console.WriteLine();
					}
					else
					{
						bool found = false;
						foreach (Customer customer in customers)
						{
							if (username.Equals(customer.Username) && customer.CheckPassword(password))
							{
								Console.WriteLine();
								Console.WriteLine(customer.Username + " logged in.");
								Console.WriteLine();
								currentCustomer = customer;
								found = true;
								SetMainMenuOptions();
								break;
							}
						}
						if (found == false)
						{
							Console.WriteLine();
							Console.WriteLine("Invalid credentials.");
							Console.WriteLine();
						}
					}
					break;
				case 4:


					Customer newCustomer = NewCustomer();
					customers.Add(newCustomer);
					currentCustomer = newCustomer;
					Console.WriteLine();
					Console.WriteLine(newCustomer.Username + " successfully added and is now logged in.");
					Console.WriteLine();
					SetMainMenuOptions();
					break;
				default:
					Console.WriteLine();
					Console.WriteLine("Not an option.");
					Console.WriteLine();
					break;
			}
		}

		private void PurchaseMenu()
		{
			int index = currentChoice - 1;
			Product product = products[index];
			if (product.InStock())
			{
				if (currentCustomer.CanAfford(product.Price))
				{
					currentCustomer.Funds -= product.Price;
					product.NrInStock--;
					currentCustomer.Orders.Add(new Order(product.Name, product.Price, DateTime.Now));
					Console.WriteLine();
					Console.WriteLine("Successfully bought " + product.Name);
					Console.WriteLine();
				}
				else
				{
					Console.WriteLine();
					Console.WriteLine("You cannot afford.");
					Console.WriteLine();
				}
			}
			else
			{
				Console.WriteLine();
				Console.WriteLine("Not in stock.");
				Console.WriteLine();
			}
		}
		private Customer NewCustomer()
		{
			Console.WriteLine("Please write your username.");
			string newUsername = Console.ReadLine();
			foreach (Customer customer in customers)
			{
				if (customer.Username.Equals(username))
				{
					Console.WriteLine();
					Console.WriteLine("Username already exists.");
					Console.WriteLine();
					break;
				}
			}
			// Would have liked to be able to quit at any time in here.
			string choice = "";
			bool next = false;
			string firstName = null;
			string lastName = null;
			string email = null;
			int age = -1;
			string address = null;
			string phoneNumber = null;

			Console.WriteLine("Do you want a password? y/n");
			string newPassword = SetChoiceYesOrNo();

			while (true)
			{
				Console.WriteLine("Do you want a first name? y/n");
				choice = Console.ReadLine();
				if (choice.Equals("y"))
				{
					while (true)
					{
						Console.WriteLine("Please write your first name.");
						firstName = Console.ReadLine();
						if (firstName.Equals(""))
						{
							Console.WriteLine();
							Console.WriteLine("Please actually write something.");
							Console.WriteLine();
							continue;
						}
						else
						{
							next = true;
							break;
						}
					}
				}
				if (choice.Equals("n") || next)
				{
					next = false;
					break;
				}
				Console.WriteLine();
				Console.WriteLine("y or n, please.");
				Console.WriteLine();
			}
			while (true)
			{
				Console.WriteLine("Do you want a last name? y/n");
				choice = Console.ReadLine();
				if (choice.Equals("y"))
				{
					while (true)
					{
						Console.WriteLine("Please write your last name.");
						lastName = Console.ReadLine();
						if (lastName.Equals(""))
						{
							Console.WriteLine();
							Console.WriteLine("Please actually write something.");
							Console.WriteLine();
							continue;
						}
						else
						{
							next = true;
							break;
						}
					}
				}
				if (choice.Equals("n") || next)
				{
					next = false;
					break;
				}
				Console.WriteLine();
				Console.WriteLine("y or n, please.");
				Console.WriteLine();
			}
			while (true)
			{
				Console.WriteLine("Do you want an email? y/n");
				choice = Console.ReadLine();
				if (choice.Equals("y"))
				{
					while (true)
					{
						Console.WriteLine("Please write your email.");
						email = Console.ReadLine();
						if (email.Equals(""))
						{
							Console.WriteLine();
							Console.WriteLine("Please actually write something.");
							Console.WriteLine();
							continue;
						}
						else
						{
							next = true;
							break;
						}
					}
				}
				if (choice.Equals("n") || next)
				{
					next = false;
					break;
				}
				Console.WriteLine();
				Console.WriteLine("y or n, please.");
				Console.WriteLine();
			}
			while (true)
			{
				Console.WriteLine("Do you want an age? y/n");
				choice = Console.ReadLine();
				if (choice.Equals("y"))
				{
					while (true)
					{
						Console.WriteLine("Please write your age.");
						string ageString = Console.ReadLine();
						try
						{
							age = int.Parse(ageString);
						}
						catch (FormatException e)
						{
							Console.WriteLine();
							Console.WriteLine("Please write a number.");
							Console.WriteLine();
							continue;
						}
						next = true;
						break;
					}
				}
				if (choice.Equals("n") || next)
				{
					next = false;
					break;
				}
				Console.WriteLine();
				Console.WriteLine("y or n, please.");
				Console.WriteLine();
			}
			while (true)
			{
				Console.WriteLine("Do you want an address? y/n");
				choice = Console.ReadLine();
				if (choice.Equals("y"))
				{
					while (true)
					{
						Console.WriteLine("Please write your address.");
						address = Console.ReadLine();
						if (address.Equals(""))
						{
							Console.WriteLine();
							Console.WriteLine("Please actually write something.");
							Console.WriteLine();
							continue;
						}
						else
						{
							next = true;
							break;
						}
					}
				}
				if (choice.Equals("n") || next)
				{
					next = false;
					break;
				}
				Console.WriteLine();
				Console.WriteLine("y or n, please.");
				Console.WriteLine();
			}
			while (true)
			{
				Console.WriteLine("Do you want a phone number? y/n");
				choice = Console.ReadLine();
				if (choice.Equals("y"))
				{
					while (true)
					{
						Console.WriteLine("Please write your phone number.");
						phoneNumber = Console.ReadLine();
						if (phoneNumber.Equals(""))
						{
							Console.WriteLine();
							Console.WriteLine("Please actually write something.");
							Console.WriteLine();
							continue;
						}
						else
						{
							next = true;
							break;
						}
					}
				}
				if (choice.Equals("n") || next)
				{
					break;
				}
				Console.WriteLine();
				Console.WriteLine("y or n, please.");
				Console.WriteLine();
			}

			return new Customer(newUsername, newPassword, firstName, lastName, email, age, address, phoneNumber);
		}
		private string SetChoiceYesOrNo()
		{
			string choice;
			while (true)
			{
				choice = Console.ReadLine();
				if (choice.Equals("y"))
				{
					return SetText();
				}
				if (choice.Equals("n"))
				{
					return null;
				}
				Console.WriteLine();
				Console.WriteLine("y or n, please.");
				Console.WriteLine();
			}
		}
		private string SetText()
		{
			string input;
			while (true)
			{
				Console.WriteLine("Please write your password.");
				input = Console.ReadLine();
				if (input.Equals(""))
				{
					Console.WriteLine();
					Console.WriteLine("Please actually write something.");
					Console.WriteLine();
				}
				else
					break;
			}
			return input;
		}

	private void BubbleSort(string variable, bool ascending)
	{
		if (variable.Equals("name"))
		{
			int length = products.Count;
			for (int i = 0; i < length - 1; i++)
			{
				bool sorted = true;
				int length2 = length - i;
				for (int j = 0; j < length2 - 1; j++)
				{
					if (ascending)
					{
						if (products[j].Name.CompareTo(products[j + 1].Name) < 0)
						{
							Product temp = products[j];
							products[j] = products[j + 1];
							products[j + 1] = temp;
							sorted = false;
						}
					}
					else
					{
						if (products[j].Name.CompareTo(products[j + 1].Name) > 0)
						{
							Product temp = products[j];
							products[j] = products[j + 1];
							products[j + 1] = temp;
							sorted = false;
						}
					}
				}
				if (sorted == true)
				{
					break;
				}
			}
		}
		else if (variable.Equals("price"))
		{
			int length = products.Count;
			for (int i = 0; i < length - 1; i++)
			{
				bool sorted = true;
				int length2 = length - i;
				for (int j = 0; j < length2 - 1; j++)
				{
					if (ascending)
					{
						if (products[j].Price > products[j + 1].Price)
						{
							Product temp = products[j];
							products[j] = products[j + 1];
							products[j + 1] = temp;
							sorted = false;
						}
					}
					else
					{
						if (products[j].Price < products[j + 1].Price)
						{
							Product temp = products[j];
							products[j] = products[j + 1];
							products[j + 1] = temp;
							sorted = false;
						}
					}
				}
				if (sorted == true)
				{
					break;
				}
			}
		}
	}
	private void SetLoginMenuOptions()
	{
		option1 = "Set Username";
		option2 = "Set Password";
		option3 = "Login";
		option4 = "Register";
		amountOfOptions = 4;
		info = "Please submit username and password.";
		currentChoice = 1;
		currentMenu = "login menu";
	}
	private void SetWareMenuOptions()
	{
		option1 = "See all wares";
		option2 = "Purchase a ware";
		option3 = "Sort wares";
		if (currentCustomer == null)
		{
			option4 = "Login";
		}
		else
		{
			option4 = "Logout";
		}
		amountOfOptions = 4;
		currentChoice = 1;
		currentMenu = "wares menu";
		info = "What would you like to do?";
	}
	private void SetMainMenuOptions()
	{
		option1 = "See Wares";
		option2 = "Customer Info";
		if (currentCustomer == null)
		{
			option3 = "Login";
		}
		else
		{
			option3 = "Logout";
		}
		info = "What would you like to do?";
		currentMenu = "main menu";
		currentChoice = 1;
		amountOfOptions = 3;
	}

	private void SetName(string name)
	{

	}
}
}

