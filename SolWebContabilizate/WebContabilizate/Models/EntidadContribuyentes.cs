using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebContabilizate.Models
{
    public class EntidadContribuyentes
    {
        public int IdContribuyente { get; set; }
        public string Nombre { get; set; }
        public string RFC { get; set; }
        public string CorreoElectronico { get; set; }
        public int IdUsuario { get; set; }

    }
}