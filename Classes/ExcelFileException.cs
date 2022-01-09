using System;

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
