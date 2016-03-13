using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    public class UndoableAction:ReversableAction<RedoableAction>
    {
    }
}
