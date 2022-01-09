using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantRosterAutomation
{
    [Serializable]
    public class AlreadyOpenInExcelException : Exception
    {
        public AlreadyOpenInExcelException(string message)
            : base(message)
        { }

    }
}
