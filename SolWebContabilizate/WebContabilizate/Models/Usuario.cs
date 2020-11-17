using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebContabilizate.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contraseña { get; set; }
    }
}