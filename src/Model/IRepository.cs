using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using thegame.DTO;

namespace thegame.Model
{
    public interface IRepository
    {
        Map GetMapById(Guid id);
        MapDto CreateMap();
    }
}
