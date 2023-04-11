using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopCleanCode.MenuStates;

namespace WebShopCleanCode.OptionStates
{
	internal class SortMenuOptionState : AbstractMenuState
	{
		public SortMenuOptionState(WebShop webShop) 
		{
			Option1 = "Sort by name, descending";
			Option2 = "Sort by name, ascending";
			Option3 = "Sort by price, descending";
			Option4 = "Sort by price, ascending";
			CurrentChoice = 1;
			Info = "How would you like to sort them?";
			AmountOfOptions = 4;
			MenuContext = new MenuDelegateContext(new SortMenuDelegateState(webShop));
			//PreviousContext = new MenuContext(new WareMenuState(webShop));
			WebShop = webShop;
		}
		public override void SetOptionContext()
		{
			MenuContext.Request();
		}

		public override void WriteOptionMenu()
		{
			CurrentChoice = WebShop.currentChoice;
			Write = new Write();
			Write.WriteOptionMenu(this);
		}
	}
}
