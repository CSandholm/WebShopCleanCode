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
			Info = "What would you like to purchase?";
			CurrentChoice = 1;
			AmountOfOptions = webShop.productProxies.Count;
			MenuContext = new MenuDelegateContext(new PurchaseMenuDelegateState(webShop));
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
			Write.WritePurchaseOptionMenu(this);
		}
	}
}
