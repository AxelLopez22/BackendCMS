using CMSPrueba.Models.Request;
using CMSPrueba.Models.Respuesta;
using CMSPrueba.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMSPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Auntentificar([FromBody] AuthRequest auth)
        {
            Respuesta r = new Respuesta();
            var res = _userService.Auth(auth);

            if(res == null)
            {
                r.Mensaje = "Usuario o contraseña incorrecta";
                return BadRequest(r);
            }
            r.Mensaje = "Usuario Encontrado";
            r.Data = res;
            return Ok(r);
        }
    }
}
