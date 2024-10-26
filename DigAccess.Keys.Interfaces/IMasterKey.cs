using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Keys.Interfaces
{
    public interface IMasterKey
    {
        public Task<string> GenerateMasterkey(string firstName, string middleName, string lastName, string personalID);
    }
}
