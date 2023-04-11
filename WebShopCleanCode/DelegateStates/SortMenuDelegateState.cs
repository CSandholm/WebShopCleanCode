using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.MenuStates
{
	internal class SortMenuDelegateState : AbstractDelegateState
	{
		public SortMenuDelegateState(WebShop webShop) 
		{ 
			request = () => { webShop.SortMenu(); };
		}
	}
}
