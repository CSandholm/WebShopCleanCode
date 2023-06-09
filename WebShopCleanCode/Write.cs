﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopCleanCode.OptionStates;

namespace WebShopCleanCode
{
	internal class Write
	{
		public void AlreadyInMainMenu()
		{
			Console.WriteLine();
			Console.WriteLine("You're already on the main menu.");
			Console.WriteLine();
		}
		public void PowerDown()
		{
			Console.WriteLine("The console powers down. You are free to leave.");
		}
		public void Welcome()
		{
			Console.WriteLine("Welcome to the WebShop!");
		}
		public void Info(string info)
		{
			Console.WriteLine(info);
		}
		public void Options(AbstractMenuState options)
		{
			Console.WriteLine("1: " + options.Option1);
			Console.WriteLine("2: " + options.Option2);
			if (options.AmountOfOptions > 2)
			{
				Console.WriteLine("3: " + options.Option3);
			}
			if (options.AmountOfOptions > 3)
			{
				Console.WriteLine("4: " + options.Option4);
			}
		}
		public void NotAnOption()
		{
			Console.WriteLine();
			Console.WriteLine("Not an option.");
			Console.WriteLine();
		}
		public void WaresSorted()
		{
			Console.WriteLine();
			Console.WriteLine("Wares sorted.");
			Console.WriteLine();
		}
		public void NobodyLoggedIn()
		{
			Console.WriteLine();
			Console.WriteLine("Nobody is logged in.");
			Console.WriteLine();
		}
		public void LoggingOut(Customer cusomter)
		{
			Console.WriteLine();
			Console.WriteLine(cusomter.Username + " logged out.");
			Console.WriteLine();
		}
		public void LoggedIn(Customer customer)
		{
			Console.WriteLine();
			Console.WriteLine(customer.Username + " logged in.");
			Console.WriteLine();
		}
		public void FundToAdd()
		{
			Console.WriteLine("How many funds would you like to add?");
		}
		public void ErrorNegativeAmount()
		{
			Console.WriteLine();
			Console.WriteLine("Don't add negative amounts.");
			Console.WriteLine();
		}
		public void AmountAdded(int amount)
		{
			Console.WriteLine();
			Console.WriteLine(amount + " added to your profile.");
			Console.WriteLine();
		}
		public void ErrorLoginToPurchaseWare()
		{
			Console.WriteLine();
			Console.WriteLine("You must be logged in to purchase wares.");
			Console.WriteLine();
		}
		public void KeyboardAppears()
		{
			Console.WriteLine("A keyboard appears.");
		}
		public void InputUsername()
		{
			Console.WriteLine("Please input your username.");
		}
		public void InputPassword()
		{
			Console.WriteLine("Please input your password.");
		}
		public void IncompleteData()
		{
			Console.WriteLine();
			Console.WriteLine("Incomplete data.");
			Console.WriteLine();
		}
		public void InvalidCredentials()
		{
			Console.WriteLine();
			Console.WriteLine("Invalid credentials.");
			Console.WriteLine();
		}
		public void AddedCustomer(Customer customer)
		{
			Console.WriteLine();
			Console.WriteLine(customer.Username + " successfully added and is now logged in.");
			Console.WriteLine();
		}
		public void SuccefullyBoughtItem(Product product)
		{
			Console.WriteLine();
			Console.WriteLine("Successfully bought " + product.Name);
			Console.WriteLine();
		}
		public void CannotAfford()
		{
			Console.WriteLine();
			Console.WriteLine("You cannot afford.");
			Console.WriteLine();
		}
		public void NotInStock()
		{
			Console.WriteLine();
			Console.WriteLine("Not in stock.");
			Console.WriteLine();
		}
		public void WriteUsername()
		{
			Console.WriteLine("Please write your username.");
		}
		public void UserNameAlreadyExists()
		{
			Console.WriteLine();
			Console.WriteLine("Username already exists.");
			Console.WriteLine();
		}
		public void NewCustomerChoice(string current)
		{
			Console.WriteLine(@"Do you want {0}? y/n", current);
		}
		public void InvalidAge()
		{
			Console.WriteLine("Invalid input for age! Age set to 0.");
		}
		public void YOrNPlease()
		{
			Console.WriteLine();
			Console.WriteLine("y or n, please.");
			Console.WriteLine();
		}
		public void WriteInput()
		{
			Console.WriteLine("Please write your input.");
		}
		public void PleaseWriteSomething()
		{
			Console.WriteLine();
			Console.WriteLine("Please actually write something.");
			Console.WriteLine();
		}
		public void WriteEmptyLine()
		{
			Console.WriteLine();
		}
		public void WriteOptionMenu(AbstractMenuState state)
		{
			Welcome();
			Info(state.Info);
			Options(state);
			state.CurrentChoice = state.CurrentChoice;

			for (int i = 0; i < state.AmountOfOptions; i++)
			{
				Console.Write(i + 1 + "\t");
			}
			Console.WriteLine();
			for (int i = 1; i < state.CurrentChoice; i++)
			{
				Console.Write("\t");
			}
			Console.WriteLine("|");

			Console.WriteLine("Your buttons are Left, Right, OK, Back and Quit.");
			if (state.WebShop.CurrentCustomer != null)
			{
				Console.WriteLine("Current user: " + state.WebShop.CurrentCustomer.Username);
			}
			else
			{
				Console.WriteLine("Nobody logged in.");
			}
		}
		public void WritePurchaseOptionMenu(AbstractMenuState state)
		{
			Welcome();
			Info(state.Info);
			state.CurrentChoice = state.CurrentChoice;

			foreach (ProductProxy product in state.WebShop.productProxies)
			{
				Console.Write(state.WebShop.productProxies.IndexOf(product) + 1 + ": ");
				product.PrintInfo();
			}
			Console.WriteLine();

			for (int i = 0; i < state.AmountOfOptions; i++)
			{
				Console.Write(i + 1 + "\t");
			}
			Console.WriteLine();

			for (int i = 1; i < state.CurrentChoice; i++)
			{
				Console.Write("\t");
			}
			Console.WriteLine("|");

			Console.WriteLine("Your buttons are Left, Right, OK, Back and Quit.");
			if (state.WebShop.CurrentCustomer != null)
			{
				Console.WriteLine("Current user: " + state.WebShop.CurrentCustomer.Username);
			}
			else
			{
				Console.WriteLine("Nobody logged in.");
			}
		}
	}
}

