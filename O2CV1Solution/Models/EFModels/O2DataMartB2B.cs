using System.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using O2V1Web.Models.EFModels;

namespace O2V1Web.Models.EFModels
{
    public partial class O2DataMartB2B : DbContext
    {
        public O2DataMartB2B()
            : base("name=O2DataMartB2B")
        {
        }

        public virtual DbSet<B2B1022> B2B1022 { get; set; }

        public virtual DbSet<Query> Queries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            //modelBuilder.Entity<BackBone>().HasRequired(t => t.Person).WithMany().HasForeignKey(t => t.PersonId);
            //modelBuilder.Entity<BackBone>().HasRequired(t => t.Property).WithMany().HasForeignKey(t => t.PropertyId);
            //modelBuilder.Entity<BackBone>().HasRequired(t => t.Mortgage).WithMany().HasForeignKey(t => t.MortgageId);
        }
    }
}
