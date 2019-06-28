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
        public int width;
        public int height;
        private string str;
        public Map(int width, int height) {
            this.height = height;
            this.width = width;
            this.map = new CellType[width, height];
        }

        public Map(String str, int width, int height) {
            if (str.Length != width * height) {
                throw new Exception();
            }
            var strIndex = 0;
            map = new CellType[width, height];
            for (int i = 0; i < width; i++) {
                for (var j = 0; j < height; j++)
                {
                    CellType cell = (CellType)int.Parse(str[strIndex++] + "");
                    map[i, j] = cell;
                }
            }

            this.str = str;

        }



       

        public string Serialize() {
            return "1,1,1,1,1 " + "1,0,4,0,1 " + "1,0,3,0,1 " + "1,0,2,0,1 " + "1,1,1,1,1";
            StringBuilder newStr = new StringBuilder();
            for (int i = 0; i < width; i++)
            {
               for (int j = 0; j < height; j++) {
                    if (j != height-1)
                        newStr.Append((int)map[i, j] + ",");
                    else
                        newStr.Append((int)map[i, j]);
               }
               if(i!= width-1)
               newStr.Append(" ");
            }
            
            return newStr.ToString();
        }
    }
}
