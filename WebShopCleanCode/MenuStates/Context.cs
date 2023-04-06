using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.MenuStates
{
    public class Context
    {
        IState state;
        public Context(IState state)
        {
            this.state = state;
        }

        public void Request()
        {
            state.request();
        }
    }
}
