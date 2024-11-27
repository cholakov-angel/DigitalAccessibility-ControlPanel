using DigAccess.Data.Entities;
using DigAccess.Data.Entities.Address;
using DigAccess.Data.Entities.Blind;
using DigAccess.Data.Entities.Feature;
using DigAccess.Data.Entities.Organisation;
using DigAccess.Data.Entities.Organisation.Organisation;
using DigAccess.Data.Seeder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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

            CitySeeder citySeeder = new CitySeeder();
            citySeeder.Configure(builder.Entity<City>());

            EmailSettingsSeeder emailSettingsSeeder = new EmailSettingsSeeder();
            emailSettingsSeeder.Configure(builder.Entity<EmailSettings>());

            OrganisationSeeder organisationSeeder = new OrganisationSeeder();
            organisationSeeder.Configure(builder.Entity<OrganisationCompany>());

            OfficeSeeder officeSeeder = new OfficeSeeder();
            officeSeeder.Configure(builder.Entity<Office>());

            FeatureSeeder featureSeeder = new FeatureSeeder();
            featureSeeder.Configure(builder.Entity<Feature>());

            ApplicationUserSeeder applicationUserSeeder = new ApplicationUserSeeder();
            applicationUserSeeder.Configure(builder.Entity<ApplicationUser>());

            BlindUserSeeder blindUserSeeder = new BlindUserSeeder();
            blindUserSeeder.Configure(builder.Entity<BlindUser>());

            BlindUserFeatureSeeder blindUserFeatureSeeder = new BlindUserFeatureSeeder();
            blindUserFeatureSeeder.Configure(builder.Entity<BlindUserFeature>());

            BlindUserLicenceSeeder blindUserLicenceSeeder = new BlindUserLicenceSeeder();
            blindUserLicenceSeeder.Configure(builder.Entity<BlindUserLicence>());

            BlindUserLogSeeder blindUserLogSeeder = new BlindUserLogSeeder();
            blindUserLogSeeder.Configure(builder.Entity<BlindUserLog>());

            RolesSeeder rolesSeeder = new RolesSeeder();
            rolesSeeder.Configure(builder.Entity<IdentityRole>());

            ApplicationUserRoleSeeder applicationUserRoleSeeder = new ApplicationUserRoleSeeder();
            applicationUserRoleSeeder.Configure(builder.Entity<IdentityUserRole<string>>());
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
