//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NoMasAccidentes.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PERSONAL
    {
        public PERSONAL()
        {
            this.ACTIVIDAD = new HashSet<ACTIVIDAD>();
        }
    
        public decimal ID_PERSONAL { get; set; }
        public string NOMBRE_PERSO { get; set; }
        public string APELLIDOP_PERSO { get; set; }
        public string APELLIDOM_PERSO { get; set; }
        public string USERNAME_PERSO { get; set; }
        public string PASSWORD_PERSO { get; set; }
        public string DIRECCION_PERSO { get; set; }
        public string TELEFONO_PERSO { get; set; }
        public string CORREO_PERSO { get; set; }
        public decimal TIPO_PERSONAL_ID_TIPOPERSONAL { get; set; }
    
        public virtual ICollection<ACTIVIDAD> ACTIVIDAD { get; set; }
        public virtual TIPO_PERSONAL TIPO_PERSONAL { get; set; }
    }
}
