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
        public GameController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("score")]
        public IActionResult Score()
        {
            return Ok(50);
        }

        [HttpGet]
        public ActionResult<MapDto> Get(int? level) {
            if (level == null)
                level = 0;
            var mapDto = repository.CreateMap((int)level);
            return Ok(mapDto);
        }

        [HttpGet("{id}")]
        public ActionResult<MapDto> GetState([FromRoute]Guid id, Movement movement)
        {
            var map = repository.GetMapById(id);
            if (map == null)
                return NotFound();
            var mapNew = Service.MoveLogic.Move(movement, map);
            var dto = new MapDto();
            dto.map = mapNew.Serialize();
            dto.id = id;
            if (MoveLogic.IsFinished(mapNew))
                dto.isFinished = true;
            return Ok(dto);
        }
    }
}