using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopCleanCode.OptionStates;

namespace WebShopCleanCode
{
	public class SetMenuContext
	{
		public void SetContextToMain(WebShop webShop)
		{
			webShop.ResetCurrentChoice();
			webShop.MenuContext = new MenuContext(new MainMenuState(webShop));
			webShop.PreviousMenuContext = webShop.MenuContext;
		}
		public void SetContextToWareMenu(WebShop webShop)
		{
			webShop.ResetCurrentChoice();
			webShop.MenuContext = new MenuContext(new WareMenuState(webShop));
			webShop.PreviousMenuContext = new MenuContext(new MainMenuState(webShop));
		}
		public void SetContextToPurchaseMenu(WebShop webShop)
		{
			webShop.ResetCurrentChoice();
			webShop.MenuContext = new MenuContext(new PurchaseMenuState(webShop));
			webShop.PreviousMenuContext = new MenuContext(new WareMenuState(webShop));
		}
		public void SetContextToCustomerMenu(WebShop webShop)
		{
			webShop.ResetCurrentChoice();
			webShop.MenuContext = new MenuContext(new CustomerMenuState(webShop));
			webShop.PreviousMenuContext = new MenuContext(new MainMenuState(webShop));
		}
		public void SetContextToLoginMenu(WebShop webShop)
		{
			webShop.ResetCurrentChoice();
			webShop.MenuContext = new MenuContext(new LoginMenuState(webShop));
			webShop.PreviousMenuContext = new MenuContext(new MainMenuState(webShop));
		}
		public void SetContextToSortMenu(WebShop webShop)
		{
			webShop.ResetCurrentChoice();
			webShop.MenuContext = new MenuContext(new SortMenuState(webShop));
			webShop.PreviousMenuContext = new MenuContext(new WareMenuState(webShop));
		}
		public void SetToPreviousContext(WebShop webShop)
		{
			webShop.ResetCurrentChoice();
			webShop.MenuContext = webShop.PreviousMenuContext;
			webShop.PreviousMenuContext = new MenuContext(new MainMenuState(webShop));
		}
	}
}
