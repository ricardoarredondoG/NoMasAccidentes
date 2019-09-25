using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoMasAccidentes.ViewModels
{
    
        // GET: Actividad
        public class ActividadViewModel
        {
            public DateTime fecha_actividad { get; set; }
            public string descripcion_actividad { get; set; }
            public int personal_id_personal { get; set; }
            public int cliente_id_cliente { get; set; }
            public int tipo_actividad_id_tipoactivi { get; set; }
            public int checklist_id_checklist { get; set; }
        }


    
}
