using CMSPrueba.Models;
using CMSPrueba.Models.Request;
using CMSPrueba.Models.Respuesta;
using CMSPrueba.Service;
using CMSPrueba.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMSPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public readonly PruebaContext _pruebaContext;

        public UserController(IUserService userService, PruebaContext pruebaContext)
        {
            _userService = userService;
            _pruebaContext = pruebaContext;
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

        [HttpPost]
        public async Task<IActionResult> AddUser(UserRequest user)
        {
            Respuesta res = new Respuesta();
            try
            {
                string spassword = encript.GetSHA256(user.Password);
                Usuario u = new Usuario();
                u.Nombre = user.Nombre;
                u.Email = user.Email;
                u.Password = spassword;
                u.Estado = true;

                _pruebaContext.Add(u);
                await _pruebaContext.SaveChangesAsync();
                res.Mensaje = "Usuario agregado con exito";
            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message;
            }
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            Respuesta res = new Respuesta();
            try
            {
                var user = await _pruebaContext.Usuarios.Where(x => x.Estado == true).ToListAsync();
                if(user == null)
                {
                    return BadRequest();
                }
                res.Mensaje = "Ok";
                res.Data = user;
            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message;
            }
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> EditUser(UserRequest user)
        {
            Respuesta res = new Respuesta();
            try
            {
                Usuario u = new Usuario();
                u = await _pruebaContext.Usuarios.FindAsync(user.IdUser);
                string spassword = encript.GetSHA256(user.Password);
                u.Nombre = user.Nombre;
                u.Email = user.Email;
                u.Password = spassword;
                u.Estado = true;

                _pruebaContext.Entry(u).State = EntityState.Modified;
                await _pruebaContext.SaveChangesAsync();
                res.Mensaje = "Usuario editado con exito";
            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message;
            }
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            Respuesta res = new Respuesta();
            try
            {
                Usuario u = new Usuario();
                u = await _pruebaContext.Usuarios.FindAsync(Id);
                if(u == null)
                {
                    return BadRequest();
                }
                u.Estado = false;
                _pruebaContext.Entry(u).State = EntityState.Modified;
                await _pruebaContext.SaveChangesAsync();
                res.Mensaje = "Eliminado con exito";

            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message;
            }

            return Ok(res);
        }
    }
}
