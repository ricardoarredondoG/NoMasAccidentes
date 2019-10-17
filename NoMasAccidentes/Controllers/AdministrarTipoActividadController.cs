using NoMasAccidentes.Models;
using NoMasAccidentes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoMasAccidentes.Controllers
{
    public class AdministrarTipoActividadController : Controller
    {
        // GET: AdministrarCliente
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

        [HttpPost]
        [Authorize]
        public JsonResult CrearTipoActividad(TipoActividadViewModel tipo_actividad)
        {
            //Validaciones.
            var resul = new baseRespuesta();
            resul.ok = true;
            if (tipo_actividad.tipo_actividad == null)
            {
                resul.mensaje = resul.mensaje + "<i class ='zmdi zmdi-alert-circle zmdi-hc-fw'></i>Ingrese fecha de inicio</br>";
                resul.ok = false;
            }

            if (resul.ok == true)
            {
                EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
                NoMasAccidentes.Models.TIPO_ACTIVIDAD tipo_actividadR = new TIPO_ACTIVIDAD();


                tipo_actividadR.TIPO_ACTIVIDAD1 = tipo_actividad.tipo_actividad;
                tipo_actividadR.VALOR_ACTIVIDAD = tipo_actividad.valor_actividad;


               

                bd.TIPO_ACTIVIDAD.Add(tipo_actividadR);
                bd.SaveChanges();
                resul.mensaje = "<i class='zmdi zmdi-check zmdi-hc-fw'></i>Tipo Actividad Registrado Correctamente</br>";
            }
            else
            {
                resul.mensaje = "<b>Error</b></br>" + resul.mensaje;
            }
            return Json(resul);
        }

        [HttpPost]
        [Authorize]
        public JsonResult EliminarTipoActividad(int id)
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var resul = new baseRespuesta();
            var tipo_actividad = bd.TIPO_ACTIVIDAD.Find(id);
            bd.Entry(tipo_actividad).State = System.Data.EntityState.Modified;
            bd.SaveChanges();
            resul.mensaje = "Contrato eliminado correctamente";
            return Json(resul);
        }

        public class baseRespuesta
        {
            public bool ok { get; set; }
            public string mensaje { get; set; }
        }
    }
}