using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopCleanCode.MenuStates;

namespace WebShopCleanCode.OptionStates
{
	internal class MainMenuOptionState : AbstractOptionState
	{
		public MainMenuOptionState(WebShop webShop) 
		{
			Option1 = "See Wares";
			Option2 = "Customer Info";
			Option3 = webShop.SetCurrentCustomer();
			Info = "What would you like to do?";
			CurrentChoice = 1;
			AmountOfOptions = 3;
			MenuContext = new MenuDelegateContext(new MainMenuDelegateState(webShop));
			PreviousContext = MenuContext;
			WebShop = webShop;
		}
		public override void WriteOptionMenu()
		{
			CurrentChoice = WebShop.currentChoice;
			Write = new Write();
			Write.WriteOptionMenu(this);
		}
		public override void SetOptionContext()
		{
			MenuContext.Request();
		}
	}
}
