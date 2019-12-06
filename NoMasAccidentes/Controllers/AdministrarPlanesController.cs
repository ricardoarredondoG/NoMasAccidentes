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
    public class AdministrarPlanesController : Controller
    {
        // GET: AdministrarPlanes
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ListarPlanes(int? page, String nombrePlan = "", String precioDesde = "", String precioHasta = "")
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var plan = bd.PLAN.ToList();

            //Busqueda por Nombre
            if (nombrePlan != "")
            {
                plan = plan.FindAll(x => x.NOMBRE.ToLower().Contains(nombrePlan.ToLower()));
            }

            //Busqueda por Precio Desde
            if (!precioDesde.Equals(""))
            {
                precioDesde = precioDesde.Replace(".", "");
                var parseprecioDesde = Int32.Parse(precioDesde);
                plan = plan.FindAll(x => x.VALOR >= parseprecioDesde);
            }
            //Busqueda por Precio Hasta
            if (!precioHasta.Equals(""))
            {
                precioHasta = precioHasta.Replace(".", "");
                var parsePrecioHasta = Int32.Parse(precioHasta);
                plan = plan.FindAll(x => x.VALOR <= parsePrecioHasta);
            }

            int pageSize = 4;
            int pageNumber = page ?? 1;

            return PartialView(plan.ToPagedList(pageNumber, pageSize));
        }

        [Authorize]
        public JsonResult Crear(PlanViewModel plan)
        {
            var resultado = new baseRespuesta();
            resultado = validaciones(plan);

            if (resultado.ok == true)
            {
                EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
                NoMasAccidentes.Models.PLAN planes = new PLAN();

                planes.NOMBRE = plan.nombre;
                planes.VALOR = Int32.Parse(plan.valor);
                planes.DESCRIPCION = plan.descripcion;
                planes.ACTIVO = "S";

                bd.PLAN.Add(planes);
                bd.SaveChanges();
                resultado.mensaje = "<i class='zmdi zmdi-check zmdi-hc-fw'></i>Plan Registrado Correctamente</br>";

            }
            else
            {
                resultado.mensaje = "<b>Error</b></br>" + resultado.mensaje;
            }
            return Json(resultado);
        }

        [HttpPost]
        [Authorize]
        public JsonResult Eliminar(int id)
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var resultado = new baseRespuesta();

            var plan = bd.PLAN.Find(id);
            plan.ACTIVO = "N";
            bd.Entry(plan).State = System.Data.EntityState.Modified;
            bd.SaveChanges();
            resultado.mensaje = "<b>Plan Eliminado Correctamente</b><br>Nota: Los planes Eliminados Seguiran estando disponibles para los contratos Asignados";
            return Json(resultado);
        }

        [HttpPost]
        [Authorize]
        public JsonResult Editar(PlanViewModel plan)
        {
            var resultado = new baseRespuesta();
            resultado = validaciones(plan);
            if (resultado.ok == true)
            {
                EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
                var planId = bd.PLAN.Find(plan.id_plan);
                planId.NOMBRE = plan.nombre;
                planId.DESCRIPCION = plan.descripcion;
                planId.VALOR = Int32.Parse(plan.valor);
                bd.Entry(planId).State = System.Data.EntityState.Modified;
                bd.SaveChanges();
                resultado.mensaje = "<i class='zmdi zmdi-check zmdi-hc-fw'></i>Plan Modificado Correctamente";

            }

            return Json(resultado);
        }

        public class baseRespuesta
        {
            public bool ok { get; set; }
            public string mensaje { get; set; }
        }

        public baseRespuesta validaciones(PlanViewModel plan)
        {
            var resultado = new baseRespuesta();
            resultado.ok = true;
            //Validaciones
            if (plan.nombre == null)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese Nombre</br>";
                resultado.ok = false;
            }


            if(plan.valor == null)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese un Valor</br>";
                resultado.ok = false;
            }else
            {
                plan.valor = plan.valor.Replace(".","");
                if (Int32.Parse(plan.valor) <= 0)
                {
                    resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese un Valor Valido</br>";
                    resultado.ok = false;
                }
            }

            
            if (plan.descripcion == null)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese Descripcion</br>";
                resultado.ok = false;
            }
            return resultado;
        }

    }
}