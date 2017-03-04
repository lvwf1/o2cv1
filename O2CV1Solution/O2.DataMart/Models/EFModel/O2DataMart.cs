using System.Data.Entity;

namespace O2.DataMart.Models.EFModel
{
    public partial class O2DataMart : DbContext
    {
        public O2DataMart()
            : base("name=O2DataMart")
        {
        }

        public virtual DbSet<HomeValueRanges> HomeValueRanges { get; set; }
        public virtual DbSet<LoanAmountRanges> LoanAmountRanges { get; set; }
        public virtual DbSet<PropertyValueRanges> PropertyValueRanges { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<StreetDirection> StreetDirections { get; set; }
        public virtual DbSet<BackBone> BackBones { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<County> Counties { get; set; }
        public virtual DbSet<InterestRateType> InterestRateTypes { get; set; }
        public virtual DbSet<LoanType> LoanTypes { get; set; }
        public virtual DbSet<Mortgage> Mortgages { get; set; }
        public virtual DbSet<MortgateType> MortgateTypes { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<Query> Queries { get; set; }
        public virtual DbSet<Feed> Feeds { get; set; }
        public virtual DbSet<FeedDetail> FeedDetails { get; set; }

        public virtual DbSet<LoanToValueRanges> LoanToValueRanges { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<BackBone>().HasRequired(t => t.Person).WithMany().HasForeignKey(t => t.PersonId);
            modelBuilder.Entity<BackBone>().HasRequired(t => t.Property).WithMany().HasForeignKey(t => t.PropertyId);
            modelBuilder.Entity<BackBone>().HasRequired(t => t.Mortgage).WithMany().HasForeignKey(t => t.MortgageId);
        }
    }
}
