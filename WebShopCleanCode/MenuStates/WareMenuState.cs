using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.MenuStates
{
	internal class WareMenuState : AbstractState
	{
		public WareMenuState(WebShop webShop) 
		{
			request = () => { webShop.WaresMenu(); };
		}
	}
}
