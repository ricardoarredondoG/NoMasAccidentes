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

            //Si es Personal
            if (tipo_usuario == 2)
            {
                var user = bd.PERSONAL.FirstOrDefault(e => e.USERNAME_PERSO == username && e.PASSWORD_PERSO == password && e.TIPO_PERSONAL.ID_TIPOPERSONAL == 2 && e.ACTIVO=="S");

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.USERNAME_PERSO, true);
                    Session["nombreApellido"] = user.NOMBRE_PERSO + " " + user.APELLIDOP_PERSO;
                    Session["tipoUsuario"] = 2;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", new { message = "*Los Datos Ingresados no son Validos " });
                }
            }
            //Si Es Administrador
            else if(tipo_usuario == 1)
            {
                var user = bd.PERSONAL.FirstOrDefault(e => e.USERNAME_PERSO == username && e.PASSWORD_PERSO == password && e.TIPO_PERSONAL.ID_TIPOPERSONAL == 1 && e.ACTIVO == "S");

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.USERNAME_PERSO, true);
                    Session["nombreApellido"] = user.NOMBRE_PERSO + " " + user.APELLIDOP_PERSO;
                    Session["tipoUsuario"] = 1;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", new { message = "*Los Datos Ingresados no son Validos " });
                }
            }
            //Si Es Cliente
            else 
            {
                var user = bd.CLIENTE.FirstOrDefault(e => e.USERNAME_CLIENTE == username && e.PASSWORD_CLIENTE == password );
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.USERNAME_CLIENTE, true);
                    Session["nombreApellido"] = user.NOMBRE_CLIENTE + " " + user.APELLIDO_CLIENTE;
                    Session["tipoUsuario"] = 3;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", new { message = "*Los Datos Ingresados no son Validos " });
                }
            }


        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }



    }
}