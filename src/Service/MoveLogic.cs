using System;
using System.Drawing;
using thegame.Model;

namespace thegame.Service
{
    public static class MoveLogic
    {
        public static Map Move(Movement movement, Map map)
        {
            var player = FindPlayer(map);
            var currentCell = CurrentCell(movement, player);
            if (map[currentCell] == CellType.Wall)
                return map;
            if (map[player] == CellType.Player)
                return SimplePlayerMove(player, currentCell, map, movement);
            return PlayerWarehouseMove(player, currentCell, map, movement);
        }

        private static Map SimplePlayerMove(Point player, Point currentCell, Map map, Movement movement)
        {
            if (map[currentCell] == CellType.Empty)
            {
                map[currentCell] = CellType.Player;
                map[player] = CellType.Empty;
            }
            if (map[currentCell] == CellType.Warehouse)
            {
                map[currentCell] = CellType.PlayerAndWareHouse;
                map[player] = CellType.Empty;
            }
            if (map[currentCell] == CellType.Box)
            {
                var nextCell = CurrentCell(movement, currentCell);
                if (map[nextCell] == CellType.Wall
                    || map[nextCell] == CellType.Box
                    || map[nextCell] == CellType.WarehouseBox)
                    return map;
                if (map[nextCell] == CellType.Empty)
                {
                    map[player] = CellType.Empty;
                    map[currentCell] = CellType.Player;
                    map[nextCell] = CellType.Box;
                }
                if (map[nextCell] == CellType.Warehouse)
                {
                    map[player] = CellType.Empty;
                    map[currentCell] = CellType.Player;
                    map[nextCell] = CellType.WarehouseBox;
                }
            }
            if (map[currentCell] == CellType.WarehouseBox)
            {
                var nextCell = CurrentCell(movement, currentCell);
                if (map[nextCell] == CellType.Wall
                    || map[nextCell] == CellType.Box
                    || map[nextCell] == CellType.WarehouseBox)
                    return map;
                if (map[nextCell] == CellType.Empty)
                {
                    map[player] = CellType.Empty;
                    map[currentCell] = CellType.PlayerAndWareHouse;
                    map[nextCell] = CellType.Box;
                }
                if (map[nextCell] == CellType.Warehouse)
                {
                    map[player] = CellType.Empty;
                    map[currentCell] = CellType.PlayerAndWareHouse;
                    map[nextCell] = CellType.WarehouseBox;
                }
            }
            return map;
        }

        private static Map PlayerWarehouseMove(Point player, Point currentCell, Map map, Movement movement)
        {
            if (map[currentCell] == CellType.Empty)
            {
                map[currentCell] = CellType.Player;
                map[player] = CellType.Warehouse;
            }
            if (map[currentCell] == CellType.Warehouse)
            {
                map[currentCell] = CellType.PlayerAndWareHouse;
                map[player] = CellType.Warehouse;
            }
            if (map[currentCell] == CellType.Box)
            {
                var nextCell = CurrentCell(movement, currentCell);
                if (map[nextCell] == CellType.Wall
                    || map[nextCell] == CellType.Box
                    || map[nextCell] == CellType.WarehouseBox)
                    return map;
                if (map[nextCell] == CellType.Empty)
                {
                    map[player] = CellType.Warehouse;
                    map[currentCell] = CellType.Player;
                    map[nextCell] = CellType.Box;
                }
                if (map[nextCell] == CellType.Warehouse)
                {
                    map[player] = CellType.Warehouse;
                    map[currentCell] = CellType.Player;
                    map[nextCell] = CellType.WarehouseBox;
                }
            }
            if (map[currentCell] == CellType.WarehouseBox)
            {
                var nextCell = CurrentCell(movement, currentCell);
                if (map[nextCell] == CellType.Wall
                    || map[nextCell] == CellType.Box
                    || map[nextCell] == CellType.WarehouseBox)
                    return map;
                if (map[nextCell] == CellType.Empty)
                {
                    map[player] = CellType.Warehouse;
                    map[currentCell] = CellType.PlayerAndWareHouse;
                    map[nextCell] = CellType.Box;
                }
                if (map[nextCell] == CellType.Warehouse)
                {
                    map[player] = CellType.Warehouse;
                    map[currentCell] = CellType.PlayerAndWareHouse;
                    map[nextCell] = CellType.WarehouseBox;
                }
            }
            return map;
        }

        public static Point FindPlayer(Map map)
        {
            for (var x = 0; x < map.Width; x++)
                for (var y = 0; y < map.Height; y++)
                    if (map.map[x, y] == CellType.Player || map.map[x, y] == CellType.PlayerAndWareHouse)
                        return new Point(x, y);
            throw new ArgumentException("Отсутствует игрок");
        }

        static Point CurrentCell(Movement movement, Point player)
        {
            var current = new Point(0,0);
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