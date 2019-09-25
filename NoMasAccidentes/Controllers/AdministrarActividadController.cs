
using NoMasAccidentes.Models;
using NoMasAccidentes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoMasAccidentes.Controllers
{
    public class ActividadController : Controller
    {
        // GET: Actividad
        [Authorize]
        [HttpPost]
        public JsonResult Crear(ActividadViewModel actividad)
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            ACTIVIDAD actividades = new ACTIVIDAD();
            actividades.FECHA_ACTIVIDAD = actividad.fecha_actividad;
            actividades.DESCRIPCION_ACTIVIDAD = actividad.descripcion_actividad;
            actividades.PERSONAL_ID_PERSONAL = actividad.personal_id_personal;
            actividades.CLIENTE_ID_CLIENTE = actividad.cliente_id_cliente;
            actividades.TIPO_ACTIVIDAD_ID_TIPOACTIVI = actividad.tipo_actividad_id_tipoactivi;
            actividades.CHECKLIST_ID_CHECKLIST = actividad.checklist_id_checklist;

            bd.ACTIVIDAD.Add(actividades);
            bd.SaveChanges();
            return Json("d");



        }
    }
}
