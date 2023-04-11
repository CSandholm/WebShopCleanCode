using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.Commands
{
	public class MyButton
	{
		ICommando command;
		public MyButton(ICommando command)
		{
			this.command = command;
		}

		public void PressButton(WebShop webShop)
		{
			command.Execute(webShop);
		}
	}
}
