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
    
    public partial class DETALLE_FACTURA
    {
        public decimal ID_DETALLE_FACTURA { get; set; }
        public string NOMBRE_ACTIVIDAD_EXTRA { get; set; }
        public decimal VALOR_ACTIVIDAD_EXTRA { get; set; }
        public decimal FACTURA_ID_FACTURA { get; set; }
        public string DETALLE_ACTIVIDAD { get; set; }
    
        public virtual FACTURA FACTURA { get; set; }
    }
}
