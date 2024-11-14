using DigAccess.Models.UserAdministrator.MasterKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Services.Interfaces
{
    public interface IMasterKeyService
    {
        public Task<MasterKeyIndexViewModel> GetUserMasterKey(string id);
    } // IMasterKeyService
}
