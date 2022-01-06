using System;
using System.Windows.Forms;

namespace RentRosterAutomation
{
    static class Program
    {
        // These are to be accessable by the entire program/solution
        public static UserPreferences userPreferences;  // The preferences should only be read from file once
                                                         // For performance reasons
        public static ExcelInterface excelInterface;     // Excel Data maintained in program
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

                userPreferences = new UserPreferences("./MyPersonalRentRosterPreferences.txt");
                // If we don't have the workbook name force the user to enter it.
                if (!userPreferences.HavePreferenceData)
                {
                    Application.Run(new Form_EditPreferences());
                }

                excelInterface = new ExcelInterface(userPreferences.RentRosterFile,
                    userPreferences.RentRosterSheet);
                if (!excelInterface.AlreadyOpenOtherApp)
                {
                    Application.Run(new Form_RentRosterApp());
                }
                else
                {
                    ReportOpen();
                    // Nothing to save
                    excelInterface.CloseWorkbookExitExcel();
                    excelInterface = null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("An unexpected error occurred: " + e.Message);
            }

            if (excelInterface != null)
            {
                excelInterface.SaveEditsCloseWorkbookExitExcel();
                excelInterface = null;
            }
        }

        public static void ReportOpen()
        {
            string alreadyOpen = "The excel workbook " + userPreferences.RentRosterFile +
                    " is alread open in another application. \n" +
                    "Please save your changes in the other application and close the " +
                    " workbook and then restart this application.";

            MessageBox.Show(alreadyOpen);

        }
    }
}
