using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.Sorting
{
	internal class ShellSort
	{
		public List<ProductProxy> Sort(string variable, bool ascending, Database db, List<ProductProxy> list)
		{
			List<Product> products = new List<Product>();
			foreach (ProductProxy proxy in list)
			{
				proxy.Load();
				products.Add(proxy.product);
			}

			int size = products.Count;

			if (variable.Equals("name"))
			{
				if (ascending)
				{
					for (int interval = size / 2; interval > 0; interval /= 2)
					{
						for (int i = interval; i < size; i++)
						{
							var currentKey = products[i];
							int j = i;

							while ((j >= interval) && currentKey.Name.CompareTo(products[j - interval].Name) < 0)
							{
								products[j] = products[j - interval];
								j -= interval;
							}
							products[j] = currentKey;
						}
					}
				}
				else
				{
					for (int interval = size / 2; interval > 0; interval /= 2)
					{
						for (int i = interval; i < size; i++)
						{
							var currentKey = products[i];
							int j = i;

							while ((j >= interval) && currentKey.Name.CompareTo(products[j - interval].Name) > 0)
							{
								products[j] = products[j - interval];
								j -= interval;
							}
							products[j] = currentKey;
						}
					}
				}
			}

			if (variable.Equals("price"))
			{
				if (ascending)
				{
					for (int interval = size / 2; interval > 0; interval /= 2)
					{
						for (int i = interval; i < size; i++)
						{
							var currentKey = products[i];
							int j = i;

							while ((j >= interval) && currentKey.Price.CompareTo(products[j - interval].Price) < 0)
							{
								products[j] = products[j - interval];
								j -= interval;
							}
							products[j] = currentKey;
						}
					}
				}
				else
				{
					for (int interval = size / 2; interval > 0; interval /= 2)
					{
						for (int i = interval; i < size; i++)
						{
							var currentKey = products[i];
							int j = i;

							while ((j >= interval) && currentKey.Price.CompareTo(products[j - interval].Price) > 0)
							{
								products[j] = products[j - interval];
								j -= interval;
							}
							products[j] = currentKey;
						}
					}
				}
			}

			List<ProductProxy> productProxies = new List<ProductProxy>();
			foreach (Product product in products)
			{
				productProxies.Add(new ProductProxy(product.Name, db));
			}
			return productProxies;
		}
	}
}
