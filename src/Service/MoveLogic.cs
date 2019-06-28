using System;
using System.Drawing;
using thegame.Model;

namespace thegame.Service
{
    
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
            var resultMap = map;

            if (map.Table[currentCell.X, currentCell.Y] == CellType.Wall)
            {
                return map;
            }

            if (map.Table[player.X, player.Y] == CellType.Player)
            {
                resultMap = SimplePlayerMove(player, currentCell, map, movement);
            }
            else
            {
                resultMap = PlayerWarehouseMove(player, currentCell, map, movement);
            }
            
            return resultMap;
        }

        private static Map PlayerWarehouseMove(Point player, Point currentCell, Map map, Movement movement)
        {
            if (map.Table[currentCell.X, currentCell.Y] == CellType.Empty)
            {
                map.Table[currentCell.X, currentCell.Y] = CellType.Player;
                map.Table[player.X, player.Y] = CellType.Warehouse;
            }
            
            if (map.Table[currentCell.X, currentCell.Y] == CellType.Warehouse)
            {
                map.Table[currentCell.X, currentCell.Y] = CellType.PlayerAndWareHouse;
                map.Table[player.X, player.Y] = CellType.Warehouse;
            }
                
            if (map.Table[currentCell.X, currentCell.Y] == CellType.Warehouse)
            {
                var nextCell = CurrentCell(movement, currentCell);
                    
                if (map.Table[nextCell.X, nextCell.Y] == CellType.Wall
                    || map.Table[nextCell.X, nextCell.Y] == CellType.Box
                    || map.Table[nextCell.X, nextCell.Y] == CellType.WarehouseBox)
                {
                    return map;
                }
                    
                if (map.Table[nextCell.X, nextCell.Y] == CellType.Empty)
                {
                    map.Table[player.X, player.Y] = CellType.Warehouse;
                    map.Table[currentCell.X, currentCell.Y] = CellType.Player;
                    map.Table[nextCell.X, nextCell.Y] = CellType.Box;
                }
                    
                if (map.Table[nextCell.X, nextCell.Y] == CellType.Warehouse)
                {
                    map.Table[player.X, player.Y] = CellType.Warehouse;
                    map.Table[currentCell.X, currentCell.Y] = CellType.Player;
                    map.Table[nextCell.X, nextCell.Y] = CellType.WarehouseBox;
                }
            }
            
            if (map.Table[currentCell.X, currentCell.Y] == CellType.WarehouseBox)
            {
                var nextCell = CurrentCell(movement, currentCell);
                    
                if (map.Table[nextCell.X, nextCell.Y] == CellType.Wall
                    || map.Table[nextCell.X, nextCell.Y] == CellType.Box
                    || map.Table[nextCell.X, nextCell.Y] == CellType.WarehouseBox)
                {
                    return map;
                }
                    
                if (map.Table[nextCell.X, nextCell.Y] == CellType.Empty)
                {
                    map.Table[player.X, player.Y] = CellType.Warehouse;
                    map.Table[currentCell.X, currentCell.Y] = CellType.PlayerAndWareHouse;
                    map.Table[nextCell.X, nextCell.Y] = CellType.Box;
                }
                    
                if (map.Table[nextCell.X, nextCell.Y] == CellType.Warehouse)
                {
                    map.Table[player.X, player.Y] = CellType.Warehouse;
                    map.Table[currentCell.X, currentCell.Y] = CellType.PlayerAndWareHouse;
                    map.Table[nextCell.X, nextCell.Y] = CellType.WarehouseBox;
                }
            }

            return map;
        }

        private static Map SimplePlayerMove(Point player, Point currentCell, Map map, Movement movement)
        {
            if (map.Table[currentCell.X, currentCell.Y] == CellType.Empty)
            {
                map.Table[currentCell.X, currentCell.Y] = CellType.Player;
                map.Table[player.X, player.Y] = CellType.Empty;
            }
            
            if (map.Table[currentCell.X, currentCell.Y] == CellType.Warehouse)
            {
                map.Table[currentCell.X, currentCell.Y] = CellType.PlayerAndWareHouse;
                map.Table[player.X, player.Y] = CellType.Empty;
            }
            
            if (map.Table[currentCell.X, currentCell.Y] == CellType.Warehouse)
            {
                var nextCell = CurrentCell(movement, currentCell);
                    
                if (map.Table[nextCell.X, nextCell.Y] == CellType.Wall
                    || map.Table[nextCell.X, nextCell.Y] == CellType.Box
                    || map.Table[nextCell.X, nextCell.Y] == CellType.WarehouseBox)
                {
                    return map;
                }
                    
                if (map.Table[nextCell.X, nextCell.Y] == CellType.Empty)
                {
                    map.Table[player.X, player.Y] = CellType.Empty;
                    map.Table[currentCell.X, currentCell.Y] = CellType.Player;
                    map.Table[nextCell.X, nextCell.Y] = CellType.Box;
                }
                    
                if (map.Table[nextCell.X, nextCell.Y] == CellType.Warehouse)
                {
                    map.Table[player.X, player.Y] = CellType.Empty;
                    map.Table[currentCell.X, currentCell.Y] = CellType.Player;
                    map.Table[nextCell.X, nextCell.Y] = CellType.WarehouseBox;
                }
            }
            
            if (map.Table[currentCell.X, currentCell.Y] == CellType.WarehouseBox)
            {
                var nextCell = CurrentCell(movement, currentCell);
                    
                if (map.Table[nextCell.X, nextCell.Y] == CellType.Wall
                    || map.Table[nextCell.X, nextCell.Y] == CellType.Box
                    || map.Table[nextCell.X, nextCell.Y] == CellType.WarehouseBox)
                {
                    return map;
                }
                    
                if (map.Table[nextCell.X, nextCell.Y] == CellType.Empty)
                {
                    map.Table[player.X, player.Y] = CellType.Empty;
                    map.Table[currentCell.X, currentCell.Y] = CellType.PlayerAndWareHouse;
                    map.Table[nextCell.X, nextCell.Y] = CellType.Box;
                }
                    
                if (map.Table[nextCell.X, nextCell.Y] == CellType.Warehouse)
                {
                    map.Table[player.X, player.Y] = CellType.Empty;
                    map.Table[currentCell.X, currentCell.Y] = CellType.PlayerAndWareHouse;
                    map.Table[nextCell.X, nextCell.Y] = CellType.WarehouseBox;
                }
            }

            return map;
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
                    current = new Point(player.X, player.Y + 1);
                    break;
                case Movement.Down:
                    current = new Point(player.X, player.Y - 1);
                    break;
                case Movement.Left:
                    current = new Point(player.X - 1, player.Y);
                    break;
                case Movement.Right:
                    current = new Point(player.X + 1, player.Y);
                    break;
            }

            return current;
        }
        
        
    }
}