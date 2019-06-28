using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thegame.Model
{
    public class Map
    {
        public CellType[,] map;
        public readonly int Width;
        public readonly int Height;
        private string description;

        public CellType this[Point p]
        {
            get => map[p.X, p.Y];
            set => map[p.X, p.Y] = value;
        }

        public Map(string description, int width, int height)
        {
            if (description.Length != width * height)
                throw new Exception();
            Width = width;
            Height = height;
            var strIndex = 0;
            map = new CellType[width, height];
            for (var i = 0; i < width; i++)
                for (var j = 0; j < height; j++)
                    map[i, j] = (CellType) int.Parse(description[strIndex++] + "");
            this.description = description;
        }

        public string Serialize()
        {
            var newStr = new StringBuilder();
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                    if (j != Height - 1)
                        newStr.Append((int) map[i, j] + ",");
                    else
                        newStr.Append((int) map[i, j]);
                if (i != Width - 1)
                    newStr.Append(" ");
            }
            return newStr.ToString();
        }
    }
}