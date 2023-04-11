using Microsoft.VisualBasic;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebShopCleanCode.ButtonCommands;
using WebShopCleanCode.MenuStates;
using WebShopCleanCode.OptionStates;
using WebShopCleanCode.Sorting;
using WebShopCleanCode.Commands;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;

namespace WebShopCleanCode
{
	public class WebShop
	{
		Database database = Database.getDbInstance();
		List<Customer> customers;
		Customer currentCustomer = null;
		MenuContext previousMenuContext;
		Write write = new Write();
		Dictionary<string, MyButton> buttons;
		public List<ProductProxy> productProxies;
		public MenuContext menuContext;
		public bool running;
		public int currentChoice = 1;
		string username = null;
		string password = null;
		public Customer CurrentCustomer { get => currentCustomer; set => currentCustomer = value; }
		public WebShop()
		{
			menuContext = new MenuContext(new MainMenuState(this));
			previousMenuContext = menuContext;
			buttons = new ReturnButtonDictionary().GetButtons();
			productProxies = database.GetProductProxies();
			customers = database.GetCustomers();
			running = true;
		}
		public void Run()
		{
			while (running)
			{
				WriteMenuFromOptionContext();
				string choice = Console.ReadLine().ToLower();
				if (buttons.ContainsKey(choice))
				{
					buttons[choice].PressButton(this);
				}
				else
					write.NotAnOption();
			}
		}
		public void MainMenu()
		{
			switch (currentChoice)
			{
				case 1:
					SetContextToWareMenu();
					break;
				case 2:
					if (IsSomeoneLoggedIn())
					{
						SetContextToCustomerMenu();
					}
					else
					{
						write.NobodyLoggedIn();
					}
					break;
				case 3:
					if (!IsSomeoneLoggedIn())
					{
						SetContextToLoginMenu();
						username = null;
						password = null;
					}
					else
					{
						LogOut();
						SetContextToMain();
					}
					break;
				default:
					write.NotAnOption();
					break;
			}
		}
		public void CustomerMenu()
		{
			switch (currentChoice)
			{
				case 1:
					CurrentCustomer.PrintOrders();
					break;
				case 2:
					CurrentCustomer.PrintInfo();
					break;
				case 3:
					write.FundToAdd();
					string amountString = Console.ReadLine();
					try
					{
						AddFunds(amountString);
					}
					catch
					{
						write.NotAnOption();
					}
					break;
				default:
					write.NotAnOption();
					break;
			}
		}
		private void AddFunds(string amountString)
		{
			int amount = int.Parse(amountString);
			if (amount < 0)
			{
				write.ErrorNegativeAmount();
			}
			else
			{
				CurrentCustomer.Funds += amount;
				write.AmountAdded(amount);
			}
		}
		public void SortMenu()
		{
			bool back = true;
			ShellSort shellSort = new ShellSort();
			switch (currentChoice)
			{
				case 1:
					productProxies = shellSort.Sort("name", false, database, productProxies);
					write.WaresSorted();
					break;
				case 2:
					productProxies = shellSort.Sort("name", true, database, productProxies);
					write.WaresSorted();
					break;
				case 3:
					productProxies = shellSort.Sort("price", false, database, productProxies);
					write.WaresSorted();
					break;
				case 4:
					productProxies = shellSort.Sort("price", true, database, productProxies);
					write.WaresSorted();
					break;
				default:
					back = false;
					write.NotAnOption();
					break;
			}
			if (back)
			{
				SetContextToWareMenu();
			}
		}
		public void WaresMenu()
		{
			switch (currentChoice)
			{
				case 1:
					write.WriteEmptyLine();
					foreach (ProductProxy product in productProxies)
					{
						product.PrintInfo();
					}
					write.WriteEmptyLine();
					break;
				case 2:
					if (IsSomeoneLoggedIn())
					{
						SetContextToPurchaseMenu();
					}
					else
					{
						write.ErrorLoginToPurchaseWare();
					}
					break;
				case 3:
					SetContextToSortMenu();
					break;
				case 4:
					if (!IsSomeoneLoggedIn())
					{
						SetContextToLoginMenu();
					}
					else
					{
						LogOut();
						SetContextToWareMenu();
					}
					break;
				case 5:
					break;
				default:
					write.NotAnOption();
					break;
			}
		}
		public void LoginMenu()
		{
			switch (currentChoice)
			{
				case 1:
					write.KeyboardAppears();
					write.InputUsername();
					username = Console.ReadLine();
					write.WriteEmptyLine();
					break;
				case 2:
					write.KeyboardAppears();
					write.InputPassword();
					password = Console.ReadLine();
					write.WriteEmptyLine();
					break;
				case 3:
					if (username == null || password == null)
					{
						write.IncompleteData();
					}
					else
					{
						bool found = false;
						foreach (Customer customer in customers)
						{
							if (username.Equals(customer.Username) && customer.CheckPassword(password))
							{
								write.LoggedIn(customer);
								CurrentCustomer = customer;
								found = true;
								SetContextToMain();
								break;
							}
						}
						if (found == false)
						{
							write.InvalidCredentials();
						}
					}
					break;
				case 4:
					AddNewCustomer();
					SetContextToMain();
					break;
				default:
					write.NotAnOption();
					break;
			}
		}
		private bool IsSomeoneLoggedIn()
		{
			if (CurrentCustomer == null)
			{
				return false;
			}
			else
				return true;
		}
		private void AddNewCustomer()
		{
			Customer newCustomer = NewCustomer();
			customers.Add(newCustomer);
			CurrentCustomer = newCustomer;
			write.AddedCustomer(newCustomer);
		}
		public void PurchaseMenu()
		{
			int index = currentChoice - 1;
			Product product = database.GetProductByName(productProxies[index].Name);
			if (product.InStock())
			{
				if (CurrentCustomer.CanAfford(product.Price))
				{
					CurrentCustomer.Funds -= product.Price;
					product.NrInStock--;
					CurrentCustomer.Orders.Add(new Order(product.Name, product.Price, DateTime.Now));
					write.SuccefullyBoughtItem(product);
				}
				else
				{
					write.CannotAfford();
				}
			}
			else
			{
				write.NotInStock();
			}
		}
		private Customer NewCustomer()
		{
			CustomerBuilder cb = new CustomerBuilder();

			write.WriteUsername();
			string newUsername = Console.ReadLine();
			foreach (Customer customer in customers)
			{
				if (customer.Username.Equals(username))
				{
					write.UserNameAlreadyExists();
					break;
				}
			}

			cb.Username(newUsername);
			write.NewCustomerChoice("a Password");
			cb.Password(SetChoiceYesOrNo());
			write.NewCustomerChoice("a fist name");
			cb.FirstName(SetChoiceYesOrNo());
			write.NewCustomerChoice("a last name");
			cb.LastName(SetChoiceYesOrNo());
			write.NewCustomerChoice("a email");
			cb.Email(SetChoiceYesOrNo());
			write.NewCustomerChoice("a age");
			try { cb.Age(Convert.ToInt32(SetChoiceYesOrNo())); }
			catch { write.InvalidAge(); }
			write.NewCustomerChoice("a adress");
			cb.Address(SetChoiceYesOrNo());
			write.NewCustomerChoice("a phone number");
			cb.Phone(SetChoiceYesOrNo());
			return cb.Build();
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
				write.YOrNPlease();
			}
		}
		private string SetText()
		{
			string input;
			while (true)
			{
				write.WriteInput();
				input = Console.ReadLine();
				if (input.Equals(""))
				{
					write.PleaseWriteSomething();
				}
				else
					break;
			}
			return input;
		}
		public string SetCurrentCustomer()
		{
			string option;
			if (!IsSomeoneLoggedIn())
			{
				option = "Login";
			}
			else
			{
				option = "Logout";
			}
			return option;
		}
		public void WritePowerDown()
		{
			write.PowerDown();
		}
		private void LogOut()
		{
			write.LoggingOut(CurrentCustomer);
			CurrentCustomer = null;
		}
		private void WriteMenuFromOptionContext()
		{
			menuContext.WriteOptionMenu();
		}
		private void SetContextToMain()
		{
			ResetCurrentChoice();
			menuContext = new MenuContext(new MainMenuState(this));
			previousMenuContext = menuContext;
		}
		private void SetContextToWareMenu()
		{
			ResetCurrentChoice();
			menuContext = new MenuContext(new WareMenuState(this));
			previousMenuContext = new MenuContext(new MainMenuState(this));
		}
		private void SetContextToPurchaseMenu()
		{
			ResetCurrentChoice();
			menuContext = new MenuContext(new PurchaseMenuState(this));
			previousMenuContext = new MenuContext(new WareMenuState(this));
		}
		private void SetContextToCustomerMenu()
		{
			ResetCurrentChoice();
			menuContext = new MenuContext(new CustomerMenuState(this));
			previousMenuContext = new MenuContext(new MainMenuState(this));
		}
		private void SetContextToLoginMenu()
		{
			ResetCurrentChoice();
			menuContext = new MenuContext(new LoginMenuState(this));
			previousMenuContext = new MenuContext(new MainMenuState(this));
		}
		private void SetContextToSortMenu()
		{
			ResetCurrentChoice();
			menuContext = new MenuContext(new SortMenuState(this));
			previousMenuContext = new MenuContext(new WareMenuState(this));
		}
		public void SetToPreviousContext()
		{
			ResetCurrentChoice();
			menuContext = previousMenuContext;
			previousMenuContext = new MenuContext(new MainMenuState(this));
		}
		public void SetOptionContext()
		{
			menuContext.SetOptionContext();
		}
		private void ResetCurrentChoice()
		{
			currentChoice = 1;
		}
	}
}

