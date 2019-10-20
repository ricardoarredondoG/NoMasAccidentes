using NoMasAccidentes.Models;
using NoMasAccidentes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoMasAccidentes.Controllers
{
    public class PersonalController : Controller
    {
        // GET: Personal
        public ActionResult Index(int pagina = 1)
        {
            var cantidadRegistroPorPagina = 4;
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var tipo_actividad = bd.TIPO_ACTIVIDAD.ToList();


            var totalRegistros = tipo_actividad.Count();
            tipo_actividad = tipo_actividad.OrderBy(x => x.ID_TIPOACTIVI).Skip((pagina - 1) * cantidadRegistroPorPagina).Take(cantidadRegistroPorPagina).ToList();

            var modelo = new IndexViewModel();
            modelo.tipo_actividad = tipo_actividad;
            modelo.PaginaActual = pagina;
            modelo.RegistrosPorPagina = cantidadRegistroPorPagina;
            modelo.TotalDeRegistros = totalRegistros;



            return View(modelo);
        }

        // GET: Personal/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Personal/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Personal/Create
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

        // GET: Personal/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Personal/Edit/5
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

        // GET: Personal/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Personal/Delete/5
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
