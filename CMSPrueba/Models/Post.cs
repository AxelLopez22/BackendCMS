using System;
using System.Collections.Generic;

namespace CMSPrueba.Models
{
    public partial class Post
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Contenido { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public string Autor { get; set; } = null!;
        public bool? Estado { get; set; }
    }
}
