using NoMasAccidentes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoMasAccidentes.ViewModels
{
    public class IndexViewModel : BaseModelo
    {
        public List<Models.PERSONAL> personal { get; set; }
    }
}