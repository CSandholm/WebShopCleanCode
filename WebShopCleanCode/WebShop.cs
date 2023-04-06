using Microsoft.VisualBasic;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebShopCleanCode.MenuStates;
using WebShopCleanCode.OptionStates;
using static System.Net.Mime.MediaTypeNames;

namespace WebShopCleanCode
{
    public class WebShop
	{
		Database database = new Database();
		List<ProductProxy> productProxies;
		List<Customer> customers = new List<Customer>();
		BubbleSort sort = new BubbleSort();
		Customer currentCustomer = null;
		MenuContext previousMenuContext; // Save the previous context to be able to go back?
		OptionContext optionContext;
		Write write = new Write();

		//Dictionary with commands or delegates for choices

		//Make properties? Or make into object?
		string currentMenu;
		public int currentChoice = 1;
		int amountOfOptions;
		string option1;
		string option2;
		string option3;
		string option4;
		string info;
		public string Info { get { return info; } set { info = value; } }
		public string Option1 { get{ return option1; } set { option1 = value; } }
		public string Option2 { get{ return option2; } set { option2 = value; } }
		public string Option3 { get{ return option3; } set { option3 = value; } }
		public string Option4 { get{ return option4; } set { option4 = value; } }
		public int AmountOfOptions { get { return amountOfOptions; } set {; } }
		public int CurrentCostumerFunds { get { return CurrentCustomer.Funds; } set { CurrentCustomer.Funds = value; } }
		public Customer CurrentCustomer { get => currentCustomer; set => currentCustomer = value; }

		string username = null;
		string password = null;
		public WebShop()
		{
			productProxies = database.GetProductProxies();
			customers = database.GetCustomers();
			optionContext = new OptionContext(new MainMenuOptionState(this));
		}
		public void Run()
		{
			while (true)
			{
				WriteMenuFromOptionContext();
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
						if (currentChoice < optionContext.amountOfOptions)
						{
							currentChoice++;
						}
						break;
					case "ok":
					case "k":
					case "o":
						optionContext.SetOptionContext();
						break;
					case "back":
					case "b":
						if (currentMenu.Equals("main menu"))
						{
							write.AlreadyInMainMenu();
						}
						else if (currentMenu.Equals("purchase menu"))
						{
							ResetCurrentChoice();
							optionContext = new OptionContext(new WareMenuOptionState(this));
							//SetWareMenuOptions();
						}
						else
						{
							ResetCurrentChoice();
							optionContext = new OptionContext(new MainMenuOptionState(this));
							//SetMainMenuOptions();
						}
						break;
					case "quit":
					case "q":
						write.PowerDown();
						return;
					default:
						write.NotAnOption();
						break;
				}
			}
		}
		private void WriteMenuFromOptionContext()
		{
			optionContext.WriteOptionMenu();
		}
		public void MainMenu()
		{
			switch (currentChoice)
			{
				case 1:
					ResetCurrentChoice();
					optionContext = new OptionContext(new WareMenuOptionState(this));
					//SetWareMenuOptions();
					break;
				case 2:
					if (CurrentCustomer != null)
					{
						ResetCurrentChoice();
						optionContext = new OptionContext(new CustomerMenuOptionState(this));
						//SetCustomerMenuOptions();
					}
					else
					{
						write.NobodyLoggedIn();
					}
					break;
				case 3:
					if (CurrentCustomer == null)
					{
						ResetCurrentChoice();
						optionContext = new OptionContext(new LoginMenuOptionState(this));
						//SetLoginMenuOptions();
						username = null;
						password = null;
					}
					else
					{
						option3 = "Login";
						write.LoggingOut(CurrentCustomer);
						ResetCurrentChoice();
						CurrentCustomer = null;
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
					break;
				default:
					write.NotAnOption();
					break;
			}
		}
		public void SortMenu()
		{
			bool back = true;
			switch (currentChoice)
			{
				case 1:
					productProxies = sort.Run("name", false);
					write.WaresSorted();
					break;
				case 2:
					productProxies = sort.Run("name", true);
					write.WaresSorted();
					break;
				case 3:
					productProxies = sort.Run("price", false);
					write.WaresSorted();
					break;
				case 4:
					productProxies = sort.Run("price", true);
					write.WaresSorted();
					break;
				default:
					back = false;
					write.NotAnOption();
					break;
			}
			if (back)
			{
				ResetCurrentChoice();
				optionContext = new OptionContext(new WareMenuOptionState(this));
				//SetWareMenuOptions();
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
					if (CurrentCustomer != null)
					{
						ResetCurrentChoice();
						optionContext = new OptionContext(new PurchaseMenuOptionState(this));
						//SetPurchaseMenuOptions();
					}
					else
					{
						write.ErrorLoginToPurchaseWare();
						ResetCurrentChoice();
					}
					break;
				case 3:
					ResetCurrentChoice();
					optionContext = new OptionContext(new SortMenuOptionState(this));
					//SetSortMenuOptions();
					break;
				case 4:
					if (CurrentCustomer == null)
					{
						ResetCurrentChoice();
						optionContext = new OptionContext(new LoginMenuOptionState(this));
						//SetLoginMenuOptions();
					}
					else
					{
						option4 = "Login";
						write.LoggingOut(CurrentCustomer);
						CurrentCustomer = null;
						ResetCurrentChoice();
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
								ResetCurrentChoice();
								optionContext = new OptionContext(new MainMenuOptionState(this));
								//SetMainMenuOptions();
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
					ResetCurrentChoice();
					optionContext = new OptionContext(new MainMenuOptionState(this));
					//SetMainMenuOptions();
					break;
				default:
					write.NotAnOption();
					break;
			}
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
			ResetCurrentChoice();
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
			// Would have liked to be able to quit at any time in here.

			write.NewCustomerChoice("Password");
			cb.Password(SetChoiceYesOrNo());
			write.NewCustomerChoice("fist name");
			cb.FirstName(SetChoiceYesOrNo());
			write.NewCustomerChoice("last name");
			cb.LastName(SetChoiceYesOrNo());
			write.NewCustomerChoice("email");
			cb.Email(SetChoiceYesOrNo());
			write.NewCustomerChoice("age");
			try { cb.Age(Convert.ToInt32(SetChoiceYesOrNo()));}
			catch { write.InvalidAge(); }
			write.NewCustomerChoice("adress");
			cb.Address(SetChoiceYesOrNo());
			write.NewCustomerChoice("phone number");
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
			if (CurrentCustomer == null)
			{
				option = "Login";
			}
			else
			{
				option = "Logout";
			}
			return option;
		}
		
		private void ResetCurrentChoice()
		{
			currentChoice = 1;
		}
	}
}

