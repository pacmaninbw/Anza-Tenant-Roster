using System;

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
