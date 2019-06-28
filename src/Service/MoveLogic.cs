using System;
using System.Drawing;
using thegame.Model;

namespace thegame.Service
{
    
    public class Map
    {
        public CellType[,] map;
        public int width;
        public int height;

        public Map(int width, int height)
        {
            map = new CellType[width,height];
            width = width;
            height = height;
        }
    }

    public static class MoveLogic
    {
        static public Map Move(Movement movement, Map map)
        {
            var player = FindPlayer(map);
            var currentCell = CurrentCell(movement, player);
            var resultMap = map;

            if (map.map[currentCell.X, currentCell.Y] == CellType.Wall)
            {
                return map;
            }

            if (map.map[player.X, player.Y] == CellType.Player)
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
            if (map.map[currentCell.X, currentCell.Y] == CellType.Empty)
            {
                map.map[currentCell.X, currentCell.Y] = CellType.Player;
                map.map[player.X, player.Y] = CellType.Warehouse;
            }
            
            if (map.map[currentCell.X, currentCell.Y] == CellType.Warehouse)
            {
                map.map[currentCell.X, currentCell.Y] = CellType.PlayerAndWareHouse;
                map.map[player.X, player.Y] = CellType.Warehouse;
            }
                
            if (map.map[currentCell.X, currentCell.Y] == CellType.Warehouse)
            {
                var nextCell = CurrentCell(movement, currentCell);
                    
                if (map.map[nextCell.X, nextCell.Y] == CellType.Wall
                    || map.map[nextCell.X, nextCell.Y] == CellType.Box
                    || map.map[nextCell.X, nextCell.Y] == CellType.WarehouseBox)
                {
                    return map;
                }
                    
                if (map.map[nextCell.X, nextCell.Y] == CellType.Empty)
                {
                    map.map[player.X, player.Y] = CellType.Warehouse;
                    map.map[currentCell.X, currentCell.Y] = CellType.Player;
                    map.map[nextCell.X, nextCell.Y] = CellType.Box;
                }
                    
                if (map.map[nextCell.X, nextCell.Y] == CellType.Warehouse)
                {
                    map.map[player.X, player.Y] = CellType.Warehouse;
                    map.map[currentCell.X, currentCell.Y] = CellType.Player;
                    map.map[nextCell.X, nextCell.Y] = CellType.WarehouseBox;
                }
            }
            
            if (map.map[currentCell.X, currentCell.Y] == CellType.WarehouseBox)
            {
                var nextCell = CurrentCell(movement, currentCell);
                    
                if (map.map[nextCell.X, nextCell.Y] == CellType.Wall
                    || map.map[nextCell.X, nextCell.Y] == CellType.Box
                    || map.map[nextCell.X, nextCell.Y] == CellType.WarehouseBox)
                {
                    return map;
                }
                    
                if (map.map[nextCell.X, nextCell.Y] == CellType.Empty)
                {
                    map.map[player.X, player.Y] = CellType.Warehouse;
                    map.map[currentCell.X, currentCell.Y] = CellType.PlayerAndWareHouse;
                    map.map[nextCell.X, nextCell.Y] = CellType.Box;
                }
                    
                if (map.map[nextCell.X, nextCell.Y] == CellType.Warehouse)
                {
                    map.map[player.X, player.Y] = CellType.Warehouse;
                    map.map[currentCell.X, currentCell.Y] = CellType.PlayerAndWareHouse;
                    map.map[nextCell.X, nextCell.Y] = CellType.WarehouseBox;
                }
            }

            return map;
        }

        private static Map SimplePlayerMove(Point player, Point currentCell, Map map, Movement movement)
        {
            if (map.map[currentCell.X, currentCell.Y] == CellType.Empty)
            {
                map.map[currentCell.X, currentCell.Y] = CellType.Player;
                map.map[player.X, player.Y] = CellType.Empty;
            }
            
            if (map.map[currentCell.X, currentCell.Y] == CellType.Warehouse)
            {
                map.map[currentCell.X, currentCell.Y] = CellType.PlayerAndWareHouse;
                map.map[player.X, player.Y] = CellType.Empty;
            }
            
            if (map.map[currentCell.X, currentCell.Y] == CellType.Warehouse)
            {
                var nextCell = CurrentCell(movement, currentCell);
                    
                if (map.map[nextCell.X, nextCell.Y] == CellType.Wall
                    || map.map[nextCell.X, nextCell.Y] == CellType.Box
                    || map.map[nextCell.X, nextCell.Y] == CellType.WarehouseBox)
                {
                    return map;
                }
                    
                if (map.map[nextCell.X, nextCell.Y] == CellType.Empty)
                {
                    map.map[player.X, player.Y] = CellType.Empty;
                    map.map[currentCell.X, currentCell.Y] = CellType.Player;
                    map.map[nextCell.X, nextCell.Y] = CellType.Box;
                }
                    
                if (map.map[nextCell.X, nextCell.Y] == CellType.Warehouse)
                {
                    map.map[player.X, player.Y] = CellType.Empty;
                    map.map[currentCell.X, currentCell.Y] = CellType.Player;
                    map.map[nextCell.X, nextCell.Y] = CellType.WarehouseBox;
                }
            }
            
            if (map.map[currentCell.X, currentCell.Y] == CellType.WarehouseBox)
            {
                var nextCell = CurrentCell(movement, currentCell);
                    
                if (map.map[nextCell.X, nextCell.Y] == CellType.Wall
                    || map.map[nextCell.X, nextCell.Y] == CellType.Box
                    || map.map[nextCell.X, nextCell.Y] == CellType.WarehouseBox)
                {
                    return map;
                }
                    
                if (map.map[nextCell.X, nextCell.Y] == CellType.Empty)
                {
                    map.map[player.X, player.Y] = CellType.Empty;
                    map.map[currentCell.X, currentCell.Y] = CellType.PlayerAndWareHouse;
                    map.map[nextCell.X, nextCell.Y] = CellType.Box;
                }
                    
                if (map.map[nextCell.X, nextCell.Y] == CellType.Warehouse)
                {
                    map.map[player.X, player.Y] = CellType.Empty;
                    map.map[currentCell.X, currentCell.Y] = CellType.PlayerAndWareHouse;
                    map.map[nextCell.X, nextCell.Y] = CellType.WarehouseBox;
                }
            }

            return map;
        }

        static Point FindPlayer(Map map)
        {
            for (int x = 0; x < map.width; x++)
            {
                for (int y = 0; y < map.height; y++)
                {
                    if (map.map[x, y] == CellType.Player || map.map[x, y] == CellType.PlayerAndWareHouse)
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