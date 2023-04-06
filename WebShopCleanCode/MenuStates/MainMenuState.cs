using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.MenuStates
{
    public class MainMenuState : AbstractMenuState
    {
        public MainMenuState(WebShop webShop)
        {
            request = () => { webShop.MainMenu(); };
        }
    }
}
