using System;
using System.Windows.Forms;

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

        public void PfeReporter()
        {
            string mbTitle = null;
            string eMsg = Message;
            switch (PFEType)
            {
                case PFEType.PFE_UNKNOWN_FIELD:
                case PFEType.PFE_VERSION_ID_OLD:
                    mbTitle = "Preference File Out of Date: ";
                    break;

                case PFEType.PFE_VERSION_ID_NEW:
                    mbTitle = "The Preference File Version: ";
                    break;

                case PFEType.PFE_VERSION_FORMAT:
                    mbTitle = "Reading preferences File Version failed: ";
                    eMsg = InnerException.Message + "\n" + InnerException.ToString();
                    break;

                case PFEType.PFE_CANT_SAVE:
                    mbTitle = "Can't save changes to the preferences file: ";
                    eMsg = Message + "\n" + InnerException.ToString();
                    break;

                default:
                    mbTitle = "Programmer Error: Unknow Error type in PreferenceFileException:";
                    eMsg = Message;
                    break;

            }
            MessageBox.Show(Message, mbTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
