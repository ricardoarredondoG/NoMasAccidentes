﻿using NoMasAccidentes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMasAccidentes.ViewModels
{
    public class IndexViewModel : BaseModelo
    {
        public List<Models.PERSONAL> personal { get; set; }

        public List<Models.CLIENTE> cliente { get; set; }
        
        public List<Models.ASISTENTE> asistente { get; set; }

        public List<Models.RUBRO> rubro { get; set; }

        public List<Models.PLAN> plan { get; set; }
        public List<Models.ACTIVIDAD> actividad { get; set; }

        public List <Models.FACTURA> factura { get; set; }
        public Models.FACTURA facturaPDF { get; set; }
        public List<Models.DETALLE_FACTURA> detalleFactura { get; set; }

        public List<Models.CONTRATO> contrato { get; set; }

        public List<Models.TIPO_ACTIVIDAD> tipo_actividad { get; set; }

        public List<Models.CHECKLIST> checklist { get; set; }

        public List<Models.TIPO_SOLICITUD> tipo_solicitud { get; set; }

        public List<Models.SOLICITUD> solicitud { get; set; }
    }

   
}