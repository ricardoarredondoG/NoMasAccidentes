﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EntitiesNoMasAccidentes : DbContext
    {
        public EntitiesNoMasAccidentes()
            : base("name=EntitiesNoMasAccidentes")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ACTIVIDAD> ACTIVIDAD { get; set; }
        public DbSet<ASISTENTE> ASISTENTE { get; set; }
        public DbSet<CAPACITACION> CAPACITACION { get; set; }
        public DbSet<CHECKLIST> CHECKLIST { get; set; }
        public DbSet<CLIENTE> CLIENTE { get; set; }
        public DbSet<CONTRATO> CONTRATO { get; set; }
        public DbSet<OPCION_CHECKLIST> OPCION_CHECKLIST { get; set; }
        public DbSet<PERSONAL> PERSONAL { get; set; }
        public DbSet<REPORTAR> REPORTAR { get; set; }
        public DbSet<RUBRO> RUBRO { get; set; }
        public DbSet<SOLICITUD> SOLICITUD { get; set; }
        public DbSet<TIPO_ACTIVIDAD> TIPO_ACTIVIDAD { get; set; }
        public DbSet<TIPO_PERSONAL> TIPO_PERSONAL { get; set; }
        public DbSet<TIPO_REPORTAR> TIPO_REPORTAR { get; set; }
        public DbSet<TIPO_SOLICITUD> TIPO_SOLICITUD { get; set; }
        public DbSet<PLAN> PLAN { get; set; }
        public DbSet<DETALLE_FACTURA> DETALLE_FACTURA { get; set; }
        public DbSet<FACTURA> FACTURA { get; set; }
    }
}
