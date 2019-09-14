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
        public ActionResult Ingresar(String username, String password, int tipo_usuario)
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();

            var user = bd.PERSONAL.FirstOrDefault(e => e.USERNAME_PERSO == username && e.PASSWORD_PERSO == password);

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.USERNAME_PERSO, true);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", new { message = "*Los Datos Ingresados no son Validos " });
            }


            
        }


    }
}