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
    
    public partial class SOLICITUD
    {
        public decimal ID_SOLICITUD { get; set; }
        public System.DateTime FECHA_SOLICITUD { get; set; }
        public string DESCRIPCION_SOLICITUD { get; set; }
        public decimal TIPO_SOLICITUD_ID_TIPOSOLICI { get; set; }
        public decimal CLIENTE_ID_CLIENTE { get; set; }
        public string ACTIVO_SOLICITUD { get; set; }
    
        public virtual TIPO_SOLICITUD TIPO_SOLICITUD { get; set; }
        public virtual CLIENTE CLIENTE { get; set; }
    }
}
