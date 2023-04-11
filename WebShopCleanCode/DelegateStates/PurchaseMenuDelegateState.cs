using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.MenuStates
{
	internal class PurchaseMenuDelegateState : AbstractDelegateState
	{
		public PurchaseMenuDelegateState(WebShop webShop)
		{
			request = () => { webShop.PurchaseMenu(); };
		}
	}
}
