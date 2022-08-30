using CMSPrueba.Models.Request;
using CMSPrueba.Models.Respuesta;

namespace CMSPrueba.Service
{
    public interface IUserService
    {
        UserResponse Auth(AuthRequest model);
    }
}
