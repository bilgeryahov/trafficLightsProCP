using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    public class ReversableAction
    {
        public ReversableAction Reverse { get; private set; }
    }

    class ReversableAction<T> : ReversableAction where T : ReversableAction
    {
        new T Reverse { get { return base.Reverse as T; } }
    }

}

