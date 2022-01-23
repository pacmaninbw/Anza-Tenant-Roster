using System;

namespace TenantRosterAutomation
{
    [Serializable]
    class ExcelFileDoesntExistException : ExcelFileException
    {
        public ExcelFileDoesntExistException(string message)
            : base(message, null)
        { }
    }
}
