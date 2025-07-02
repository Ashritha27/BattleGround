using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace BattleGround.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BattleController : ControllerBase
    {


        public record PlayerState(string Avatar, int HP);

        private static Dictionary<string, PlayerState> players = new();


        [HttpGet]
        public IActionResult GetAll() => Ok(players);


        [HttpPost("add/{name}")]
        public IActionResult Add(string name, [FromQuery] string avatar = "👾")
        {
            if (!players.ContainsKey(name))
                players[name] = new PlayerState(avatar, 100);
            return Ok(players);
        }

        [HttpPost("attack/{name}")]
        public IActionResult Attack(string name)
        {
            if (players.ContainsKey(name))
            {
                var current = players[name];
                var newHp = Math.Max(0, current.HP - 10);
                players[name] = current with { HP = newHp };
            }
            return Ok(players);
        }

        [HttpPost("heal/{name}")]
        public IActionResult Heal(string name)
        {
            if (players.ContainsKey(name))
            {
                var current = players[name];
                var newHp = Math.Min(100, current.HP + 10);
                players[name] = current with { HP = newHp };
            }
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
