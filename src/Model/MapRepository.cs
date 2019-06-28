using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using thegame.DTO;

namespace thegame.Model
{
    public class MapRepository : IRepository
    {
        private Dictionary<Guid, Map> repository;

        public MapRepository() {
            repository = new Dictionary<Guid, Map>();
        }
        public MapDto CreateMap(int LevelId)
        {
            Map map = GetMap(LevelId);
            var id = Guid.NewGuid();
            repository[id] = map;
            MapDto dto = new MapDto();
            dto.map = map.Serialize();
            dto.id = id;
            return dto;

        }

        public Map GetMapById(Guid id)
        {
            if (!repository.ContainsKey(id))
                return null;
            return repository[id];

        }

        private static Dictionary<int, Map> levels = new Dictionary<int, Map>()
        {
            {0, new Map("11111" + "10401" + "10301" + "10201" + "11111", 5, 5)},
            {
                1, new Map("111111111" + "111000321" + "124300001" + "111032031" + "121030021" + "101020031" +
                           "130033221" + "100020001" + "111111111", 9, 9)
            },
            {
                2, new Map(string.Concat("00000000000000", "00000000000000", "11111111111111", "12200100000111",
                    "12200103003001", "12200131111001", "12200004011001", "12200101003011", "11111101130301",
                    "11103003030301", "11100001000001", "11111111111111", "00000000000000", "00000000000000"), 14, 14)
            }
        };

       
        public Map GetMap(int LevelId) {
            if (!levels.ContainsKey(LevelId))
                return levels[0];
            return levels[LevelId];
        }
    }
}
