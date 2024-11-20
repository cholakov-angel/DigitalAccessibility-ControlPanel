using DigAccess.Application;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Services
{
    public class BaseService
    {
        protected readonly DigAccessAdminPanelContext context;
        public BaseService(DigAccessAdminPanelContext context)
        {
            this.context = context;
        } // BaseService
    } // BaseService
}
