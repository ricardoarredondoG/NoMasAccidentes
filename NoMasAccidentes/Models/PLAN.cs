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
    
    public partial class PLAN
    {
        public PLAN()
        {
            this.CONTRATO = new HashSet<CONTRATO>();
        }
    
        public decimal ID_PLAN { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public decimal VALOR { get; set; }
        public string ACTIVO { get; set; }
    
        public virtual ICollection<CONTRATO> CONTRATO { get; set; }
    }
}
