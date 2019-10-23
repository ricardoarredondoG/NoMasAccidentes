using NoMasAccidentes.Models;
using NoMasAccidentes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoMasAccidentes.Controllers
{
    public class TipoActividadController : Controller
    {
        // GET: TipoActividad
        public ActionResult Index(int pagina = 1)
        {
            var cantidadRegistroPorPagina = 4;
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var tipo_actividad = bd.TIPO_ACTIVIDAD.ToList();


            var totalRegistros = tipo_actividad.Count();
            tipo_actividad = tipo_actividad.OrderBy(x => x.ID_TIPOACTIVI).Skip((pagina - 1) * cantidadRegistroPorPagina).Take(cantidadRegistroPorPagina).ToList();

            var modelo = new IndexViewModel();
            modelo.tipo_actividad = tipo_actividad;
            modelo.PaginaActual = pagina;
            modelo.RegistrosPorPagina = cantidadRegistroPorPagina;
            modelo.TotalDeRegistros = totalRegistros;



            return View(modelo);
        }

        
    }
}
