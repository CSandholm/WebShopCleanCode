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
using System.Reflection.Metadata.Ecma335;

namespace WebShopCleanCode
{
	public class WebShop
	{
		Database database = Database.getDbInstance();
		List<Customer> customers;
		Customer currentCustomer = null;
		Write write = new Write();
		Dictionary<string, MyButton> buttons;
		MenuContext previousMenuContext;
		MenuContext menuContext;
		SetMenuContext setMenuContext = new SetMenuContext();

		public List<ProductProxy> productProxies;
		public bool running;
		public int currentChoice = 1;
		string username = null;
		string password = null;

		public SetMenuContext SetMenuContext { get => setMenuContext; }
		public MenuContext MenuContext { get => menuContext; set => menuContext = value; }
		public MenuContext PreviousMenuContext { get => previousMenuContext; set => previousMenuContext = value; }
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
					setMenuContext.SetContextToWareMenu(this);
					break;
				case 2:
					TryAccessCustomerMenu();
					break;
				case 3:
					LogOutOrLogIn();
					break;
				default:
					write.NotAnOption();
					break;
			}
		}
		private void TryAccessCustomerMenu()
		{
			if (IsSomeoneLoggedIn())
			{
				setMenuContext.SetContextToCustomerMenu(this);
			}
			else
			{
				write.NobodyLoggedIn();
			}
		}
		private void LogOutOrLogIn()
		{
			if (!IsSomeoneLoggedIn())
			{
				setMenuContext.SetContextToLoginMenu(this);
				username = null;
				password = null;
			}
			else
			{
				LogOut();
				setMenuContext.SetContextToMain(this);
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
					AddFunds();
					break;
				default:
					write.NotAnOption();
					break;
			}
		}
		private void AddFunds()
		{
			write.FundToAdd();
			string amountString = Console.ReadLine();
			try
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
			catch
			{
				write.NotAnOption();
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
				setMenuContext.SetContextToWareMenu(this);
			}
		}
		public void WaresMenu()
		{
			switch (currentChoice)
			{
				case 1:
					PrintProducts();
					break;
				case 2:
					TryAccessPurchaseMenu();
					break;
				case 3:
					setMenuContext.SetContextToSortMenu(this);
					break;
				case 4:
					LogOutOrLogIn();
					break;
				case 5:
					break;
				default:
					write.NotAnOption();
					break;
			}
		}
		private void PrintProducts()
		{
			write.WriteEmptyLine();
			foreach (ProductProxy product in productProxies)
			{
				product.PrintInfo();
			}
			write.WriteEmptyLine();
		}
		private void TryAccessPurchaseMenu()
		{
			if (IsSomeoneLoggedIn())
			{
				setMenuContext.SetContextToPurchaseMenu(this);
			}
			else
			{
				write.ErrorLoginToPurchaseWare();
			}
		}
		public void LoginMenu()
		{
			switch (currentChoice)
			{
				case 1:
					UsernameInput();
					break;
				case 2:
					PasswordInput();
					break;
				case 3:
					TryLogIn();
					break;
				case 4:
					AddNewCustomer();
					setMenuContext.SetContextToMain(this);
					break;
				default:
					write.NotAnOption();
					break;
			}
		}
		private void TryLogIn()
		{
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
						setMenuContext.SetContextToMain(this);
						break;
					}
				}
				if (found == false)
				{
					write.InvalidCredentials();
				}
			}
		}
		private void PasswordInput()
		{
			write.KeyboardAppears();
			write.InputPassword();
			password = Console.ReadLine();
			write.WriteEmptyLine();
		}
		private void UsernameInput()
		{
			write.KeyboardAppears();
			write.InputUsername();
			username = Console.ReadLine();
			write.WriteEmptyLine();
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
		public void ResetCurrentChoice()
		{
			currentChoice = 1;
		}
		public void SetOptionContext()
		{
			menuContext.SetOptionContext();
		}
		private void WriteMenuFromOptionContext()
		{
			menuContext.WriteOptionMenu();
		}
	}
}

