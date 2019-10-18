using NoMasAccidentes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMasAccidentes.ViewModels
{
    public class RubroViewModel : BaseModelo
    {
        //public List<Models.RUBRO> rubro { get; set; }

        public int id_rubro { get; set; }
        public String nombre_rubro { get; set; }
        public String desc_rubro { get; set; }
        public String activo_rubro { get; set; }

    }

    
}