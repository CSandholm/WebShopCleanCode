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
		public List<ProductProxy> productProxies;
		List<Customer> customers = new List<Customer>();
		Customer currentCustomer = null;
		OptionContext previousOptionContext;
		public OptionContext optionContext;
		Write write = new Write();
		Dictionary<string, MyButton> buttons;
		public bool running;

		//Dictionary with commands or delegates for choices

		public int currentChoice = 1;
		public int CurrentCostumerFunds { get { return CurrentCustomer.Funds; } set { CurrentCustomer.Funds = value; } }
		public Customer CurrentCustomer { get => currentCustomer; set => currentCustomer = value; }

		string username = null;
		string password = null;
		public WebShop()
		{
			productProxies = database.GetProductProxies();
			customers = database.GetCustomers();
			optionContext = new OptionContext(new MainMenuOptionState(this));
			running = true;
			buttons = new ReturnButtonDictionary().GetButtons();
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

				/*
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
						SetPreviousContext();
						SetOptionContext();
						break;
					case "back":
					case "b":
						optionContext = previousOptionContext;
						previousOptionContext = new OptionContext(new MainMenuOptionState(this));
						break;
					case "quit":
					case "q":
						write.PowerDown();
						return;
					default:
						write.NotAnOption();
						break;
				}
				*/
			}
		}
		public void MainMenu()
		{
			switch (currentChoice)
			{
				case 1:
					ResetCurrentChoice();
					optionContext = new OptionContext(new WareMenuOptionState(this));
					break;
				case 2:
					if (CurrentCustomer != null)
					{
						ResetCurrentChoice();
						optionContext = new OptionContext(new CustomerMenuOptionState(this));
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
						username = null;
						password = null;
					}
					else
					{
						LogOut();
						optionContext = new OptionContext(new MainMenuOptionState(this));
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
			BubbleSort sort = new BubbleSort();
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
				ResetCurrentChoice();
				optionContext = new OptionContext(new WareMenuOptionState(this));
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
					break;
				case 4:
					if (CurrentCustomer == null)
					{
						ResetCurrentChoice();
						optionContext = new OptionContext(new LoginMenuOptionState(this));
					}
					else
					{
						LogOut();
						optionContext = new OptionContext(new WareMenuOptionState(this));
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

		public void SetPreviousContext()
		{
			if(previousOptionContext != optionContext)
			{
				previousOptionContext = optionContext;
			}
		}
		private void LogOut()
		{
			write.LoggingOut(CurrentCustomer);
			CurrentCustomer = null;
			ResetCurrentChoice();
		}
		public void SetOptionToPreviousContext()
		{
			optionContext = previousOptionContext;
			previousOptionContext = new OptionContext(new MainMenuOptionState(this));
		}
		public void SetOptionContext()
		{
			optionContext.SetOptionContext();
		}
		private void WriteMenuFromOptionContext()
		{
			optionContext.WriteOptionMenu();
		}
		public void WritePowerDown()
		{
			write.PowerDown();
		}
	}
}

