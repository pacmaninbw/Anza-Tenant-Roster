using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantRosterAutomation
{
    [Serializable]
    class ExcelFileException : Exception
    {
        public ExcelFileException(string message)
            : base(message)
        { }

    }
}
