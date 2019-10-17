using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoMasAccidentes.ViewModels
{
    public class ContratoViewModel
    {
        public DateTime fecha_inicio_contrato { get; set; }
        public DateTime fecha_fin_contrato { get; set; }
        public int cliente_id_cliente { get; set; }
        public int plan_id_plan { get; set; }
    }
}