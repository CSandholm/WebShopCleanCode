using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopCleanCode.MenuStates;

namespace WebShopCleanCode.OptionStates
{
	internal class PurchaseMenuOptionState : AbstractOptionState
	{
		public PurchaseMenuOptionState(WebShop webShop) 
		{
			Option1 = "Sort by name, descending";
			Option2 = "Sort by name, ascending";
			Option3 = "Sort by price, descending";
			Option4 = "Sort by price, ascending";
			Info = "How would you like to sort them?";
			CurrentChoice = webShop.currentChoice;
			AmountOfOptions = 4;
			MenuContext = new MenuContext(new SortMenuState(webShop));
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
