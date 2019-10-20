﻿using NoMasAccidentes.Models;
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
        public ActionResult Index(int pagina = 1, String rut="",  String nombre="", int selectEstado = 0, int selectMes = 0, int selectAno = 0, DateTime? fechaVencimientoDesde = null, DateTime? fechaVencimientoHasta = null)
        {
            var cantidadRegistrosPorPagina = 4;
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();
            var factura = bd.FACTURA.ToList();

            //Busqueda por Rut
            if (rut != "")
            {
                factura = factura.FindAll(x => x.RUT_CLIENTE.Contains(rut));
            }

            //Busqueda por Nombre
            if (nombre != "")
            {
                factura = factura.FindAll(x => x.NOMBRE_CLIENTE.ToLower().Contains(nombre.ToLower()));
            }

            //Busqueda por año

            if (selectAno != 0)
            {
                factura = factura.FindAll(x => x.FECHA.Year == selectAno);
            }

            //Busqueda por Mes

            if (selectMes != 0)
            {
                factura = factura.FindAll(x => x.FECHA.Month == selectMes);
            }

            //Busqueda por Fecha Vencimiento Desde

            if (fechaVencimientoDesde != null)
            {
                factura = factura.FindAll(x => x.FECHA_VENCIMIENTO >= fechaVencimientoDesde);
            }

            //Busqueda por Fecha de Vencimiento Hasta
            
            if (fechaVencimientoHasta != null)
            {
                factura = factura.FindAll(x => x.FECHA_VENCIMIENTO <= fechaVencimientoHasta);
            }

            //Busqueda por Estado

            if (selectEstado == 1)
            {
                factura = factura.FindAll(x => x.PAGADO == "S");
            }else if(selectEstado == 2)
            {
                factura = factura.FindAll(x => x.PAGADO == "N");
            }else if(selectEstado == 3)
            {
                DateTime fechaActual = DateTime.Now;
                factura = factura.FindAll(x => x.PAGADO == "N" & x.FECHA_VENCIMIENTO <= fechaActual);
            }


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