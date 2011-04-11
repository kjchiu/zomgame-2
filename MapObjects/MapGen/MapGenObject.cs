using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zomgame.MapObjects.MapGen
{
    abstract class MapGenObject
    {
        public enum Direction {NORTH, SOUTH, EAST, WEST};

        public abstract void Construct(Map aMap);
    }
}
