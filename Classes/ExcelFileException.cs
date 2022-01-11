using System;

namespace TenantRosterAutomation
{
    [Serializable]
    class ExcelFileException : Exception
    {
        public ExcelFileException(string message, Exception innerException = null)
            : base(message, innerException)
        { }

    }
}
