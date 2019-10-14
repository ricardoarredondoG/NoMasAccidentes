using NoMasAccidentes.Models;
using NoMasAccidentes.ViewModels;
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
        public ActionResult Index(int pagina=1, String nombrePlan="", String precioDesde="", String precioHasta="" )
        {
            var cantidadRegistrosPorPagina = 4;
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var plan = bd.PLAN.ToList();

            //Busqueda por Nombre
            if (nombrePlan != "")
            {
                plan = plan.FindAll(x => x.NOMBRE.ToLower().Contains(nombrePlan.ToLower()));
            }

            //Busqueda por Precio Desde
            if (!precioDesde.Equals("") )
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

            var totalRegistros = plan.Count();
            plan = plan.OrderBy(x => x.ID_PLAN).Skip((pagina - 1) * cantidadRegistrosPorPagina).Take(cantidadRegistrosPorPagina).ToList();

            var modelo = new IndexViewModel();
            modelo.PaginaActual = pagina;
            modelo.TotalDeRegistros = totalRegistros;
            modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
            modelo.plan = plan;

            return View(modelo);
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
            resultado.mensaje = "Plan Eliminado Correctamente";
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
            var parseValor = 0;
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