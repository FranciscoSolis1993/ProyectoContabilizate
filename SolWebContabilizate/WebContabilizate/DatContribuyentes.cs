using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebContabilizate
{
    public class DatContribuyentes
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sql"].ConnectionString);
        DataTable dt = new DataTable();
        DataRow dr;
        SqlCommand com = new SqlCommand();

        public DataRow Login(string usuario, string contraseña)
        {
            com = new SqlCommand("spVerificaContribuyente", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@NombreUsuario", usuario);
            com.Parameters.AddWithValue("@Contraseña", contraseña);
            SqlDataAdapter da = new SqlDataAdapter(com);
            dt = new DataTable();
            da.Fill(dt);
            DataRow dr = dt.Rows[0];
            return dr;
        }
        public DataTable ObtenerContribuyentes(int idReclutador)
        {
            com = new SqlCommand("spObtenerContribuyentes", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@id", idReclutador);
            SqlDataAdapter da = new SqlDataAdapter(com);
            dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public int AgregarContribuyente(string nombre, string RFC, string CorreoElectronico, int IdUsuario)
        {
            int filas = 0;
            com = new SqlCommand("spAgregarContribuyente", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@nombre", nombre);
            com.Parameters.AddWithValue("@rfc", RFC);
            com.Parameters.AddWithValue("@correoelectronico", CorreoElectronico);
            com.Parameters.AddWithValue("@idusuario", IdUsuario);
            
            try
            {
                con.Open();
                filas = com.ExecuteNonQuery();
                con.Close();
                return filas;

            }
            catch (Exception ex)
            {
                con.Close();
                throw ex;
            }
        }

        public DataRow ObtenerContribuyente(int IdContribuyente)
        {
            com = new SqlCommand("spObtenerContribuyente", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@idcontribuyente", IdContribuyente);
            SqlDataAdapter da = new SqlDataAdapter(com);
            dt = new DataTable();
            DataRow dr;
            da.Fill(dt);
            dr = dt.Rows[0];
            return dr;

        }

        public int BorrarContribuyente(int IdContribuyente)
        {
            int filas = 0;
            com = new SqlCommand("BorrarContribuyente", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@idcontribuyente", IdContribuyente);
            try
            {
                con.Open();
                filas = com.ExecuteNonQuery();
                con.Close();
                return filas;

            }
            catch (Exception ex)
            {

                con.Close();
                throw ex;
            }

        }

        public int EditarContribuyente(int IdContribuyente, string nombre, string RFC, string CorreoElectronico, int IdUsuario)
        {
            int filas = 0;
            com = new SqlCommand("EditarContribuyente", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@idcontribuyente", IdContribuyente);
            com.Parameters.AddWithValue("@nombre", nombre);
            com.Parameters.AddWithValue("@rfc", RFC);
            com.Parameters.AddWithValue("@correoelectronico", CorreoElectronico);
            com.Parameters.AddWithValue("@idusuario", IdUsuario);
           

            try
            {
                con.Open();
                filas = com.ExecuteNonQuery();
                con.Close();
                return filas;
            }
            catch (Exception)
            {
                con.Close();
                throw;
            }
        }
    }
}