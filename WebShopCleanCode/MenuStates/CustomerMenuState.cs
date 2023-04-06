﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.MenuStates
{
	public class CustomerMenuState : AbstractState
	{
		public CustomerMenuState(WebShop webShop)
		{
			request = () => { webShop.CustomerMenu(); };
		}
	}
}