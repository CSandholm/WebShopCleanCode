using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.MenuStates
{
	public class CustomerDelegateState : AbstractDelegateState
	{
		public CustomerDelegateState(WebShop webShop)
		{
			request = () => { webShop.CustomerMenu(); };
		}
	}
}