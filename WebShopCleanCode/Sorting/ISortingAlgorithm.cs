using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.Sorting
{
	internal interface ISortingAlgorithm
	{
		public abstract void Sort();

		public void Sort(int[] array, int indexFrom, int indexTo)
		{
			int temp = array[indexTo];
			array[indexTo] = array[indexFrom];
			array[indexFrom] = temp;
		}
	}
}
