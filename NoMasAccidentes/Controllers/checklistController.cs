using NoMasAccidentes.Models;
using NoMasAccidentes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoMasAccidentes.Controllers
{
    public class checklistController : Controller
    {
        // GET: checklist
        public ActionResult Index(int pagina = 1, String desc="",String nombre="")
        {
            var cantidadRegistroPorPagina = 4;
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var checklist = bd.CHECKLIST.ToList();


            var totalRegistros = checklist.Count();
            checklist = checklist.OrderBy(x => x.ID_CHECKLIST).Skip((pagina - 1) * cantidadRegistroPorPagina).Take(cantidadRegistroPorPagina).ToList();

            var modelo = new IndexViewModel();
            modelo.checklist = checklist;
            modelo.PaginaActual = pagina;
            modelo.RegistrosPorPagina = cantidadRegistroPorPagina;
            modelo.TotalDeRegistros = totalRegistros;



            return View(modelo);
        }

        public JsonResult Crear(ChecklistViewModel check)
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            NoMasAccidentes.Models.CHECKLIST checklist = new CHECKLIST();
            checklist.NOMBRE_CHECKLIST = check.nombre;
            checklist.DESCRIPCION_CHECKLIST = check.desc;

            bd.CHECKLIST.Add(checklist);
            
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


    }
}
