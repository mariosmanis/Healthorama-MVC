//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Healthorama.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HealthoramaEntities : DbContext
    {
        public HealthoramaEntities()
            : base("name=HealthoramaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ADMIN> ADMIN { get; set; }
        public virtual DbSet<APPOINTMENT> APPOINTMENT { get; set; }
        public virtual DbSet<DOCTOR> DOCTOR { get; set; }
        public virtual DbSet<DOCTOR_AVAILABILITY> DOCTOR_AVAILABILITY { get; set; }
        public virtual DbSet<PATIENT> PATIENT { get; set; }
    }
}
