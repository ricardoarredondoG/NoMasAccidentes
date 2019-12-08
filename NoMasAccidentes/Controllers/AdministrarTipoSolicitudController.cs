using NoMasAccidentes.Models;
using NoMasAccidentes.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoMasAccidentes.Controllers
{
    public class AdministrarTipoSolicitudController : Controller
    {
        // GET: AdministrarTipoSolicitud
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListarTipoSolicitud(int? page, string nombreSolicitud = "")
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var tipo_solicitud = bd.TIPO_SOLICITUD.ToList();

            //Busqueda por Tipo Solicitud
            if (nombreSolicitud != "")
            {
                tipo_solicitud = tipo_solicitud.FindAll(x => x.NOMBRE_TIPOSOLICITUD.ToLower().Contains(nombreSolicitud.ToLower()));
            }

            int pageSize = 5;

            int pageNumber = page ?? 1;

            return PartialView(tipo_solicitud.ToPagedList(pageNumber, pageSize));
        }


        [HttpPost]
        [Authorize]
        public JsonResult Crear(TipoSolicitudViewModel tipo_solicitud)
        {
            var resultado = new baseRespuesta();
            resultado = validaciones(tipo_solicitud);
            if (resultado.ok == true)
            {
                EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
                NoMasAccidentes.Models.TIPO_SOLICITUD tipo_solici = new TIPO_SOLICITUD();


                tipo_solici.NOMBRE_TIPOSOLICITUD = tipo_solicitud.nombre_tipsolic;
                tipo_solici.DESCRIPCION_TIPOSOLICITUD = tipo_solicitud.desc_tiposolic;
                tipo_solici.ACTIVO_TIPOSOLICITUD = "S";

                //Eliminar espacios en Blanco
                var nombre = tipo_solicitud.nombre_tipsolic.Replace(" ", "");
                var descripcion = tipo_solicitud.desc_tiposolic.Replace(" ", "");


                bd.TIPO_SOLICITUD.Add(tipo_solici);
                bd.SaveChanges();
                resultado.mensaje = "<i class='zmdi zmdi-check zmdi-hc-fw'></i>Tipo Solicitud Registrada Correctamente</br>";
            }
            else
            {
                resultado.mensaje = "<b>Error</b></br>" + resultado.mensaje;
            }

            return Json(resultado);
            }


        [HttpPost]
        [Authorize]
        public JsonResult Editar(TipoSolicitudViewModel sol)
        {
            var resultado = new baseRespuesta();
            resultado = validaciones(sol);
            if (resultado.ok == true)
            {
                EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            NoMasAccidentes.Models.TIPO_SOLICITUD tipo_solicitud = new TIPO_SOLICITUD();

            var tiposol = bd.TIPO_SOLICITUD.Find(sol.id_tipsolic);
            tiposol.NOMBRE_TIPOSOLICITUD = sol.nombre_tipsolic;
            tiposol.DESCRIPCION_TIPOSOLICITUD = sol.desc_tiposolic;
            //tiposol.ACTIVO_TIPOSOLICITUD = "S";
            bd.Entry(tiposol).State = System.Data.EntityState.Modified;
            bd.SaveChanges();
            resultado.mensaje = "<i class='zmdi zmdi-check zmdi-hc-fw'></i>Tipo Solicitud Modificada Correctamente";

            }

            return Json(resultado);
            
        }


        [HttpPost]
        [Authorize]
        public JsonResult Eliminar(int id)
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var resultado = new baseRespuesta();

            var tipSol = bd.TIPO_SOLICITUD.Find(id);
            tipSol.ACTIVO_TIPOSOLICITUD = "N";
            bd.Entry(tipSol).State = System.Data.EntityState.Modified;
            bd.SaveChanges();
            resultado.mensaje = "Tipo Solicitud Eliminada Correctamente";

            return Json(resultado);
        }

        public class baseRespuesta
        {
            public bool ok { get; set; }
            public string mensaje { get; set; }
        }

        public baseRespuesta validaciones(TipoSolicitudViewModel tipoSol)
        {
            var resultado = new baseRespuesta();
            resultado.ok = true;
            //Validaciones
            if (tipoSol.nombre_tipsolic == null)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese Tipo de Solicitud</br>";
                resultado.ok = false;
            }
            if (tipoSol.desc_tiposolic == null)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese Descripción de la Solicitud</br>";
                resultado.ok = false;
            }



            return resultado;
        }


    }
}