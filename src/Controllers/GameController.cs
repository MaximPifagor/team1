using Microsoft.AspNetCore.Mvc;
using thegame.Service;
using System;
using thegame.DTO;
using thegame.Model;

namespace thegame.Controllers
{
    [Route("api/game")]
    public class GameController : Controller
    {
        IRepository repository;
        public GameController(IRepository repository) {
            this.repository = repository; 
        }

        [HttpGet("score")]
        public IActionResult Score()
        { 
            return Ok(50);
        }
        [HttpGet]
        public ActionResult<MapDto> Get() {
            MapDto mapDto = repository.CreateMap();
            return Ok(mapDto);
        }

        [HttpGet("{id}")]
        public ActionResult<MapDto> GetState([FromRoute]Guid id, Movement movement) {
            Map map = repository.GetMapById(id);
            if (map == null)
                return NotFound();
            Map mapNew = Service.MoveLogic.Move(movement, map);
            MapDto dto = new MapDto();
            dto.map = mapNew.Serialize();
            dto.id = id;
            return Ok(dto);
        }
    }
}
