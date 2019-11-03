using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMasAccidentes.ViewModels
{
    public class ActividadViewModel
    {
        public int idActividad { get; set; }
        public DateTime fecha { get; set; }
        public String descripcion { get; set; }
        public int tipo { get; set; }
        public int personal { get; set; }
        public int cliente { get; set; }
        public int check { get; set; }
    }
}