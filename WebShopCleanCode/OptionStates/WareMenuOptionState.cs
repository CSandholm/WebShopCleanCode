using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopCleanCode.MenuStates;

namespace WebShopCleanCode.OptionStates
{
	internal class WareMenuOptionState : AbstractOptionState
	{
		public WareMenuOptionState(WebShop webShop) 
		{
			Option1 = "See all wares";
			Option2 = "Purchase a ware";
			Option3 = "Sort wares";
			Option4 = webShop.SetCurrentCustomer();
			AmountOfOptions = 4;
			CurrentChoice = webShop.currentChoice;
			Info = "What would you like to do?";
			MenuContext = new MenuContext(new WareMenuState(webShop));
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
