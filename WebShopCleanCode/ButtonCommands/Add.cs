﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopCleanCode.Commands;

namespace WebShopCleanCode.ButtonCommands
{
	public class Add : ICommando
	{
		public WebShop Execute(WebShop webShop)
		{
			if (webShop.currentChoice >= webShop.MenuContext.amountOfOptions)
			{
				webShop.currentChoice = webShop.MenuContext.amountOfOptions;
			}
			else
				webShop.currentChoice += 1;

			return webShop;
		}
	}
}
