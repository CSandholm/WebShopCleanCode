using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.MenuStates
{
    public class Context
    {
        AbstractState state;
        public Context(AbstractState state)
        {
            this.state = state;
        }

        public void Request()
        {
            state.request();
        }
    }
}
