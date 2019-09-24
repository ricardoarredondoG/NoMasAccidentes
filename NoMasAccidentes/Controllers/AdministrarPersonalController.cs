using NoMasAccidentes.Models;
using NoMasAccidentes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoMasAccidentes.Controllers
{
    public class AdministrarPersonalController : Controller
    {
        // GET: AdministrarPersonal
        public ActionResult Index(int pagina= 1, string nombre= "", string apellidoP="", string usuario="", string correo="")
        {
            var cantidadRegistrosPorPagina = 4;
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var personal = bd.PERSONAL.ToList();
           
            //Busqueda por Nombre
            if (nombre != "")
            {
                personal = personal.FindAll(x => x.NOMBRE_PERSO.ToLower().Contains(nombre.ToLower()));
            }
            //Busqueda por Apellido Paterno

            if (apellidoP != "")
            {
                personal = personal.FindAll(x => x.APELLIDOP_PERSO.ToLower().Contains(apellidoP.ToLower()));
            }

            //Busqueda por Usuario

            if (usuario != "")
            {
                personal = personal.FindAll(x => x.USERNAME_PERSO.ToLower().Contains(usuario.ToLower()));
            }

            //Busqueda por Correo Electronico

            if (correo != "")
            {
                personal = personal.FindAll(x => x.CORREO_PERSO.ToLower().Contains(correo.ToLower()));
            }


            var totalRegistros = personal.Count();
            personal = personal.OrderBy(x => x.ID_PERSONAL).Skip((pagina - 1) * cantidadRegistrosPorPagina).Take(cantidadRegistrosPorPagina).ToList();


            var modelo = new IndexViewModel();

            modelo.personal = personal;
            modelo.PaginaActual = pagina;
            modelo.TotalDeRegistros = totalRegistros;
            modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;


            return View(modelo);
        }
    }
}