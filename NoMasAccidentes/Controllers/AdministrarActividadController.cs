﻿using NoMasAccidentes.Models;
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
        public ActionResult Index(int pagina = 1, string descripcion = "", string fecha = "", string tipo = "", string cliente_id = "", string personal_id = "")
        {
            var cantidadRegistrosPorPagina = 4;
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var actividad = bd.ACTIVIDAD.ToList();
            var tipoActividad = bd.TIPO_ACTIVIDAD.ToList();
            var personal = bd.PERSONAL.ToList();
            var cliente = bd.CLIENTE.ToList();
            var check = bd.CHECKLIST.ToList();
            //Busqueda por Nombre
            //if (nombre != "")
            /*{
                actividad = actividad.FindAll(x => x.FECHA_ACTIVIDAD.ToString());
            }*/
            var totalRegistros = actividad.Count();
            actividad = actividad.OrderBy(x => x.ID_ACTIVIDAD).Skip((pagina - 1) * cantidadRegistrosPorPagina).Take(cantidadRegistrosPorPagina).ToList();


            var modelo = new IndexViewModel();

            modelo.actividad = actividad;
            modelo.PaginaActual = pagina;
            modelo.TotalDeRegistros = totalRegistros;
            modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
            modelo.tipo_actividad = tipoActividad;
            modelo.personal = personal;
            modelo.cliente = cliente;
            modelo.checklist = check;
            return View(modelo);

        }


        [HttpPost]
        [Authorize]

        public JsonResult Crear(ActividadViewModel actividad)
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            NoMasAccidentes.Models.ACTIVIDAD actividades = new ACTIVIDAD();

            actividades.DESCRIPCION_ACTIVIDAD = actividad.descripcion;
            actividades.FECHA_ACTIVIDAD = actividad.fecha;
            actividades.TIPO_ACTIVIDAD_ID_TIPOACTIVI = actividad.tipo;
            actividades.CHECKLIST_ID_CHECKLIST = actividad.check;
            actividades.PERSONAL_ID_PERSONAL = actividad.personal;
            actividades.CLIENTE_ID_CLIENTE = actividad.cliente;
            actividades.ACTIVO_ACTIVIDAD = "S";

            bd.ACTIVIDAD.Add(actividades);
            try
            {
                bd.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }

            }


            return Json("d");


        }

        [HttpPost]
        [Authorize]
        public JsonResult EliminarA(int id)
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var resul = new baseRespuesta();
            var actividad = bd.ACTIVIDAD.Find(id);
            actividad.ACTIVO_ACTIVIDAD = "N";
            bd.Entry(actividad).State = System.Data.EntityState.Modified;
            bd.SaveChanges();
            resul.mensaje = "Actividad eliminada correctamente";
            return Json(resul);
        }

        public class baseRespuesta
        {
            public bool ok { get; set; }
            public string mensaje { get; set; }
        }

        [HttpPost]
        [Authorize]
        public JsonResult Editar(ActividadViewModel actividad)
        {
            var resultado = new baseRespuesta();
            resultado = validaciones(actividad);
            if (resultado.ok == true)
            {
                EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            NoMasAccidentes.Models.ACTIVIDAD actividades = new ACTIVIDAD();
            var actividadid = bd.ACTIVIDAD.Find(actividad.idActividad);

            actividadid.DESCRIPCION_ACTIVIDAD = actividad.descripcion;
            actividadid.FECHA_ACTIVIDAD = actividad.fecha.Date;
            actividadid.TIPO_ACTIVIDAD_ID_TIPOACTIVI = actividad.tipo;
            actividadid.PERSONAL_ID_PERSONAL = actividad.personal;
            actividadid.CLIENTE_ID_CLIENTE = actividad.cliente;
            actividadid.CHECKLIST_ID_CHECKLIST = actividad.check;

            bd.Entry(actividadid).State = System.Data.EntityState.Modified;
            bd.SaveChanges();
            resultado.mensaje = "<i class='zmdi zmdi-check zmdi-hc-fw'></i>Actividad Modificada Correctamente";

            }

            return Json(resultado);
        }

        public baseRespuesta validaciones(ActividadViewModel act)
        {
            var resultado = new baseRespuesta();
            resultado.ok = true;
            //Validaciones
            if (act.descripcion == null)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese Descripción de Atividad</br>";
                resultado.ok = false;
            }
            if (act.fecha == null)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese Fecha de Actividad</br>";
                resultado.ok = false;
            }

            if (act.tipo == null)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese Tipo de Actividad</br>";
                resultado.ok = false;
            }

            if (act.personal == null)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese Personal de Actividad</br>";
                resultado.ok = false;
            }

            if (act.cliente == null)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese Cliente de Actividad</br>";
                resultado.ok = false;
            }

            if (act.check == null)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese Check de Actividad</br>";
                resultado.ok = false;
            }

            return resultado;
        }


    }
}
