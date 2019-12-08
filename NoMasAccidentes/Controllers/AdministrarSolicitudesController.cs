﻿using NoMasAccidentes.Models;
using NoMasAccidentes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoMasAccidentes.Controllers
{
    public class AdministrarSolicitudesController : Controller
    {
        // GET: AdministrarSolicitudes
        public ActionResult Index()
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var modelo = new IndexViewModel();
            //Tipo Solicitud
            modelo.tipo_solicitud = bd.TIPO_SOLICITUD.ToList();
            return View(modelo);
        }
    }
}