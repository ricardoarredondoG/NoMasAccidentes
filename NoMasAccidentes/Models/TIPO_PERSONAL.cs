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
    
    public partial class TIPO_PERSONAL
    {
        public TIPO_PERSONAL()
        {
            this.USUARIO = new HashSet<USUARIO>();
        }
    
        public decimal ID_TIPOPERSONAL { get; set; }
        public string TIPO_PERSONAL1 { get; set; }
        public string DESC_TIPOPERSONAL { get; set; }
    
        public virtual ICollection<USUARIO> USUARIO { get; set; }
    }
}
