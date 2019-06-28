using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thegame.Model
{
    public class Map
    {
        public CellType[,] map;
        public int Width;
        public int Height;
        private string str;

        public Map(string str, int width, int height)
        {
            if (str.Length != width * height)
                throw new Exception();
            Width = width;
            Height = height;
            var strIndex = 0;
            map = new CellType[width, height];
            for (var i = 0; i < width; i++)
                for (var j = 0; j < height; j++)
                    map[i, j] = (CellType) int.Parse(str[strIndex++] + "");
            this.str = str;
        }

        public Map Clone()
        {
            //string st = (string)str.Clone();
            return new Map(str, Width, Height);
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