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
    
    public partial class TIPO_ACTIVIDAD
    {
        public TIPO_ACTIVIDAD()
        {
            this.ACTIVIDAD = new HashSet<ACTIVIDAD>();
        }
    
        public decimal ID_TIPOACTIVI { get; set; }
        public string TIPO_ACTIVIDAD1 { get; set; }
        public decimal VALOR_ACTIVIDAD { get; set; }
    
        public virtual ICollection<ACTIVIDAD> ACTIVIDAD { get; set; }
    }
}
