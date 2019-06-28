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
        public static bool IsFinished(Map map)
        {  
                for (var i = 0; i < map.width; i++)
                    for (var j = 0; j < map.height; j++)
                        if (map.map[i, j] == CellType.PlayerAndWareHouse || map.map[i, j] == CellType.Warehouse || map.map[i,j] == CellType.Box)
                            return false;
                return true;
            
        }
    }
}
