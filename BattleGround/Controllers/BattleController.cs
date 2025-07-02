using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace BattleGround.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BattleController : ControllerBase
    {
        private static ConcurrentDictionary<string, int> players = new();

        [HttpGet]
        public IActionResult GetAll() => Ok(players);

        [HttpPost("add/{name}")]
        public IActionResult Add(string name)
        {
            if (!players.ContainsKey(name))
                players[name] = 100;
            return Ok(players);
        }

        [HttpPost("attack/{name}")]
        public IActionResult Attack(string name)
        {
            if (players.ContainsKey(name))
                players[name] = Math.Max(0, players[name] - 10);
            return Ok(players);
        }

        [HttpPost("heal/{name}")]
        public IActionResult Heal(string name)
        {
            if (players.ContainsKey(name))
                players[name] = Math.Min(100, players[name] + 10);
            return Ok(players);
        }

        [HttpPost("reset")]
        public IActionResult Reset()
        {
            players.Clear();
            return Ok(players);
        }
    }
}
