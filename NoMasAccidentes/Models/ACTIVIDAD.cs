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
    
    public partial class ACTIVIDAD
    {
        public ACTIVIDAD()
        {
            this.CAPACITACION = new HashSet<CAPACITACION>();
        }
    
        public decimal ID_ACTIVIDAD { get; set; }
        public System.DateTime FECHA_ACTIVIDAD { get; set; }
        public string DESCRIPCION_ACTIVIDAD { get; set; }
        public decimal PERSONAL_ID_PERSONAL { get; set; }
        public decimal CLIENTE_ID_CLIENTE { get; set; }
        public decimal TIPO_ACTIVIDAD_ID_TIPOACTIVI { get; set; }
        public decimal CHECKLIST_ID_CHECKLIST { get; set; }
        public string ACTIVO_ACTIVIDAD { get; set; }
    
        public virtual CHECKLIST CHECKLIST { get; set; }
        public virtual TIPO_ACTIVIDAD TIPO_ACTIVIDAD { get; set; }
        public virtual ICollection<CAPACITACION> CAPACITACION { get; set; }
        public virtual PERSONAL PERSONAL { get; set; }
        public virtual CLIENTE CLIENTE { get; set; }
    }
}
