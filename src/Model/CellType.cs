using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thegame.Model
{
    public enum CellType
    {
        Empty,
        Wall,
        Warehouse,
        Box,
        Player
    }

    public enum Movement
    {
        Up,
        Down,
        Left,
        Right
    }
}
