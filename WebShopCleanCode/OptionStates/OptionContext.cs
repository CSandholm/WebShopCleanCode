using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopCleanCode.MenuStates;

namespace WebShopCleanCode.OptionStates
{
	internal class OptionContext
	{
		AbstractOptionState state;
		public int amountOfOptions;
		public OptionContext(AbstractOptionState state)
		{
			this.state = state;
			amountOfOptions = state.AmountOfOptions;
		}

		public void WriteOptionMenu()
		{
			state.WriteOptionMenu();
		}

		public void SetOptionContext()
		{
			state.SetOptionContext();
		}
	}
}
