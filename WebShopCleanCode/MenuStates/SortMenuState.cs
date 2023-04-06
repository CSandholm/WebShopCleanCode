using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.MenuStates
{
	internal class SortMenuState : AbstractMenuState
	{
		public SortMenuState(WebShop webShop) 
		{ 
			request = () => { webShop.SortMenu(); };
		}
	}
}
