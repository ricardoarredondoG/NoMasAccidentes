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
        public ActionResult Index(int pagina=1, String nombreTipoContrato="", int precioDesde=0, int precioHasta=0 )
        {
            var cantidadRegistrosPorPagina = 4;
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var tipoContrato = bd.TIPO_CONTRATO.ToList();

            //Busqueda por Nombre
            if (nombreTipoContrato != "")
            {
                tipoContrato = tipoContrato.FindAll(x => x.NOMBRE.ToLower().Contains(nombreTipoContrato.ToLower()));
            }

            //Busqueda por Precio Desde
            if (precioDesde != 0)
            {
                tipoContrato = tipoContrato.FindAll(x => x.VALOR >= precioDesde);
            }
            //Busqueda por Precio Hasta
            if (precioHasta != 0)
            {
                tipoContrato = tipoContrato.FindAll(x => x.VALOR <= precioHasta);
            }

            var totalRegistros = tipoContrato.Count();
            tipoContrato = tipoContrato.OrderBy(x => x.ID_TIPO_CONTRATO).Skip((pagina - 1) * cantidadRegistrosPorPagina).Take(cantidadRegistrosPorPagina).ToList();

            var modelo = new IndexViewModel();
            modelo.PaginaActual = pagina;
            modelo.TotalDeRegistros = totalRegistros;
            modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
            modelo.tipoContrato = tipoContrato;

            return View(modelo);
        }
    }
}