//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
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
        public string DIRECCION_PERSO { get; set; }
        public string TELEFONO_PERSO { get; set; }
        public string CORREO_PERSO { get; set; }
        public string ACTIVO { get; set; }
        public decimal USUARIO { get; set; }
    
        public virtual ICollection<ACTIVIDAD> ACTIVIDAD { get; set; }
        public virtual USUARIO USUARIO1 { get; set; }
    }
}
