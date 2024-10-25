using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigAccess.Interfaces;
using DigAccess.Web.Data;

namespace DigAccess.Services
{
    public class BaseService : IService
    {
        protected DigAccessDbContext context;
        public BaseService(DigAccessDbContext context)
        {
            this.context = context;
        } // BaseService
    }
}
