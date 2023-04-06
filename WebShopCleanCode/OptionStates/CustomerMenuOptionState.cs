using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopCleanCode.MenuStates;

namespace WebShopCleanCode.OptionStates
{
	internal class CustomerMenuOptionState : AbstractOptionState
	{

		public CustomerMenuOptionState(WebShop webShop) 
		{
			Option1 = "See your orders";
			Option2 = "Set your info";
			Option3 = "Add funds";
			Option4 = "";
			AmountOfOptions = 3;
			CurrentChoice = webShop.currentChoice;
			Info = "What would you like to do?";
			MenuContext = new MenuContext(new CustomerMenuState(webShop));
			WebShop = webShop;
		}
		public override void SetOptionContext()
		{
			MenuContext.Request();
		}

		public override void WriteOptionMenu()
		{
			Write = new Write();
			Write.WriteOptionMenu(this);
		}
	}
}
