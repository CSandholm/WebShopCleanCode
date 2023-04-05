using System;
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
		Database database = new Database();
		List<Product> products = new List<Product>();
		List<Customer> customers = new List<Customer>();
		BubbleSort sort = new BubbleSort();
		Customer currentCustomer;

		string currentMenu;
		int currentChoice;
		int amountOfOptions;
		string option1;
		string option2;
		string option3;
		string option4;
		string info;
		string username = null;
		string password = null;
		public WebShop()
		{
			products = database.GetProducts();
			customers = database.GetCustomers();
		}
		public void Run()
		{
			SetMainMenuOptions();
			while (true)
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
						WriteNotAnOption();
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
						SetCustomerMenuOptions();
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
						ResetCurrentChoice();
						currentCustomer = null;
					}
					break;
				default:
					WriteNotAnOption();
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
						WriteNotAnOption();
					}
					break;
				default:
					WriteNotAnOption();
					break;
			}
		}

		private void SortMenu()
		{
			bool back = true;
			switch (currentChoice)
			{
				case 1:
					products = sort.Run("name", false, products);
					WriteWaresSorted();
					break;
				case 2:
					products = sort.Run("name", true, products);
					WriteWaresSorted();
					break;
				case 3:
					products = sort.Run("price", false, products);
					WriteWaresSorted();
					break;
				case 4:
					products = sort.Run("price", true, products);
					WriteWaresSorted();
					break;
				default:
					back = false;
					WriteNotAnOption();
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
						SetPurchaseMenuOptions();
					}
					else
					{
						Console.WriteLine();
						Console.WriteLine("You must be logged in to purchase wares.");
						Console.WriteLine();
						ResetCurrentChoice();
					}
					break;
				case 3:
					SetSortMenuOptions();
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
						ResetCurrentChoice();
					}
					break;
				case 5:
					break;
				default:
					WriteNotAnOption();
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
					WriteNotAnOption();
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
			Console.WriteLine("Do you want a password? y/n");
			string newPassword = SetChoiceYesOrNo();
			Console.WriteLine("Do you want a first name? y/n");
			string firstName = SetChoiceYesOrNo();
			Console.WriteLine("Do you want a last name? y/n");
			string lastName = SetChoiceYesOrNo();
			Console.WriteLine("Do you want an email? y/n");
			string email = SetChoiceYesOrNo();
			Console.WriteLine("Do you want an age? y/n");
			int age = 0;
			try { age = Convert.ToInt32(SetChoiceYesOrNo()); }
			catch { Console.WriteLine("Invalid input for age! Age set to 0."); }
			Console.WriteLine("Do you want an address? y/n");
			string address = SetChoiceYesOrNo();
			Console.WriteLine("Do you want a phone number? y/n");
			string phoneNumber = SetChoiceYesOrNo();

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
				Console.WriteLine("Please write your input.");
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

		private void SetCustomerMenuOptions()
		{
			option1 = "See your orders";
			option2 = "Set your info";
			option3 = "Add funds";
			option4 = "";
			amountOfOptions = 3;
			ResetCurrentChoice();
			info = "What would you like to do?";
			currentMenu = "customer menu";
		}
		private void SetPurchaseMenuOptions()
		{
			currentMenu = "purchase menu";
			info = "What would you like to purchase?";
			ResetCurrentChoice();
			amountOfOptions = products.Count;
		}
		private void SetSortMenuOptions()
		{
			option1 = "Sort by name, descending";
			option2 = "Sort by name, ascending";
			option3 = "Sort by price, descending";
			option4 = "Sort by price, ascending";
			info = "How would you like to sort them?";
			currentMenu = "sort menu";
			ResetCurrentChoice();
			amountOfOptions = 4;
		}
		private void SetLoginMenuOptions()
		{
			option1 = "Set Username";
			option2 = "Set Password";
			option3 = "Login";
			option4 = "Register";
			amountOfOptions = 4;
			info = "Please submit username and password.";
			ResetCurrentChoice();
			currentMenu = "login menu";
		}
		private void SetWareMenuOptions()
		{
			option1 = "See all wares";
			option2 = "Purchase a ware";
			option3 = "Sort wares";
			option4 = SetCurrentCustomer();
			amountOfOptions = 4;
			ResetCurrentChoice();
			currentMenu = "wares menu";
			info = "What would you like to do?";
		}
		private void SetMainMenuOptions()
		{
			option1 = "See Wares";
			option2 = "Customer Info";
			option3 = SetCurrentCustomer();
			info = "What would you like to do?";
			currentMenu = "main menu";
			ResetCurrentChoice();
			amountOfOptions = 3;
		}
		private string SetCurrentCustomer()
		{
			string option;
			if (currentCustomer == null)
			{
				option = "Login";
			}
			else
			{
				option = "Logout";
			}
			return option;
		}
		private void WriteNotAnOption()
		{
			Console.WriteLine();
			Console.WriteLine("Not an option.");
			Console.WriteLine();
		}
		private void WriteWaresSorted()
		{
			Console.WriteLine();
			Console.WriteLine("Wares sorted.");
			Console.WriteLine();
		}
		private void ResetCurrentChoice()
		{
			currentChoice = 1;
		}
	}
}

