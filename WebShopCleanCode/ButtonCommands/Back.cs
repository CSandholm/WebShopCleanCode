using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopCleanCode.Commands;

namespace WebShopCleanCode.ButtonCommands
{
	public class Back : ICommando
	{
		public WebShop Execute(WebShop webShop)
		{
			webShop.SetToPreviousContext();
			return webShop;
		}
	}
}
