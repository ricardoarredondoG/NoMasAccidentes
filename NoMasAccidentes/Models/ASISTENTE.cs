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
    
    public partial class ASISTENTE
    {
        public ASISTENTE()
        {
            this.CAPACITACION = new HashSet<CAPACITACION>();
        }
    
        public decimal ID_ASISTENTE { get; set; }
        public string NOMBRE_ASISTENTE { get; set; }
        public string APELLIDOM_ASISTENTE { get; set; }
        public string APELLIDOP_ASISTENTE { get; set; }
        public decimal CLIENTE_ID_CLIENTE { get; set; }
    
        public virtual CLIENTE CLIENTE { get; set; }
        public virtual ICollection<CAPACITACION> CAPACITACION { get; set; }
    }
}
