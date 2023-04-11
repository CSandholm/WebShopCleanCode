using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.MenuStates
{
    public class MainMenuDelegateState : AbstractDelegateState
    {
        public MainMenuDelegateState(WebShop webShop)
        {
            request = () => { webShop.MainMenu(); };
        }
    }
}
