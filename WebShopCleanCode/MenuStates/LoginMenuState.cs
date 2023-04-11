using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopCleanCode.MenuStates;

namespace WebShopCleanCode.OptionStates
{
	internal class LoginMenuOptionState : AbstractMenuState
	{
		public LoginMenuOptionState(WebShop webShop) 
		{
			Option1 = "Set Username";
			Option2 = "Set Password";
			Option3 = "Login";
			Option4 = "Register";
			AmountOfOptions = 4;
			Info = "Please submit username and password.";
			CurrentChoice = 1;
			MenuContext = new MenuDelegateContext(new LoginDelegateState(webShop));
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
