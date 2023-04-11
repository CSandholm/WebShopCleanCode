using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.MenuStates
{
	internal class LoginMenuState : AbstractMenuState
	{
		public LoginMenuState(WebShop webShop)
		{
			request = () => { webShop.LoginMenu(); };
		}
	}
}
