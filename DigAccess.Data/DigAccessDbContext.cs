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
        } // DigAccessDbContext

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BlindUserFeature>()
                .HasKey(x => new { x.BlindUserId, x.FeatureId });

            builder.Entity<BlindUserLicence>()
                .Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.Entity<BlindUser>()
                .HasIndex(x => x.PersonalId)
                .IsUnique(true);

            builder.Entity<ApplicationUser>()
                .HasIndex(x => x.PersonalId)
                .IsUnique(true);

            builder.Entity<ApplicationUser>()
                .Property(x => x.ApprovalStatus)
                .HasDefaultValue(0);
            base.OnModelCreating(builder);

            builder.Entity<Question>()
                .Property(x=> x.IsAnswered)
                .HasDefaultValue(false);

            builder.Entity<Answer>()
                .Property(x=> x.IsReviewed)
                .HasDefaultValue(false);
        } // OnModelCreating

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
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }



    } // DigAccessDbContext
}
