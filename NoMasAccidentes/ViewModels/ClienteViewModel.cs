using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMasAccidentes.ViewModels
{
   

    public class ClienteViewModel
    {

    
        public string rut_cliente { get; set; }
        public string nombre_cliente { get; set; }
        public string apellido_cliente { get; set; }
        public string telefono_cliente { get; set; }
        public string direc_cliente { get; set; }
        public string correo_cliente { get; set; }
        public int rubro_id_rubro { get; set; }
        
        

    }
}