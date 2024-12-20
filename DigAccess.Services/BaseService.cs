﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigAccess.Data.Entities;
using DigAccess.Interfaces;
using DigAccess.Web.Data;
using Microsoft.AspNetCore.Identity;

namespace DigAccess.Services
{
    public class BaseService : IService
    {
        protected DigAccessDbContext context;
        protected UserManager<ApplicationUser> userManager;

        public BaseService(DigAccessDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        } // BaseService
        protected async Task<ApplicationUser?> GetOfficeWorker(string workerId, string role)
        {
            if (workerId == null)
            {
                throw new ArgumentException("Invalid user!");
            }

            var officeWorker = await userManager.FindByIdAsync(workerId);

            if (officeWorker == null)
            {
                throw new ArgumentException("Invalid user!");
            }

            if (await userManager.IsInRoleAsync(officeWorker, role) == false)
            {
                throw new ArgumentException("Invalid user!");
            }
            return officeWorker;
        } // GetOfficeWorker
    }
}
