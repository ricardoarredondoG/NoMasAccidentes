using NoMasAccidentes.Models;
using NoMasAccidentes.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NoMasAccidentes.Controllers
{
    public class AdministrarSolicitudesController : Controller
    {
        // GET: AdministrarSolicitudes
        [Authorize]
        [AccessDeniedAuthorize(Roles = "Administrador, Cliente")]
        public ActionResult Index()
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var modelo = new IndexViewModel();
            //Tipo Solicitud
            modelo.tipo_solicitud = bd.TIPO_SOLICITUD.ToList();
            modelo.tipo_solicitud.Where(x => x.ACTIVO_TIPOSOLICITUD == "S");
            return View(modelo);
        }

        [Authorize]
        [AccessDeniedAuthorize(Roles = "Administrador, Cliente")]
        public ActionResult ListarSolicitud(int? page, String nombreCliente = "", int tipoSolicitud = 0, String estado = "", DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var solicitudes = bd.SOLICITUD.ToList();
            //Busqueda por rol
            if (Roles.IsUserInRole("Cliente"))
            {
                solicitudes = solicitudes.FindAll(x => x.CLIENTE.USUARIO.USUARIO1.Equals(User.Identity.Name));
            }
            //Busqueda por Nombre
            if (nombreCliente != "" && nombreCliente != "undefined")
            {
                solicitudes = solicitudes.FindAll(x => x.CLIENTE.NOMBRE_CLIENTE.ToLower().Contains(nombreCliente.ToLower()));
            }
            int pageSize = 4;

            int pageNumber = page ?? 1;

            return PartialView(solicitudes.ToPagedList(pageNumber, pageSize));
        }

        public JsonResult Crear(SolicitudViewModel solicitud)
        {
            var resultado = new baseRespuesta();
            resultado = validaciones(solicitud);

            if (resultado.ok == true)
            {
                EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
                NoMasAccidentes.Models.SOLICITUD solicitudes = new SOLICITUD();

                solicitudes.TIPO_SOLICITUD_ID_TIPOSOLICI = solicitud.tipo_solicitud;
                solicitudes.DESCRIPCION_SOLICITUD = solicitud.detalle_solicitud;
                solicitudes.ACTIVO_SOLICITUD = "S";
                solicitudes.FECHA_SOLICITUD =  DateTime.Now;
                var idCliente = bd.CLIENTE.FirstOrDefault(e => e.USUARIO.USUARIO1 == User.Identity.Name).ID_CLIENTE;
                solicitudes.CLIENTE_ID_CLIENTE = idCliente;
                solicitudes.ESTADO = "En Revisión";


                bd.SOLICITUD.Add(solicitudes);
                bd.SaveChanges();
                resultado.mensaje = "<i class='zmdi zmdi-check zmdi-hc-fw'></i>Solicitud Registrada Correctamente</br>";;

            }
            else
            {
                resultado.mensaje = "<b>Error</b></br>" + resultado.mensaje;
            }
            return Json(resultado);
        }

        [HttpPost]
        [Authorize]
        public JsonResult CambiarEstado(SolicitudViewModel solicitud)
        {

                EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();

                var solicitudId = bd.SOLICITUD.Find(solicitud.idSolicitud);
                solicitudId.ESTADO = solicitud.estado;

                bd.Entry(solicitudId).State = System.Data.EntityState.Modified;
                bd.SaveChanges();
                var mensaje = "Estado Modificado Correctamente";

            

                return Json(mensaje);
        }

        public class baseRespuesta
        {
            public bool ok { get; set; }
            public string mensaje { get; set; }
        }

        public baseRespuesta validaciones(SolicitudViewModel solicitud)
        {
            var resultado = new baseRespuesta();
            resultado.ok = true;
            //Validaciones
            if (solicitud.detalle_solicitud == null)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese El Detalle de la Solicitud</br>";
                resultado.ok = false;
            }
            if (solicitud.tipo_solicitud == 0)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Seleccione un Tipo de Solicitud</br>";
                resultado.ok = false;
            }
            return resultado;
        }

    }
}