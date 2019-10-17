using NoMasAccidentes.Models;
using NoMasAccidentes.ViewModels;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoMasAccidentes.Controllers
{
    public class AdministrarFacturaController : Controller
    {
        // GET: AdministrarFactura
        [Authorize]
        public ActionResult Index(int pagina = 1)
        {
            var cantidadRegistrosPorPagina = 4;
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var factura = bd.FACTURA.ToList();
            var totalRegistros = factura.Count();

            factura = factura.OrderBy(x => x.ID_FACTURA).Skip((pagina - 1) * cantidadRegistrosPorPagina).Take(cantidadRegistrosPorPagina).ToList();

            var modelo = new IndexViewModel();
            modelo.factura = factura;
            modelo.PaginaActual = pagina;
            modelo.TotalDeRegistros = totalRegistros;
            modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
            return View(modelo);
        }

        public ActionResult Factura(int idFactura = 0)
        {
            //Consultar Factura
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var modelo = new IndexViewModel();
            var factura = bd.FACTURA.Find(idFactura);
            modelo.facturaPDF = factura;

            //Consultar Detalle Factura

            var detalleFactura = bd.DETALLE_FACTURA.ToList().FindAll(x => x.FACTURA_ID_FACTURA == idFactura);
            modelo.detalleFactura = detalleFactura;
            return View(modelo);
        }

        
        public ActionResult FacturaPDF(int id = 0)
        {
            if(id != 0)
            {
                return new ActionAsPdf("Factura", new { idFactura = id })
                {
                    FileName = "Factura.pdf"
                };
            }else
            {
                return Index();
            }
            
        }
    }
}