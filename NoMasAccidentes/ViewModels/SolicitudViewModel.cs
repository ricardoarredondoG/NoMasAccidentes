using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMasAccidentes.ViewModels
{
    public class SolicitudViewModel
    {
        public int idSolicitud { get; set; }
        public int tipo_solicitud { get; set; }
        public String detalle_solicitud { get; set; }
        public String estado { get; set; }
    }
}
