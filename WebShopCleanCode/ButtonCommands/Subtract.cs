using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WebShopCleanCode.Commands;

namespace WebShopCleanCode.ButtonCommands
{
	public class Subtract : ICommando
	{
		public WebShop Execute(WebShop webShop)
		{
			if (webShop.currentChoice <= 1)
			{
				webShop.currentChoice = 1;
			}
			else
				webShop.currentChoice -= 1;

			return webShop;
		}
	}
}
