using NoMasAccidentes.Models;
using NoMasAccidentes.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList.Mvc;

namespace NoMasAccidentes.Controllers
{
    public class AdministrarRubroController : Controller
    {
        [Authorize]
        [AccessDeniedAuthorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            return View();
        }
        // GET: AdministrarRubro
        [Authorize]
        [AccessDeniedAuthorize(Roles = "Administrador")]
        public ActionResult ListarRubro(int? page, string nomRub = "", string descRub = "")
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var rubro = bd.RUBRO.ToList();
            //Busqueda por Nombre
            if (nomRub != "")
            {
                rubro = rubro.FindAll(x => x.NOMBRE_RUBRO.ToLower().Contains(nomRub.ToLower()));
            }

            if (descRub != "")
            {
                rubro = rubro.FindAll(x => x.DESC_RUBRO.ToLower().Contains(descRub.ToLower()));
            }

            var modelo = new IndexViewModel();

            modelo.rubro = rubro;

            int pageSize = 4;

            int pageNumber = page ?? 1;

            return PartialView(modelo.rubro.ToPagedList(pageNumber, pageSize));

        }
        //public ActionResult Index(int pagina = 1, string nomRub = "", string descRub = "")
        //{
        //    var cantidadRegistrosPorPagina = 4;

        //    EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
        //    var rubro = bd.RUBRO.ToList();

        //    //Busqueda por Nombre
        //    if (nomRub != "")
        //    {
        //        rubro = rubro.FindAll(x => x.NOMBRE_RUBRO.ToLower().Contains(nomRub.ToLower()));
        //    }


        //    var totalRegistros = rubro.Count();
        //    rubro = rubro.OrderBy(x => x.ID_RUBRO).Skip((pagina - 1) * cantidadRegistrosPorPagina).Take(cantidadRegistrosPorPagina).ToList();


        //    var modelo = new IndexViewModel();

        //    modelo.rubro = rubro;
        //    modelo.PaginaActual = pagina;
        //    modelo.TotalDeRegistros = totalRegistros;
        //    modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;


        //    return View(modelo);


        //    //return View();
        //}




        [HttpPost]
        [Authorize]
        [AccessDeniedAuthorize(Roles = "Administrador")]
        public JsonResult Crear(RubroViewModel rub)
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            NoMasAccidentes.Models.RUBRO rubro = new RUBRO();

            var resultado = new baseRespuesta();
            resultado = validaciones(rub);
            if (resultado.ok == true)
            {


                rubro.NOMBRE_RUBRO = rub.nombre_rubro;
                rubro.DESC_RUBRO = rub.desc_rubro;
                rubro.ACTIVO_RUBRO = "S";

                //Eliminar espacios en Blanco
                var nombre = rub.nombre_rubro.Replace(" ", "");
                var descripcion = rub.desc_rubro.Replace(" ", "");


                bd.RUBRO.Add(rubro);
                bd.SaveChanges();

                resultado.mensaje = "<i class='zmdi zmdi-check zmdi-hc-fw'></i>Rubro Registrado Correctamente</br>";
            }
            else
            {
                resultado.mensaje = "<b>Error</b></br>" + resultado.mensaje;
            }
            return Json(resultado);

        }


        [HttpPost]
        [Authorize]
        [AccessDeniedAuthorize(Roles = "Administrador")]
        public JsonResult Editar(RubroViewModel rub)
        {
            var resultado = new baseRespuesta();
            resultado = validaciones(rub);
            //if (resultado.ok == true)
            //{
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            NoMasAccidentes.Models.RUBRO rubro = new RUBRO();
            var rubroid = bd.RUBRO.Find(rub.id_rubro);
            rubroid.NOMBRE_RUBRO = rub.nombre_rubro;
            rubroid.DESC_RUBRO = rub.desc_rubro;
            rubroid.ACTIVO_RUBRO = "S";
            bd.Entry(rubroid).State = System.Data.EntityState.Modified;
            bd.SaveChanges();
            resultado.mensaje = "<i class='zmdi zmdi-check zmdi-hc-fw'></i>Rubro Modificado Correctamente";

            //}

            return Json(resultado);
        }

        [HttpPost]
        [Authorize]
        [AccessDeniedAuthorize(Roles = "Administrador")]
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

        public class baseRespuesta
        {
            public bool ok { get; set; }
            public string mensaje { get; set; }
        }

        public baseRespuesta validaciones(RubroViewModel rubro)
        {
            var resultado = new baseRespuesta();
            resultado.ok = true;
            //Validaciones
            if (rubro.nombre_rubro == null)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese Nombre de Rubro</br>";
                resultado.ok = false;
            }
            if (rubro.desc_rubro == null)
            {
                resultado.mensaje = resultado.mensaje + "<i class='zmdi zmdi-alert-circle zmdi-hc-fw'></i> Ingrese Descripción del Rubro</br>";
                resultado.ok = false;
            }



            return resultado;
        }
    }
}