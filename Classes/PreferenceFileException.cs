using System;

namespace TenantRosterAutomation
{
    enum PFEType {
        PFE_VERSION_ID_OLD,
        PFE_VERSION_ID_NEW,
        PFE_VERSION_FORMAT,
        PFE_UNKNOWN_FIELD,
        PFE_CANT_SAVE
    }
    [Serializable]
    class PreferenceFileException : Exception
    {
        public PFEType PFEType { get; private set; }

        public PreferenceFileException(string message, PFEType pFEType, Exception innerException = null)
            : base(message, innerException)
        {
            PFEType = pFEType;
        }

    }
}
