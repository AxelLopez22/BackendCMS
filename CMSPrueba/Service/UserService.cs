using CMSPrueba.Models;
using CMSPrueba.Models.Commons;
using CMSPrueba.Models.Request;
using CMSPrueba.Models.Respuesta;
using CMSPrueba.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CMSPrueba.Service
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        public readonly PruebaContext _pruebaContext;

        public UserService(PruebaContext pruebaContext, IOptions<AppSettings> appsettings)
        {
            _pruebaContext = pruebaContext;
            _appSettings = appsettings.Value;
        }

        public UserResponse Auth(AuthRequest model)
        {
            UserResponse response = new UserResponse();
            //Usuario u = new Usuario();
      
            string spassword = encript.GetSHA256(model.Password);
            var u = _pruebaContext.Usuarios.Where(x => x.Email == model.Email && x.Password == spassword).FirstOrDefault();


            if (u == null) return null;

            //response.Email = user.Result.Email;
            response.Email = u.Email;
            response.Token = GetToken(u);

            return response;
        }

        private string GetToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, usuario.IdUser.ToString()),
                            new Claim(ClaimTypes.Email, usuario.Email)
                        }
                    ),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
