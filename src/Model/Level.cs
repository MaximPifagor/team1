using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Remotion.Linq.Utilities;
using thegame.Service;

namespace thegame.Model
{
    public class Level
    {
        public Map Map { get; set; }
        public Point PlayerPos { get; set; }

        public Level(string map, int width, int height)
        {
            Map = new Map(map, width, height);
            PlayerPos = MoveLogic.FindPlayer(Map);
        }
        public bool IsFinished
        {
            get
            {
                for (var i = 0; i < Map.width; i++)
                    for (var j = 0; j < Map.height; j++)
                        if (Map.map[i, j] == CellType.PlayerAndWareHouse || Map.map[i, j] == CellType.Warehouse || Map.map[i,j] == CellType.Box)
                            return false;
                return true;
            }
        }
    }
}
