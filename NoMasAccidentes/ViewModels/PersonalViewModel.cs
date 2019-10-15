using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMasAccidentes.ViewModels
{
    public class PersonalViewModel
    {
        public int id_personal { get; set; }
        public String nombre_perso { get; set; }
        public String apellidop_perso { get; set; }
        public String apellidom_perso { get; set; }
        public String direccion_perso { get; set; }
        public String telefono_perso { get; set; }
        public String correo_pero { get; set; }
        public int tipo_personal { get; set; }

    }
}