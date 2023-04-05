using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode
{
	internal class BubbleSort
	{
		public List<Product> Run(string variable, bool ascending, List<Product> products)
		{
			if (variable.Equals("name"))
			{
				int length = products.Count;
				for (int i = 0; i < length - 1; i++)
				{
					bool sorted = true;
					int length2 = length - i;
					for (int j = 0; j < length2 - 1; j++)
					{
						if (ascending)
						{
							if (products[j].Name.CompareTo(products[j + 1].Name) < 0)
							{
								Product temp = products[j];
								products[j] = products[j + 1];
								products[j + 1] = temp;
								sorted = false;
							}
						}
						else
						{
							if (products[j].Name.CompareTo(products[j + 1].Name) > 0)
							{
								Product temp = products[j];
								products[j] = products[j + 1];
								products[j + 1] = temp;
								sorted = false;
							}
						}
					}
					if (sorted == true)
					{
						break;
					}
				}
			}
			else if (variable.Equals("price"))
			{
				int length = products.Count;
				for (int i = 0; i < length - 1; i++)
				{
					bool sorted = true;
					int length2 = length - i;
					for (int j = 0; j < length2 - 1; j++)
					{
						if (ascending)
						{
							if (products[j].Price > products[j + 1].Price)
							{
								Product temp = products[j];
								products[j] = products[j + 1];
								products[j + 1] = temp;
								sorted = false;
							}
						}
						else
						{
							if (products[j].Price < products[j + 1].Price)
							{
								Product temp = products[j];
								products[j] = products[j + 1];
								products[j + 1] = temp;
								sorted = false;
							}
						}
					}
					if (sorted == true)
					{
						break;
					}
				}
			}
			return products;
		}
	}
}
