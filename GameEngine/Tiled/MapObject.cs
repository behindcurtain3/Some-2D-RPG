﻿using System.Collections.Generic;
using GameEngine.Interfaces;

namespace GameEngine.Tiled
{
    public class MapObject : PropertyBag
    {
        public int X { get; set; }
        public int Y { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return string.Format("MapObject: Name={0}, Type={1}, Pos=({2},{3})",
                Name, Type, X, Y);
        }
    }
}
