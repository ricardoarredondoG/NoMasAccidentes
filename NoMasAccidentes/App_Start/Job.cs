using NoMasAccidentes.Scheduled;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NoMasAccidentes.App_Start
{
    public class Job : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            // Según el job que se vaya a ejecutar
            switch (context.JobDetail.Key.ToString())
            {
                case "app.generarFactura":

                    Factura clase = new Factura();

                    clase.generarFactura();

                    break;
            }
        }
    }
}