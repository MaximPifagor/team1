using Microsoft.AspNetCore.Mvc;
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
    }
}
