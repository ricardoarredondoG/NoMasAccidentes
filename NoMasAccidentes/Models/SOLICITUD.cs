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
    
    public partial class SOLICITUD
    {
        public decimal ID_SOLICITUD { get; set; }
        public System.DateTime FECHA_SOLICITUD { get; set; }
        public string DESCRIPCION_SOLICITUD { get; set; }
        public decimal TIPO_SOLICITUD_ID_TIPOSOLICI { get; set; }
        public decimal CLIENTE_ID_CLIENTE { get; set; }
        public string ACTIVO_SOLICITUD { get; set; }
        public string ESTADO { get; set; }
    
        public virtual TIPO_SOLICITUD TIPO_SOLICITUD { get; set; }
        public virtual CLIENTE CLIENTE { get; set; }
    }
}
