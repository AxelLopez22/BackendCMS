using System;
using System.Collections.Generic;

namespace CMSPrueba.Models
{
    public partial class Usuario
    {
        public int IdUser { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public bool Estado { get; set; }
    }
}
