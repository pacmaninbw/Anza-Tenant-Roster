using System;
using System.Windows.Forms;

namespace RentRosterAutomation
{
    static class Program
    {
        // These are to be accessable by the entire program/solution
        public static CUserPreferences preferences;                 // The preferences should only be read from file once
                                                                    // For performance reasons
        public static CExcelInteropMethods excelInteropMethods;     // Excel Data maintained in program
                                                                    // Data used in many classes
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

                preferences = new CUserPreferences("./MyPersonalRentRosterPreferences.txt");
                // If we don't have the workbook name force the user to enter it.
                if (!preferences.HavePreferenceData)
                {
                    Application.Run(new Form_EditPreferences());
                }

                excelInteropMethods = new CExcelInteropMethods(preferences.RentRosterFile,
                    preferences.RentRosterSheet);
                if (!excelInteropMethods.AlreadyOpenOtherApp)
                {
                    Application.Run(new Form_RentRosterApp());
                }
                else
                {
                    ReportOpen();
                    // Nothing to save
                    excelInteropMethods.CloseWorkbookExitExcel();
                    excelInteropMethods = null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("An unexpected error occurred: " + e.Message);
            }

            if (excelInteropMethods != null)
            {
                excelInteropMethods.SaveEditsCloseWorkbookExitExcel();
                excelInteropMethods = null;
            }
        }

        public static void ReportOpen()
        {
            string alreadyOpen = "The excel workbook " + preferences.RentRosterFile +
                    " is alread open in another application. \n" +
                    "Please save your changes in the other application and close the " +
                    " workbook and then restart this application.";

            MessageBox.Show(alreadyOpen);

        }
    }
}
