﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMasAccidentes.ViewModels
{
    public class PlanViewModel
    {
        public int id_plan { get; set; }
        public String nombre { get; set; }
        public String valor { get; set; }
        public String descripcion { get; set; }
    }
}