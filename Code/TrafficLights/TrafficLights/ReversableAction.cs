using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    public abstract class ReversableAction
    {
        public abstract ReversableAction Reverse { get; }

        public abstract void Apply();
    }

    public abstract class ReversableAction<T> : ReversableAction where T : ReversableAction
    {
        public sealed override ReversableAction Reverse
        {
            get { return ReverseAction; }
        }

        public abstract T ReverseAction { get; }

        //new T Reverse { get { return base.Reverse as T; } }
    }

}

