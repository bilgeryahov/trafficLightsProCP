using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary> Abstract class defining a reversable action</summary>
    public abstract class ReversableAction
    {
        public abstract ReversableAction Reverse { get; }

        public abstract void Apply();
    }

    /// <summary> Abstract class defining a reversable action that must have a speficic counter</summary>
    public abstract class ReversableAction<T> : ReversableAction where T : ReversableAction
    {
        public sealed override ReversableAction Reverse
        {
            get { return ReverseAction; }
        }

        public abstract T ReverseAction { get; }
    }

}

