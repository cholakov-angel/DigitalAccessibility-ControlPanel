using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Common
{
    public static class GuidParser
    {
        public static Guid GuidParse(string toBeParsed)
        {
            bool isLicenceIdVald = Guid.TryParse(toBeParsed, out Guid guidResult);

            if (!isLicenceIdVald)
            {
                throw new ArgumentException("Invalid id format!");

            }

            return guidResult;
        } // GuidParse
    } // GuidParser
}
