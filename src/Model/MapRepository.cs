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
        public MapDto CreateMap()
        {
            Map map = new Map("11111"+ "10401" +"10301"+ "10201"+ "11111",5,5);
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
    }
}
