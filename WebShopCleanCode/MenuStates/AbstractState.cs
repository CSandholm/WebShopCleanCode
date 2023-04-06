﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.MenuStates
{
    public abstract class AbstractState
    {
        public delegate void RequestHandle();
        public RequestHandle request;
    }
}
