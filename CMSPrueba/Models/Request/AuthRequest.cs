using System.ComponentModel.DataAnnotations;

namespace CMSPrueba.Models.Request
{
    public class AuthRequest
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
