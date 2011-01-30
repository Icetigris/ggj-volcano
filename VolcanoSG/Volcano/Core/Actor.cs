using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Volcano
{
    abstract class Actor : Node
    {
        public int Health { get; protected set; }
        public bool IsAlive { get; protected set; }
    }
}
