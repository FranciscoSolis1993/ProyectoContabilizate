using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebContabilizate.Models;

namespace WebContabilizate
{
    public class BusContribuyentes
    {
        public Usuario Login(string usuario, string contraseña)
        {
            DatContribuyentes datos = new DatContribuyentes();
            DataRow dr = datos.Login(usuario, contraseña);
            Usuario vusuario = new Usuario();
            vusuario.IdUsuario = Convert.ToInt32(dr["IdContribuyente"]);
            vusuario.NombreCompleto = dr["NombreCompleto"].ToString();
            vusuario.CorreoElectronico = dr["CorreoElectronico"].ToString();
            vusuario.Contraseña= dr["Contraseña"].ToString();
            
            return vusuario;
        }
        public List<EntidadContribuyentes> ObtenerContribuyentes(int idUsuario)
        {
            DatContribuyentes datos = new DatContribuyentes();
            List<EntidadContribuyentes> ls = new List<EntidadContribuyentes>();
            DataTable dt = new DataTable();
            dt = datos.ObtenerContribuyentes(idUsuario);

            foreach (DataRow dr in dt.Rows)
            {
                EntidadContribuyentes can = new EntidadContribuyentes();
                can.IdContribuyente= Convert.ToInt32(dr["IdContribuyente"]);
                can.Nombre= dr["Nombre"].ToString();
                can.RFC= dr["RFC"].ToString();
                can.CorreoElectronico= dr["CorreoElectronico"].ToString();
                can.IdUsuario= Convert.ToInt32(dr["IdUsuario"]);
                
                ls.Add(can);
            }
            return ls;
        }
        public void AgregarContribuyente(EntidadContribuyentes j)
        {
            int filas = new DatContribuyentes().AgregarContribuyente(j.Nombre, j.RFC, j.CorreoElectronico,j.IdUsuario);
            if (filas != 1)
            {
                throw new ApplicationException("Error al Agregar");
            }
        }

        public EntidadContribuyentes ObtenerContribuyente(int IdContribuyentes)
        {
            DataRow dr = new DatContribuyentes().ObtenerContribuyente(IdContribuyentes);
            EntidadContribuyentes can = new EntidadContribuyentes();
            can.IdContribuyente = Convert.ToInt32(dr["IdContribuyente"]);
            can.Nombre = dr["Nombre"].ToString();
            can.RFC = dr["RFC"].ToString();
            can.CorreoElectronico = dr["CorreoElectronico"].ToString();
            can.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
            return can;
        }

        public void BorrarPost(int IdContribuyente)
        {
            DatContribuyentes datos = new DatContribuyentes();
            int filas = datos.BorrarContribuyente(IdContribuyente);
            if (filas != 1)
            {
                throw new ApplicationException("Error al borrar");
            }

        }

        public void EditarContribuyente(EntidadContribuyentes j)
        {
            int filas = new DatContribuyentes().EditarContribuyente(j.IdContribuyente,j.Nombre, j.RFC, j.CorreoElectronico, j.IdUsuario);
            if (filas != 1)
            {
                throw new ApplicationException("Error al Editar");
            }
        }
    }
}