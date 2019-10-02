using NoMasAccidentes.Models;
using NoMasAccidentes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoMasAccidentes.Controllers
{
    public class AdministrarRubroController : Controller
    {
        // GET: AdministrarRubro
        public ActionResult Index(int pagina = 1, string nomRub = "", string descRub = "")
        {
            var cantidadRegistrosPorPagina = 4;

            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var rubro = bd.RUBRO.ToList();

            //Busqueda por Nombre
            if (nomRub != "")
            {
                rubro = rubro.FindAll(x => x.NOMBRE_RUBRO.ToLower().Contains(nomRub.ToLower()));
            }


            var totalRegistros = rubro.Count();
            rubro = rubro.OrderBy(x => x.ID_RUBRO).Skip((pagina - 1) * cantidadRegistrosPorPagina).Take(cantidadRegistrosPorPagina).ToList();


            var modelo = new IndexViewModel();

            modelo.rubro = rubro;
            modelo.PaginaActual = pagina;
            modelo.TotalDeRegistros = totalRegistros;
            modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;


            return View(modelo);


            //return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult Eliminar(int id)
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var resultado = new baseRespuesta();

            var rubro = bd.RUBRO.Find(id);
            rubro.ACTIVO_RUBRO = "N";
            bd.Entry(rubro).State = System.Data.EntityState.Modified;
            bd.SaveChanges();
            resultado.mensaje = "Rubro Eliminado Correctamente";





            return Json(resultado);
        }

        [HttpPost]
        [Authorize]
        public JsonResult Crear(RubroViewModel rub)
        {


            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            NoMasAccidentes.Models.RUBRO rubro = new RUBRO();

            rubro.NOMBRE_RUBRO = rub.nombre_rubro;
            rubro.DESC_RUBRO = rub.desc_rubro;

            //Generar Usuario

            //Eliminar espacios en Blanco
            var nombre = rub.nombre_rubro.Replace(" ", "");
            var descripcion = rub.desc_rubro.Replace(" ", "");

            
            

            bd.RUBRO.Add(rubro);
            bd.SaveChanges();
            return Json("d");
        }
    }
}