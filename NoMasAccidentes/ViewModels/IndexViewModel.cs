﻿using NoMasAccidentes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMasAccidentes.ViewModels
{
    public class IndexViewModel : BaseModelo
    {
        public List<Models.PERSONAL> personal { get; set; }

        public List<Models.CLIENTE> cliente { get; set; }
        
        public List<Models.ASISTENTE> asistente { get; set; }

        public List<Models.RUBRO> rubro { get; set; }

        public List<Models.PLAN> plan { get; set; }
    }

   
}