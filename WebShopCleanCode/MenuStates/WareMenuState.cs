using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopCleanCode.MenuStates;

namespace WebShopCleanCode.OptionStates
{
	internal class WareMenuState : AbstractMenuState
	{
		public WareMenuState(WebShop webShop) 
		{
			Option1 = "See all wares";
			Option2 = "Purchase a ware";
			Option3 = "Sort wares";
			Option4 = webShop.SetCurrentCustomer();
			AmountOfOptions = 4;
			CurrentChoice = 1;
			Info = "What would you like to do?";
			MenuContext = new MenuDelegateContext(new WareMenuDelegateState(webShop));
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
