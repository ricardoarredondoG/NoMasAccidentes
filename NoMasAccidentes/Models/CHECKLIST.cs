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
    
    public partial class CHECKLIST
    {
        public CHECKLIST()
        {
            this.ACTIVIDAD = new HashSet<ACTIVIDAD>();
            this.OPCION_CHECKLIST = new HashSet<OPCION_CHECKLIST>();
        }
    
        public decimal ID_CHECKLIST { get; set; }
        public string NOMBRE_CHECKLIST { get; set; }
        public string DESCRIPCION_CHECKLIST { get; set; }
    
        public virtual ICollection<ACTIVIDAD> ACTIVIDAD { get; set; }
        public virtual ICollection<OPCION_CHECKLIST> OPCION_CHECKLIST { get; set; }
    }
}
