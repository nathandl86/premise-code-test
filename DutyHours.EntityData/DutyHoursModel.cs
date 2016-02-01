namespace DutyHours.EntityData
{
    using System.Configuration;
    using System.Data.Entity;
    using System.Diagnostics;
    public partial class DutyHoursModel : DbContext
    {
        public DutyHoursModel()
            : base()
        {
            Configuration.LazyLoadingEnabled = false;

            var conn = ConfigurationManager.ConnectionStrings["DutyHours"];            
            Database.Connection.ConnectionString = conn.ConnectionString;

            Database.Log = message => Debug.WriteLine(message);
        }

        public virtual IDbSet<InstitutionAdmin> InstitutionAdmins { get; set; }
        public virtual IDbSet<InstitutionResident> InstitutionResidents { get; set; }
        public virtual IDbSet<Institution> Institutions { get; set; }
        public virtual IDbSet<ResidentShift> ResidentShifts { get; set; }
        public virtual IDbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InstitutionResident>()
                .HasMany(e => e.ResidentShifts)
                .WithRequired(e => e.InstitutionResident)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Institution>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Institution>()
                .HasMany(e => e.InstitutionAdmins)
                .WithRequired(e => e.Institution)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Institution>()
                .HasMany(e => e.InstitutionResidents)
                .WithRequired(e => e.Institution)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.MiddleName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.InstitutionAdmins)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.InstitutionResidents)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }       
    }
}
