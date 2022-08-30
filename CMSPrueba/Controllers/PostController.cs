using CMSPrueba.Models;
using CMSPrueba.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMSPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        public readonly PruebaContext _pruebaContext;

        public PostController(PruebaContext pruebaContext)
        {
            _pruebaContext = pruebaContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetPost()
        {
            var post = await _pruebaContext.Posts.Where(x => x.Estado == true).ToListAsync();
            if(post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(PostRequest post)
        {
            Post p = new Post();
            try
            {
                p.Titulo = post.Titulo;
                p.Contenido = post.Contenido;
                p.FechaCreacion = DateTime.Now;
                p.Autor = post.Autor;
                p.Estado = post.Estado;

                _pruebaContext.Add(p);
                await _pruebaContext.SaveChangesAsync();    
            } catch(Exception ex)
            {
                var res = ex.Message;
            }
            return Ok(p);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost(PostRequest post)
        {
            Post p = new Post();
            try
            {
                p = await _pruebaContext.Posts.FindAsync(post.Id);
                p.Titulo = post.Titulo;
                p.Contenido = post.Contenido;
                p.FechaCreacion = DateTime.Now;
                p.Autor = post.Autor;
                p.Estado = post.Estado;

                _pruebaContext.Entry(p).State = EntityState.Modified;
                await _pruebaContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var res = ex.Message;
            }
            return Ok(p);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePost(int Id)
        {
            Post p = new Post();
            try
            {
                p = await _pruebaContext.Posts.FindAsync(Id);
                p.Estado = false;

                _pruebaContext.Entry(p).State = EntityState.Modified;
                await _pruebaContext.SaveChangesAsync();
            } catch(Exception ex)
            {
                var res = ex.Message;
            }
            return Ok("Post Eliminado");
        }
    }
}
