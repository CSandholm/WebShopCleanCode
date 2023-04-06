using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode
{
	public abstract class IState //Change name if using abstract class and not interface
	{
		public delegate void RequestHandle();
		public RequestHandle request;
	}
}
