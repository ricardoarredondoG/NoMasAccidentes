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
