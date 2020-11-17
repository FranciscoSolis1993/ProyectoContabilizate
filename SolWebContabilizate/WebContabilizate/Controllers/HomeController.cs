using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebContabilizate.Models;

namespace WebContabilizate.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {

                TempData["e"] = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public ActionResult Index(Usuario usuario)
        {

            try
            {
                Usuario vUsuario = new Usuario();
                var hashValue = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(usuario.Contraseña)).Select(s => s.ToString("x2")));
                vUsuario.Contraseña = hashValue;
                vUsuario = new BusContribuyentes().Login(usuario.NombreCompleto,usuario.Contraseña);
                if (vUsuario != null)
                {
                    Session["usuario"] = usuario;
                }
                else
                {
                    throw new ApplicationException("Usuario ó contraseña incorrectos");
                }

                return RedirectToAction("Menu");

            }
            catch (Exception ex)
            {

                TempData["e"] = ex.Message;
                return View();
            }
        }

        public ActionResult Menu()
        {
            return View();
        }
        public JsonResult ObtenerContribuyentes()
        {
            try
            {
                Usuario usuario = (Usuario)Session["usuario"];
                List<EntidadContribuyentes> ls = new List<EntidadContribuyentes>();
                BusContribuyentes obj = new BusContribuyentes();
                ls = obj.ObtenerContribuyentes(usuario.IdUsuario);
                return Json(new { lsj = ls, m = "ok" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { m = ex.Message }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        public JsonResult AgregarContribuyente(EntidadContribuyentes j)
        {
            try
            {
                Usuario usuario = (Usuario)Session["usuario"];
                usuario.IdUsuario = j.IdUsuario;
                j.RFC = ValidarRFC(j.RFC);
                BusContribuyentes obj = new BusContribuyentes();
                obj.AgregarContribuyente(j);
                return Json(new { m = "ok", msg = "Se Agrego el Contribuyente" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { m = ex.Message }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        private string ValidarRFC(string rfc)
        {

            try
            {
                if (rfc == null)
                {
                    throw new ApplicationException("El RFC ES OBLIGATORIO");

                }
                string resultado = rfc;
                string añoNacimiento = string.Empty;
                string mesNacimienti = string.Empty;
                string diaNacimienti = string.Empty;


                añoNacimiento = $@"{resultado[4]}{resultado[5]}";
                mesNacimienti = $@"{resultado[6]}{resultado[7]}";
                diaNacimienti = $@"{resultado[8]}{resultado[9]}";
                int año = Convert.ToInt32(añoNacimiento);
                int mes = Convert.ToInt32(mesNacimienti);
                int dia = Convert.ToInt32(diaNacimienti);
                int bisiesto = año % 4;
                if (año < 20)
                {
                    año = año + 2000;
                }
                else
                {
                    año = año + 1900;
                }
                añoNacimiento = Convert.ToString(año);
                if (mes > 12)
                {
                    throw new ApplicationException("El RFC esta Incorrecto");
                }
                if (bisiesto == 0)
                {
                    if (mes == 2)
                    {
                        if (dia > 29)
                        {
                            throw new ApplicationException("El RFC esta Incorrecto");

                        }
                    }
                }
                else
                {
                    if (mes == 2)
                    {
                        if (dia > 28)
                        {
                            throw new ApplicationException("El RFC esta Incorrecto");

                        }
                    }

                }
                if (mes == 1 || mes == 3 || mes == 5 || mes == 7 || mes == 8 || mes == 10 || mes == 12)
                {
                    if (dia > 31)
                    {
                        throw new ApplicationException("El RFC esta Incorrecto");
                    }

                }
                else
                {
                    if (dia > 30)
                    {
                        throw new ApplicationException("El RFC esta Incorrecto");
                    }
                }
                
                return rfc;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("El RFC esta Incorrecto");

            }

        }

        public JsonResult BorrarGet(int IdContribuyente)
        {
            try
            {
                EntidadContribuyentes e = new EntidadContribuyentes();
                BusContribuyentes obj = new BusContribuyentes();
                e = obj.ObtenerContribuyente(IdContribuyente);
                return Json(new { ent = e, m = "ok" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { m = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult BorrarPost(int IdContribuyentes)
        {
            try
            {
                BusContribuyentes obj = new BusContribuyentes();
                obj.BorrarPost(IdContribuyentes);
                return Json(new { m = "ok", msg = "Se dio de baja contribuyente" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { m = ex.Message }, JsonRequestBehavior.AllowGet);
                throw;

            }
        }

        public JsonResult EditarGet(int IdContribuyentes)
        {
            try
            {
                EntidadContribuyentes e = new EntidadContribuyentes();
                BusContribuyentes obj = new BusContribuyentes();
                e = obj.ObtenerContribuyente(IdContribuyentes);
                return Json(new { ent = e, m = "ok" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(new { m = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EditarPost(EntidadContribuyentes j)
        {
            try
            {
                BusContribuyentes obj = new BusContribuyentes();
                obj.EditarContribuyente(j);
                return Json(new { m = "ok", msg = "Se Edito contribuyente" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { m = ex.Message }, JsonRequestBehavior.AllowGet);
                throw;

            }
        }
    }
}