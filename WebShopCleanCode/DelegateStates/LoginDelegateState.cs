using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.MenuStates
{
	internal class LoginDelegateState : AbstractDelegateState
	{
		public LoginDelegateState(WebShop webShop)
		{
			request = () => { webShop.LoginMenu(); };
		}
	}
}
