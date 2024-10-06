using DigAccess.Data.Entities;
using DigAccess.Data.Entities.Address;
using DigAccess.Data.Entities.Blind;
using DigAccess.Data.Entities.Feature;
using DigAccess.Data.Entities.Organisation;
using DigAccess.Data.Entities.Organisation.Organisation;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigAccess.Web.Data
{
    public class DigAccessDbContext : IdentityDbContext
    {
        public DigAccessDbContext(DbContextOptions<DigAccessDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BlindUserFeature>()
                .HasKey(x => new { x.BlindUserId, x.FeatureId });

            base.OnModelCreating(builder);
        }

        public virtual DbSet<OrganisationCompany> Organisations { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<BlindUser> BlindUsers { get; set; }
        public virtual DbSet<BlindUserLicence> BlindUsersLicences { get; set; }
        public virtual DbSet<BlindUserEmail> BlindUsersEmails { get; set; }
        public virtual DbSet<EmailSettings> EmailsSettings { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<BlindUserFeature> BlindUsersFeatures { get; set; }
        public virtual DbSet<BlindUserLog> BlindUsersLogs { get; set; }
        public virtual DbSet<MasterKey> MasterKeys { get; set; }


    }
}
