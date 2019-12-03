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
            //var columna3 = ContarPorNombre(ListaTipoSolicitud, "Crear");
            //Se puede agregar mas columnas dependiendo del tipo de solicitud que puedan existir

           // var columna3 = ContarPorNombre(ListaTipoSolicitud, "Reporte");
           //En los valores de x, luego del arreglo se pueden agregar el nombre de las columnas de los valores de y, las variables deben estar en este último.
           
            var caracterColumna = new Chart(width: 400, height: 400).AddSeries(chartType: "Column", xValue: new[] { "accidentes", "fiscalizacion"},
                yValues: new[] { columna1, columna2  }).GetBytes("png");

            
            return File(caracterColumna, "image/bytes");
        }

        public ActionResult crearPie()
        {
            var context = new EntitiesNoMasAccidentes();
            var ListaTipoSolicitud = context.TIPO_SOLICITUD.ToList();
            //Variables que llaman a la funcion con los parámetros para que estas funcionen.
            var columna1 = ContarPorNombre(ListaTipoSolicitud, "Reporte Accidente");
            var columna2 = ContarPorNombre(ListaTipoSolicitud, "Reporte Fiscalización");
            //var columna3 = ContarPorNombre(ListaTipoSolicitud, "Crear");
            //Se puede agregar mas columnas dependiendo del tipo de solicitud que puedan existir

            // var columna3 = ContarPorNombre(ListaTipoSolicitud, "Reporte");
            //En los valores de x, luego del arreglo se pueden agregar el nombre de las columnas de los valores de y, las variables deben estar en este último.

            var caracterColumna = new Chart(width: 400, height: 400).AddSeries(chartType: "Pie", xValue: new[] { "accidentes", "fiscalizacion" },
                yValues: new[] { columna1, columna2 }).GetBytes("png");


            return File(caracterColumna, "image/bytes");
        }



        private string ContarPorNombre(List<TIPO_SOLICITUD> lista, string v)
        {
           var result = lista.Count(it => it.NOMBRE_TIPOSOLICITUD == v);
            return Convert.ToString(result);
        }
        /*
        public ActionResult crearGraficoDeLinea()
        {
            var chart = new Chart(width: 600, height: 200).AddSeries(chartType: "line", xValue: new[] { "10", "50", "30", "70" },
            yValues: new[] { "50", "70", "90", "110" }).GetBytes("png");
            return File(chart, "image/byts");
        }
        */
      
        // GET: AdministrarEstadisticas
        public ActionResult Index()
        {
            return View("Index");
        }
       
       
        
    }
}