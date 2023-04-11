using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.MenuStates
{
    public class MenuDelegateContext
    {
        AbstractDelegateState state;
        public MenuDelegateContext(AbstractDelegateState state)
        {
            this.state = state;
        }

        public void Request()
        {
            state.request();
        }
    }
}
