using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace NoMasAccidentes.ViewModels
{
    public class TipoSolicitudViewModel
    {

        public int id_tipsolic { get; set; }
        public String nombre_tipsolic { get; set; }
        public String desc_tiposolic { get; set; }
    }

    public class TipoSolicitudList
    {
        public List<TipoSolicitudViewModel> listaTipoSolicitud { get; set; }


        int contar(string nombre)
        {
            var resultado= listaTipoSolicitud.Count(it => it.nombre_tipsolic == nombre);

            return resultado ;
        }


    }
}