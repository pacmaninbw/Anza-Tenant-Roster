using System;

namespace TenantRosterAutomation
{
    [Serializable]
    class ExcelFileCantOpenException : ExcelFileException
    {
        public ExcelFileCantOpenException(string message, Exception innerException = null)
            : base(message, innerException)
        { }

    }
}
