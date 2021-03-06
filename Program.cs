using System;
using System.Windows.Forms;

namespace TenantRosterAutomation
{
    static class Program
    {
#if DEBUG
        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;
#endif

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if DEBUG
            AttachConsole(ATTACH_PARENT_PROCESS);
            Console.WriteLine("\nStarting TenantRosterAutomation Application");
#endif
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new RentRosterApp());
            }
            catch (PreferenceFileException pfE)
            {
                // These should all have been handled in RentRosterApp(), but report it
                pfE.PfeReporter();
            }
            catch (AlreadyOpenInExcelException e)
            {
                MessageBox.Show(e.Message, "Excel File Already Open:",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show("An unexpected error occurred: " + e.ToString(),
                    "Unexpected Error: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
