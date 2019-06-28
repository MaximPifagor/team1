using System;
using thegame.Model;

namespace thegame.Service
{
    public class Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    
    public class Map
    {
        public CellType[,] Table;
        public int X;
        public int Y;

        public Map(int x, int y)
        {
            Table = new CellType[x,y];
            X = x;
            Y = y;
        }
    }
    
    public static class MoveLogic
    {
        static public Map Move(Movement movement, Map map)
        {
            var player = FindPlayer(map);
            var currentCell = CurrentCell(movement, player);

            if (map.Table[currentCell.x, currentCell.y] == CellType.Wall)
            {
                return map;
            }

            if (map.Table[player.x, player.y] == CellType.Player)
            {
                if (map.Table[currentCell.x, currentCell.y] == CellType.Empty)
                {
                    map.Table[currentCell.x, currentCell.y] = CellType.Player;
                    map.Table[player.x, player.y] = CellType.Empty;
                }
            
                if (map.Table[currentCell.x, currentCell.y] == CellType.Warehouse)
                {
                    map.Table[currentCell.x, currentCell.y] = CellType.PlayerAndWareHouse;
                    map.Table[player.x, player.y] = CellType.Empty;
                }
            
                if (map.Table[currentCell.x, currentCell.y] == CellType.Warehouse)
                {
                    MoveBox();
                }
            }
            else
            {
                if (map.Table[currentCell.x, currentCell.y] == CellType.Empty)
                {
                    map.Table[currentCell.x, currentCell.y] = CellType.Player;
                    map.Table[player.x, player.y] = CellType.Warehouse;
                }
            
                if (map.Table[currentCell.x, currentCell.y] == CellType.Warehouse)
                {
                    map.Table[currentCell.x, currentCell.y] = CellType.PlayerAndWareHouse;
                    map.Table[player.x, player.y] = CellType.Warehouse;
                }
            }
            
            
            

            return map;
        }

        private static void MoveBox()
        {
            
        }

        static Point FindPlayer(Map map)
        {
            for (int x = 0; x < map.X; x++)
            {
                for (int y = 0; y < map.Y; y++)
                {
                    if (map.Table[x, y] == CellType.Player || map.Table[x, y] == CellType.PlayerAndWareHouse)
                    {
                        return new Point(x, y);
                    }
                }
            }

            throw new ArgumentException("Отсутствует игрок");
        }

        static Point CurrentCell(Movement movement, Point player)
        {
            Point current = new Point(0,0);
            switch (movement)
            {
                case Movement.Up:
                    current = new Point(player.x, player.y + 1);
                    break;
                case Movement.Down:
                    current = new Point(player.x, player.y - 1);
                    break;
                case Movement.Left:
                    current = new Point(player.x - 1, player.y);
                    break;
                case Movement.Right:
                    current = new Point(player.x + 1, player.y);
                    break;
            }

            return current;
        }
        
        
    }
}