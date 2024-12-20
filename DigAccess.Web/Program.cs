﻿using DigAccess.Data.Entities;
using DigAccess.Data.Entities.Address;
using DigAccess.Data.Entities.Blind;
using DigAccess.Data.Entities.Feature;
using DigAccess.Data.Entities.Organisation.Organisation;
using DigAccess.Data.Entities.Organisation;
using DigAccess.Data.Seeder;
using DigAccess.Interfaces;
using DigAccess.Services;
using DigAccess.Services.Interfaces;
using DigAccess.Services.OfficeAdministrator;
using DigAccess.Services.OrgAdministrator;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DigAccess.Services.UserAdministrator;
using DigAccess.Services.Admin;

namespace DigAccess.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<DigAccessDbContext>(options =>
               {
                   options.UseSqlServer(connectionString);
                   // Сийдване на информацията
               });

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services
                .AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DigAccessDbContext>();
            builder.Services.AddScoped<IService, BaseService>();
            builder.Services.AddScoped<IBlindUserService, BlindUserService>();
            builder.Services.AddScoped<ILicenseService, LicenceService>();
            builder.Services.AddScoped<IMasterKeyService, MasterKeyService>();
            builder.Services.AddScoped<IUserAdministratorService, UserAdministratorService>();
            builder.Services.AddScoped<IEmailSettingsService, EmailSettingsService>();
            builder.Services.AddScoped<IWaitingApprovalService, WaitingApprovalService>();
            builder.Services.AddScoped<IOfficeWorkerService, OfficeWorkerService>();
            builder.Services.AddScoped<IQuestionService, QuestionService>();
            builder.Services.AddScoped<IQuestionOfficeWorkerService, QuestionOfficeWorkerService>();
            builder.Services.AddScoped<IAnswerOfficeWorkerService, AnswerOfficeWorkerService>();
            builder.Services.AddScoped<IWorkerOfficeAdminService, WorkerOfficeAdminService>();
            builder.Services.AddScoped<IAnswerUserAdministratorService, AnswerUserAdministratorService>();
            builder.Services.AddScoped<IOfficeDetailsService, OfficeDetailsService>();
            builder.Services.AddScoped<IOfficeOrgAdminService, OfficeOrgAdminService>();
            builder.Services.AddScoped<IUsersOrgAdminService, UsersOrgAdminService>();
            builder.Services.AddScoped<IOrganisationOrgAdminService, OrganisationOrgAdminService>();
            builder.Services.AddScoped<IAdminOrgService, AdminOrgService>();
            builder.Services.AddScoped<ILogService, LogService>();
            builder.Services.AddScoped<IBlindUserFeatureService, BlindUserFeatureService>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseStatusCodePagesWithReExecute("/Home/HandleError/{0}");

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithReExecute("/Home/HandleError/{0}");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();


            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "UserAdministrator", "OfficeAdministrator", "OrgAdministrator", "OfficeWorker", "WaitingApproval", "Admin" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }
            app.Run();
        } // Main
    } // Program
}
