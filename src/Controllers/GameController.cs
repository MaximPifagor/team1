using Microsoft.AspNetCore.Mvc;
using thegame.Service;

namespace thegame.Controllers
{
    [Route("api/game")]
    public class GameController : Controller
    {
        [HttpGet("score")]
        public IActionResult Score()
        {
            //MoveLogic.Move()
            return Ok(50);
        }
    }
}
