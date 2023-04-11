using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.MenuStates
{
	internal class WareMenuDelegateState : AbstractDelegateState
	{
		public WareMenuDelegateState(WebShop webShop) 
		{
			request = () => { webShop.WaresMenu(); };
		}
	}
}
