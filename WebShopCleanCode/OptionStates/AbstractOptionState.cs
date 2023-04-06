using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopCleanCode.MenuStates;

namespace WebShopCleanCode.OptionStates
{
	public abstract class AbstractOptionState
	{
		int currentChoice;
		int amountOfOptions;
		string option1;
		string option2;
		string option3;
		string option4;
		string info;
		MenuContext menuContext;
		WebShop webShop;
		Write write;

		public int CurrentChoice { get => currentChoice; set => currentChoice = value; }
		public int AmountOfOptions { get => amountOfOptions; set => amountOfOptions = value; }
		public string Option1 { get => option1; set => option1 = value; }
		public string Option2 { get => option2; set => option2 = value; }
		public string Option3 { get => option3; set => option3 = value; }
		public string Option4 { get => option4; set => option4 = value; }
		public string Info { get => info; set => info = value; }
		public MenuContext MenuContext { get => menuContext; set => menuContext = value; }
		public WebShop WebShop { get => webShop; set => webShop = value; }
		internal Write Write { get => write; set => write = value; }

		public abstract void WriteOptionMenu();
		public abstract void SetOptionContext();
	}
}
