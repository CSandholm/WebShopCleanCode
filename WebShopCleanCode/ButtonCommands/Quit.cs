using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopCleanCode.Commands;

namespace WebShopCleanCode.ButtonCommands
{
	public class Quit : ICommando
	{
		public WebShop Execute(WebShop webShop)
		{
			webShop.WritePowerDown();
			webShop.running = false;
			return webShop;
		}
	}
}
