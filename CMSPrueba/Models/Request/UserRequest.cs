namespace CMSPrueba.Models.Request
{
    public class UserRequest
    {
        public int IdUser { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public bool Estado { get; set; }
    }
}
