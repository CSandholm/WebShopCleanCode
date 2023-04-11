using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopCleanCode.Commands;

namespace WebShopCleanCode.ButtonCommands
{
	public class ReturnButtonDictionary
	{
		public Dictionary<string, MyButton> GetButtons()
		{
			MyButton quit = new MyButton(new Quit());
			MyButton back = new MyButton(new Back());
			MyButton add = new MyButton(new Add());
			MyButton accept = new MyButton(new Accept());
			MyButton subtract = new MyButton(new Subtract());

			Dictionary<string, MyButton> buttons = new Dictionary<string, MyButton>();
			buttons.Add("l", subtract);
			buttons.Add("left", subtract);
			buttons.Add("r", add);
			buttons.Add("right", add);
			buttons.Add("q", quit);
			buttons.Add("quit", quit);
			buttons.Add("o", accept);
			buttons.Add("k", accept);
			buttons.Add("ok", accept);
			buttons.Add("b", back);
			buttons.Add("back", back);

			return buttons;
		}
	}
}
