using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoMasAccidentes.Models;

namespace NoMasAccidentes.Controllers
{
    public class HomeController : Controller
    {
   

        public ActionResult Login(String message ="")
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
                return RedirectToAction("Login", new { message = " " });
            }
            else
            {
                return RedirectToAction("Login", new { message = "*Los Datos Ingresados no son Validos " });
            }


            
        }


    }
}