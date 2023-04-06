using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode
{
	public class ProductProxy
	{
		public string Name { get; set; }
		private Product product;
		Database db;

		//With a proxy design patters we can avoid memory overflow with unwanted loading of objects we're not going
		//to use. A proxy object is created that is used to find the actual object in the database. However,
		//it is null until we actually use it.

		public ProductProxy(string username, Database db)
		{
			Name = username;
			this.db = db;
		}

		//Load object from database
		public void Load()
		{
			if(product == null)
			{
				Console.WriteLine("Loading: " + Name);
				product = db.GetProductByName(Name);
			}
		}

		public void PrintInfo()
		{
			Load();
			product.PrintInfo();
		}
	}
}
