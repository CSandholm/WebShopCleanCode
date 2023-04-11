using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopCleanCode.Commands;
using WebShopCleanCode.OptionStates;

namespace WebShopCleanCode.ButtonCommands
{
	public class Accept : ICommando
	{
		public WebShop Execute(WebShop webShop)
		{
			webShop.SetPreviousContext();
			webShop.SetOptionContext();
			return webShop;
		}
	}
}
