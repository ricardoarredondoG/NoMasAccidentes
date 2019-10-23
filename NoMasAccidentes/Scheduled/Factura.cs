using NoMasAccidentes.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace NoMasAccidentes.Scheduled
{
    public class Factura
    {
        public void generarFactura()
        {
            EntitiesNoMasAccidentes bd = new EntitiesNoMasAccidentes();

            DateTime fechaActual = DateTime.Now;
            DateTime primerDiaMes = new DateTime(fechaActual.Year, fechaActual.Month - 1, 1);
            DateTime ultimoDiaMes = primerDiaMes.AddMonths(1).AddDays(-1);

            //Contratos Activos y con fecha de vencimiento menor a la fecha actual
            List<Models.CONTRATO> contratos = bd.CONTRATO.ToList().FindAll(x => x.ACTIVO.Equals("S") & x.FECHA_FIN_CONTRATO >= fechaActual);

            //Obtener actividades que se generaron entre el primer y ultimo dia del mes(Mes anterior ya que la factura se genera el primero de cada mes)
            List<Models.ACTIVIDAD> actividades = bd.ACTIVIDAD.ToList().FindAll(x => x.FECHA_ACTIVIDAD >= primerDiaMes && x.FECHA_ACTIVIDAD<= ultimoDiaMes);

            //Creacion de factura 
            NoMasAccidentes.Models.FACTURA factura = new FACTURA();

            foreach (var contrato in contratos)
            {
                //Buscar actividades del cliente del contrato
                var actividad = actividades.FindAll(x=> x.CLIENTE_ID_CLIENTE == contrato.CLIENTE_ID_CLIENTE);
                var totalActividades = actividad.Sum(x=> x.TIPO_ACTIVIDAD.VALOR_ACTIVIDAD);

                //Calcular Fecha de vencimiento
                DateTime fechaVencimiento = ultimoDiaMes.AddMonths(1);

                //Calcular Sub Total
                var subTtotal = contrato.PLAN.VALOR + totalActividades;

                //Calcular IVA
                var iva = Math.Round((subTtotal/100)*19);

                //Calcular Total 
                var total = iva + subTtotal;


                //Agregar Factura
                factura.CONTRATO_ID_CONTRATO = contrato.ID_CONTRATO;
                factura.FECHA = fechaActual;
                factura.PAGADO = "N";
                factura.SUBTOTAL = subTtotal;
                factura.TOTAL = total;
                factura.IVA = iva;
                factura.FECHA_VENCIMIENTO = fechaVencimiento;
                factura.NOMBRE_PLAN = contrato.PLAN.NOMBRE;
                factura.VALOR_PLAN = contrato.PLAN.VALOR;
                factura.NOMBRE_CLIENTE = contrato.CLIENTE.NOMBRE_CLIENTE +" "+ contrato.CLIENTE.APELLIDO_CLIENTE;
                factura.DIRECCION_CLIENTE = contrato.CLIENTE.DIREC_CLIENTE;
                factura.RUT_CLIENTE = contrato.CLIENTE.RUT_CLIENTE;
                factura.CORREO_ELECTRONICO_CLIENTE = contrato.CLIENTE.CORREO_CLIENTE;
                bd.FACTURA.Add(factura);
                bd.SaveChanges();

                //Detalle Factura

                foreach (var acti in actividad)
                {
                    //Creacion de detalleFactura 
                    NoMasAccidentes.Models.DETALLE_FACTURA detalleFactura = new DETALLE_FACTURA();

                    detalleFactura.NOMBRE_ACTIVIDAD_EXTRA = acti.TIPO_ACTIVIDAD.TIPO_ACTIVIDAD1;
                    detalleFactura.VALOR_ACTIVIDAD_EXTRA = acti.TIPO_ACTIVIDAD.VALOR_ACTIVIDAD;
                    detalleFactura.DETALLE_ACTIVIDAD = "Actividad Realizada el " + acti.FECHA_ACTIVIDAD.ToString("D") + " Por el Profesional a Cargo: " + acti.PERSONAL.NOMBRE_PERSO + " " + acti.PERSONAL.APELLIDOP_PERSO;
                    detalleFactura.FACTURA_ID_FACTURA = bd.FACTURA.Max(x=> x.ID_FACTURA);
                    bd.DETALLE_FACTURA.Add(detalleFactura);
                    bd.SaveChanges();

                }
            }


            
        }

    }
}