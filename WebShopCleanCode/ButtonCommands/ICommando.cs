using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.Commands
{
	public interface ICommando
	{
		public WebShop Execute(WebShop webShop);
	}
}
