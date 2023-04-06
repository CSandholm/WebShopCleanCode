using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.MenuStates
{
	internal class PurchaseMenuState : AbstractMenuState
	{
		public PurchaseMenuState(WebShop webShop)
		{
			request = () => { webShop.PurchaseMenu(); };
		}
	}
}
