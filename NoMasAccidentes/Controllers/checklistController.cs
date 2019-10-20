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
        public ActionResult Index(int pagina = 1)
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

        // GET: checklist/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: checklist/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: checklist/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: checklist/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: checklist/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: checklist/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: checklist/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
