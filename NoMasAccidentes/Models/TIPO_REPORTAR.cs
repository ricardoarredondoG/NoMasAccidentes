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
    
    public partial class TIPO_REPORTAR
    {
        public TIPO_REPORTAR()
        {
            this.REPORTAR = new HashSet<REPORTAR>();
        }
    
        public decimal ID_TIPO_REPORTAR { get; set; }
        public string TIPO_REPORTAR1 { get; set; }
    
        public virtual ICollection<REPORTAR> REPORTAR { get; set; }
    }
}
