using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode
{
	internal class ProductProxy
	{
		public string Name { get; set; }
		private Product product;
		Database db;

		public ProductProxy(string username, Database db)
		{
			Name = username;
			this.db = db;
		}
	}
}
