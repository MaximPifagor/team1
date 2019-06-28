using System;
using System.Drawing;
using thegame.Model;

namespace thegame.Service
{
    public static class MoveLogic
    {
        public static bool IsFinished(Map map)
        {
            for (var i = 0; i < map.Width; i++)
                for (var j = 0; j < map.Height; j++)
                    if (map.map[i, j] == CellType.Box)
                        return false;
            return true;
        }

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
                AssignNewMapPoints(map, Tuple.Create(player, CellType.Empty), Tuple.Create(currentCell, CellType.Player));
            if (map[currentCell] == CellType.Warehouse)
                AssignNewMapPoints(map, Tuple.Create(player, CellType.Empty), Tuple.Create(currentCell, CellType.PlayerAndWareHouse));
            if (map[currentCell] == CellType.Box)
            {
                var nextCell = CurrentCell(movement, currentCell);
                if (map[nextCell] == CellType.Wall || map[nextCell] == CellType.Box || map[nextCell] == CellType.WarehouseBox)
                    return map;
                if (map[nextCell] == CellType.Empty)
                    AssignNewMapPoints(map,
                        Tuple.Create(player, CellType.Empty),
                        Tuple.Create(currentCell, CellType.Player),
                        Tuple.Create(nextCell, CellType.Box));
                if (map[nextCell] == CellType.Warehouse)
                    AssignNewMapPoints(map,
                        Tuple.Create(player, CellType.Empty),
                        Tuple.Create(currentCell, CellType.Player),
                        Tuple.Create(nextCell, CellType.WarehouseBox));
            }
            if (map[currentCell] == CellType.WarehouseBox)
            {
                var nextCell = CurrentCell(movement, currentCell);
                if (map[nextCell] == CellType.Wall || map[nextCell] == CellType.Box || map[nextCell] == CellType.WarehouseBox)
                    return map;
                if (map[nextCell] == CellType.Empty)
                    AssignNewMapPoints(map,
                        Tuple.Create(player, CellType.Empty),
                        Tuple.Create(currentCell, CellType.PlayerAndWareHouse),
                        Tuple.Create(nextCell, CellType.Box));
                if (map[nextCell] == CellType.Warehouse)
                    AssignNewMapPoints(map,
                        Tuple.Create(player, CellType.Empty),
                        Tuple.Create(currentCell, CellType.PlayerAndWareHouse),
                        Tuple.Create(nextCell, CellType.WarehouseBox));
            }
            return map;
        }

        private static Map PlayerWarehouseMove(Point player, Point currentCell, Map map, Movement movement)
        {
            if (map[currentCell] == CellType.Empty)
                AssignNewMapPoints(map, Tuple.Create(player, CellType.Warehouse), Tuple.Create(currentCell, CellType.Player));
            if (map[currentCell] == CellType.Warehouse)
                AssignNewMapPoints(map, Tuple.Create(player, CellType.Warehouse), Tuple.Create(currentCell, CellType.PlayerAndWareHouse));
            if (map[currentCell] == CellType.Box)
            {
                var nextCell = CurrentCell(movement, currentCell);
                if (map[nextCell] == CellType.Wall || map[nextCell] == CellType.Box || map[nextCell] == CellType.WarehouseBox)
                    return map;
                if (map[nextCell] == CellType.Empty)
                    AssignNewMapPoints(map,
                        Tuple.Create(player, CellType.Warehouse),
                        Tuple.Create(currentCell, CellType.Player),
                        Tuple.Create(nextCell, CellType.Box));
                if (map[nextCell] == CellType.Warehouse)
                    AssignNewMapPoints(map,
                        Tuple.Create(player, CellType.Warehouse),
                        Tuple.Create(currentCell, CellType.Player),
                        Tuple.Create(nextCell, CellType.WarehouseBox));
            }
            if (map[currentCell] == CellType.WarehouseBox)
            {
                var nextCell = CurrentCell(movement, currentCell);
                if (map[nextCell] == CellType.Wall || map[nextCell] == CellType.Box || map[nextCell] == CellType.WarehouseBox)
                    return map;
                if (map[nextCell] == CellType.Empty)
                    AssignNewMapPoints(map,
                        Tuple.Create(player, CellType.Warehouse),
                        Tuple.Create(currentCell, CellType.PlayerAndWareHouse),
                        Tuple.Create(nextCell, CellType.Box));
                if (map[nextCell] == CellType.Warehouse)
                    AssignNewMapPoints(map, 
                        Tuple.Create(player, CellType.Warehouse), 
                        Tuple.Create(currentCell, CellType.PlayerAndWareHouse), 
                        Tuple.Create(nextCell, CellType.WarehouseBox));
            }
            return map;
        }

        private static void AssignNewMapPoints(Map map, params Tuple<Point, CellType>[] values)
        {
            foreach (var value in values)
                map[value.Item1] = value.Item2;
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