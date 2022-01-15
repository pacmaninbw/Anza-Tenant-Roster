using System;

namespace TenantRosterAutomation
{
    [Serializable]
    class WordException : Exception
    {
        public WordException(string message, Exception innerException = null)
            : base(message, innerException)
        { }
    }
}
