using System;
using System.Windows.Forms;

namespace TenantRosterAutomation
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new RentRosterApp());
            }
            catch (AlreadyOpenInExcelException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show("An unexpected error occurred: " + e.ToString());
            }
        }
    }
}
