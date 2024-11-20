using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DigAccess.Application;

public partial class DigAccessAdminPanelContext : DbContext
{
    public DigAccessAdminPanelContext()
    {
    } // DigAccessAdminPanelContext

    public DigAccessAdminPanelContext(DbContextOptions<DigAccessAdminPanelContext> options)
        : base(options)
    {
    } // DigAccessAdminPanelContext

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<BlindUser> BlindUsers { get; set; }

    public virtual DbSet<BlindUsersEmail> BlindUsersEmails { get; set; }

    public virtual DbSet<BlindUsersFeature> BlindUsersFeatures { get; set; }

    public virtual DbSet<BlindUsersLicence> BlindUsersLicences { get; set; }

    public virtual DbSet<BlindUsersLog> BlindUsersLogs { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<EmailsSetting> EmailsSettings { get; set; }

    public virtual DbSet<Feature> Features { get; set; }

    public virtual DbSet<Office> Offices { get; set; }

    public virtual DbSet<Organisation> Organisations { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasIndex(e => e.QuestionId, "IX_Answers_QuestionId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Question).WithMany(p => p.Answers).HasForeignKey(d => d.QuestionId);
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.OfficeId, "IX_AspNetUsers_OfficeId");

            entity.HasIndex(e => e.PersonalId, "IX_AspNetUsers_PersonalId")
                .IsUnique()
                .HasFilter("([PersonalId] IS NOT NULL)");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.ApprovalStatus).HasDefaultValue(0);
            entity.Property(e => e.Discriminator)
                .HasMaxLength(21)
                .HasDefaultValue("");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.PersonalId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasOne(d => d.Office).WithMany(p => p.AspNetUsers).HasForeignKey(d => d.OfficeId);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<BlindUser>(entity =>
        {
            entity.HasIndex(e => e.AdministratorId, "IX_BlindUsers_AdministratorId");

            entity.HasIndex(e => e.CityId, "IX_BlindUsers_CityId");

            entity.HasIndex(e => e.PersonalId, "IX_BlindUsers_PersonalId")
                .IsUnique()
                .HasFilter("([PersonalId] IS NOT NULL)");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FirstName).HasMaxLength(60);
            entity.Property(e => e.LastName).HasMaxLength(60);
            entity.Property(e => e.MiddleName).HasMaxLength(60);
            entity.Property(e => e.PersonalId).HasMaxLength(10);
            entity.Property(e => e.Phone).HasMaxLength(10);
            entity.Property(e => e.Street).HasDefaultValue("");
            entity.Property(e => e.Telknumber).HasColumnName("TELKNumber");

            entity.HasOne(d => d.Administrator).WithMany(p => p.BlindUsers).HasForeignKey(d => d.AdministratorId);

            entity.HasOne(d => d.City).WithMany(p => p.BlindUsers).HasForeignKey(d => d.CityId);
        });

        modelBuilder.Entity<BlindUsersEmail>(entity =>
        {
            entity.HasIndex(e => e.BlindUserId, "IX_BlindUsersEmails_BlindUserId");

            entity.HasIndex(e => e.EmailSettingsId, "IX_BlindUsersEmails_EmailSettingsId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.BlindUser).WithMany(p => p.BlindUsersEmails).HasForeignKey(d => d.BlindUserId);

            entity.HasOne(d => d.EmailSettings).WithMany(p => p.BlindUsersEmails).HasForeignKey(d => d.EmailSettingsId);
        });

        modelBuilder.Entity<BlindUsersFeature>(entity =>
        {
            entity.HasKey(e => new { e.BlindUserId, e.FeatureId });

            entity.HasIndex(e => e.FeatureId, "IX_BlindUsersFeatures_FeatureId");

            entity.HasOne(d => d.BlindUser).WithMany(p => p.BlindUsersFeatures).HasForeignKey(d => d.BlindUserId);

            entity.HasOne(d => d.Feature).WithMany(p => p.BlindUsersFeatures).HasForeignKey(d => d.FeatureId);
        });

        modelBuilder.Entity<BlindUsersLicence>(entity =>
        {
            entity.HasIndex(e => e.BlindUserId, "IX_BlindUsersLicences_BlindUserId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.MacAddress).HasMaxLength(24);

            entity.HasOne(d => d.BlindUser).WithMany(p => p.BlindUsersLicences).HasForeignKey(d => d.BlindUserId);
        });

        modelBuilder.Entity<BlindUsersLog>(entity =>
        {
            entity.HasIndex(e => e.BlindUserId, "IX_BlindUsersLogs_BlindUserId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.BlindUser).WithMany(p => p.BlindUsersLogs).HasForeignKey(d => d.BlindUserId);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<EmailsSetting>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Feature>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Office>(entity =>
        {
            entity.HasIndex(e => e.CityId, "IX_Offices_CityId");

            entity.HasIndex(e => e.OrganisationId, "IX_Offices_OrganisationId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.LocalPhone).HasMaxLength(10);
            entity.Property(e => e.Street).HasDefaultValue("");

            entity.HasOne(d => d.City).WithMany(p => p.Offices).HasForeignKey(d => d.CityId);

            entity.HasOne(d => d.Organisation).WithMany(p => p.Offices).HasForeignKey(d => d.OrganisationId);
        });

        modelBuilder.Entity<Organisation>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.NationalPhone)
                .HasMaxLength(10)
                .HasColumnName("National_Phone");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Questions_UserId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.Questions).HasForeignKey(d => d.UserId);
        });

        OnModelCreatingPartial(modelBuilder);
    } // OnModelCreating

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
} // DigAccessAdminPanelContext
