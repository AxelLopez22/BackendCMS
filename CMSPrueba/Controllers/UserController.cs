using CMSPrueba.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMSPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Auntentificar([FromBody] AuthRequest auth)
        {
            return Ok(auth);
        }
    }
}
