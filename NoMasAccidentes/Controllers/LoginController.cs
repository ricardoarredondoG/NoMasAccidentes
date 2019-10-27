using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NoMasAccidentes.Models;

namespace NoMasAccidentes.Controllers
{
    public class LoginController : Controller
    {
   

        public ActionResult Index(String message ="")
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        public ActionResult Ingresar(String username, String password)
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();

            var user = bd.USUARIO.FirstOrDefault(e => e.USUARIO1 == username && e.PASSWORD == password);
            if (user != null)
            {


                if(user.TIPO_USUARIO == 1 || user.TIPO_USUARIO == 2)
                {
                    var user1 = bd.PERSONAL.FirstOrDefault(e => e.USUARIO == user.ID_USUARIO);
                    

                    if (user1.ACTIVO.Equals("S"))
                    {
                        FormsAuthentication.SetAuthCookie(user.USUARIO1, true);
                        Session["nombreApellido"] = user1.NOMBRE_PERSO + " " + user1.APELLIDOP_PERSO;
                    }else
                    {
                        return RedirectToAction("Index", new { message = "*Usuario Inactivo" });
                    }

                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", new { message = "*Los Datos Ingresados no son Validos " });
            }


           


        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }



    }
}