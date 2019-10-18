using NoMasAccidentes.Models;
using NoMasAccidentes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoMasAccidentes.Controllers
{
    public class ContratoController : Controller
    {
        // GET: AdministrarCliente
        public ActionResult Index(int pagina = 1)
        {
            var cantidadRegistroPorPagina = 4;
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var contrato = bd.CONTRATO.ToList();


            var totalRegistros = contrato.Count();
            contrato = contrato.OrderBy(x => x.ID_CONTRATO).Skip((pagina - 1) * cantidadRegistroPorPagina).Take(cantidadRegistroPorPagina).ToList();

            var modelo = new IndexViewModel();
            modelo.contrato = contrato;
            modelo.PaginaActual = pagina;
            modelo.RegistrosPorPagina = cantidadRegistroPorPagina;
            modelo.TotalDeRegistros = totalRegistros;


            //Cliente
            modelo.cliente = bd.CLIENTE.ToList();
            //Plan
            modelo.plan = bd.PLAN.ToList();


            return View(modelo);
        }

        [HttpPost]
        [Authorize]
        public JsonResult CrearContrato(ContratoViewModel contrato)
        {
            //Validaciones.
            var resul = new baseRespuesta();
            resul.ok = true;
            if (contrato.fecha_inicio_contrato == null)
            {
                resul.mensaje = resul.mensaje + "<i class ='zmdi zmdi-alert-circle zmdi-hc-fw'></i>Ingrese fecha de inicio</br>";
                resul.ok = false;
            }
            if (contrato.fecha_fin_contrato == null)
            {
                resul.mensaje = resul.mensaje + "<i class ='zmdi zmdi-alert-circle zmdi-hc-fw'></i>Ingrese fecha de termino</br>";
                resul.ok = false;
            }

            if (resul.ok == true)
            {
                EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
                NoMasAccidentes.Models.CONTRATO contratoR = new CONTRATO();


                contratoR.FECHA_INICIO_CONTRATO = contrato.fecha_inicio_contrato;
                contratoR.FECHA_FIN_CONTRATO = contrato.fecha_fin_contrato;
                contratoR.CLIENTE_ID_CLIENTE = contrato.cliente_id_cliente;
                contratoR.PLAN_ID_PLAN = contrato.plan_id_plan;
                
                contratoR.ACTIVO = "S";

                bd.CONTRATO.Add(contratoR);
                bd.SaveChanges();
                resul.mensaje = "<i class='zmdi zmdi-check zmdi-hc-fw'></i>Contrato Registrado Correctamente</br>";
            }
            else
            {
                resul.mensaje = "<b>Error</b></br>" + resul.mensaje;
            }
            return Json(resul);
        }

        [HttpPost]
        [Authorize]
        public JsonResult EliminarContrato(int id)
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var resul = new baseRespuesta();
            var contrato = bd.CONTRATO.Find(id);
            contrato.ACTIVO = "N";
            bd.Entry(contrato).State = System.Data.EntityState.Modified;
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