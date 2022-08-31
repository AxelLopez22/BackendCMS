using CMSPrueba.Models;
using CMSPrueba.Models.Request;
using CMSPrueba.Models.Respuesta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMSPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        public readonly PruebaContext _pruebaContext;

        public PostController(PruebaContext pruebaContext)
        {
            _pruebaContext = pruebaContext;
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> GetPost(int Id)
        {
            Respuesta res = new Respuesta();
            try
            {
                var post = await _pruebaContext.Post.Where(x => x.Id == Id && x.Estado == true).FirstOrDefaultAsync();
                res.Mensaje = "Encontrado";
                res.Data = post;
            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message;
            }
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPost()
        {
            Respuesta res = new Respuesta();
            try
            {
                var post = await _pruebaContext.Post.Where(x => x.Estado == true).ToListAsync();
                res.Mensaje = "Ok";
                res.Data = post;
            } catch(Exception ex)
            {
                res.Mensaje = ex.Message;
            }

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(PostRequest post)
        {
            Respuesta res = new Respuesta();
            try
            {
                Post p = new Post();
                p.Titulo = post.Titulo;
                p.Contenido = post.Contenido;
                p.FechaCreacion = DateTime.Now;
                p.Autor = post.Autor;
                p.Estado = post.Estado;

                _pruebaContext.Add(p);
                await _pruebaContext.SaveChangesAsync();
                res.Mensaje = "Agregado con exito";
            } catch(Exception ex)
            {
               res.Mensaje = ex.Message;
            }
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost(PostRequest post)
        {
            Respuesta res = new Respuesta();
            try
            {
                Post p = new Post();
                p = await _pruebaContext.Post.FindAsync(post.Id);
                p.Titulo = post.Titulo;
                p.Contenido = post.Contenido;
                p.FechaCreacion = DateTime.Now;
                p.Autor = post.Autor;
                p.Estado = post.Estado;

                _pruebaContext.Entry(p).State = EntityState.Modified;
                await _pruebaContext.SaveChangesAsync();
                res.Mensaje = "Editado con exito";
            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message;
            }
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePost(int Id)
        {
            Respuesta res = new Respuesta();
            try
            {
                Post p = new Post();
                p = await _pruebaContext.Post.FindAsync(Id);
                p.Estado = false;

                _pruebaContext.Entry(p).State = EntityState.Modified;
                await _pruebaContext.SaveChangesAsync();
                res.Mensaje = "Eliminado con exito";
            } catch(Exception ex)
            {
                res.Mensaje = ex.Message;
            }
            return Ok(res);
        }
    
    }
}
