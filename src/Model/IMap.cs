using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thegame.Model
{
    public interface IMap
    {
        string Serialize();
        Map Clone();
    }

    class Map2 : IMap
    {
        public string Serialize()
        {
            throw new NotImplementedException();
        }

        public Map Clone()
        {
            throw new NotImplementedException();
        }
    }
}
