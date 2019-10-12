using NoMasAccidentes.Models;
using NoMasAccidentes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoMasAccidentes.Controllers
{
    public class AdministrarPlanesController : Controller
    {
        // GET: AdministrarPlanes
        [Authorize]
        public ActionResult Index(int pagina=1, String nombrePlan="", int precioDesde=0, int precioHasta=0 )
        {
            var cantidadRegistrosPorPagina = 4;
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var plan = bd.PLAN.ToList();

            //Busqueda por Nombre
            if (nombrePlan != "")
            {
                plan = plan.FindAll(x => x.NOMBRE.ToLower().Contains(nombrePlan.ToLower()));
            }

            //Busqueda por Precio Desde
            if (precioDesde != 0)
            {
                plan = plan.FindAll(x => x.VALOR >= precioDesde);
            }
            //Busqueda por Precio Hasta
            if (precioHasta != 0)
            {
                plan = plan.FindAll(x => x.VALOR <= precioHasta);
            }

            var totalRegistros = plan.Count();
            plan = plan.OrderBy(x => x.ID_PLAN).Skip((pagina - 1) * cantidadRegistrosPorPagina).Take(cantidadRegistrosPorPagina).ToList();

            var modelo = new IndexViewModel();
            modelo.PaginaActual = pagina;
            modelo.TotalDeRegistros = totalRegistros;
            modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
            modelo.plan = plan;

            return View(modelo);
        }
    }
}