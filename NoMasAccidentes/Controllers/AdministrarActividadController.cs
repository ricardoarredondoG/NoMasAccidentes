using NoMasAccidentes.Models;
using NoMasAccidentes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoMasAccidentes.Controllers
{
    public class AdministrarActividadController : Controller
    {
        // GET: AdministrarActividad
        [Authorize]
        public ActionResult Index(int pagina = 1, string nombre = "", string apellidoP = "", string direccion = "", string telefono = "", string correo = "", string tipo = "")
        {
            var cantidadRegistrosPorPagina = 4;
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var actividad = bd.ACTIVIDAD.ToList();

            //Busqueda por Nombre
            //if (nombre != "")
            /*{
                actividad = actividad.FindAll(x => x.FECHA_ACTIVIDAD.ToString());
            }*/
            var totalRegistros = actividad.Count();
            actividad = actividad.OrderBy(x => x.ID_ACTIVIDAD).Skip((pagina - 1) * cantidadRegistrosPorPagina).Take(cantidadRegistrosPorPagina).ToList();

              
            var modelo = new ActividadViewModel();

            modelo.actividad = actividad;
            //modelo.PaginaActual = pagina;
            //modelo.TotalDeRegistros = totalRegistros;
            //modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;


            return View(modelo);

        }

       
        
        
        /*public JsonResult Crear(ActividadViewModel actividad)
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            NoMasAccidentes.Models.ACTIVIDAD = new ACTIVIDAD();

            return 0;
        }*/

     
}
}
