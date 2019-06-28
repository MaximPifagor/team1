using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using thegame.Model;

namespace thegame.DTO
{
    public class MapDto
    {
        public string map { get; set; }
        public Guid id { get; set; }
        public bool isFinished { get; set; }
    }
}