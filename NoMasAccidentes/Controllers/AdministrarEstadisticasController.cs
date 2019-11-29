using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoMasAccidentes.Models;
using NoMasAccidentes.ViewModels;
using System.Collections;
using System.Web.Helpers;







namespace NoMasAccidentes.Controllers
{
    public class AdministrarEstadisticasController : Controller
    {
        [Authorize]
        [AccessDeniedAuthorize(Roles = "Administrador")]
        public ActionResult crearBarra()
        {
            var context = new EntitiesNoMasAccidentes();
            var ListaTipoSolicitud = context.TIPO_SOLICITUD.ToList();
            //Variables que llaman a la funcion con los parámetros para que estas funcionen.
            var columna1 = ContarPorNombre(ListaTipoSolicitud, "Reporte Accidente");
            var columna2 = ContarPorNombre(ListaTipoSolicitud, "Reporte Fiscalización");
            var columna3 = ContarPorNombre(ListaTipoSolicitud, "Crear");
            //Se puede agregar mas columnas dependiendo del tipo de solicitud que puedan existir

           // var columna3 = ContarPorNombre(ListaTipoSolicitud, "Reporte");
            var caracter = new Chart(width: 800, height: 600).AddSeries(chartType: "Column", xValue: new[] { "accidentes", "fiscalizacion", "Crear"},
                yValues: new[] { columna1, columna2, columna3  }).GetBytes("png");

            return File(caracter, "image/bytes");
        }

        private string ContarPorNombre(List<TIPO_SOLICITUD> lista, string v)
        {
           var result = lista.Count(it => it.NOMBRE_TIPOSOLICITUD == v);
            return Convert.ToString(result);
        }

        public ActionResult crearGraficoDeLinea()
        {
            var chart = new Chart(width: 600, height: 200).AddSeries(chartType: "line", xValue: new[] { "10", "50", "30", "70" },
            yValues: new[] { "50", "70", "90", "110" }).GetBytes("png");
            return File(chart, "image/byts");
        }

      
        // GET: AdministrarEstadisticas
        public ActionResult Index()
        {
            return View("Index");
        }
       
       
        
    }
}