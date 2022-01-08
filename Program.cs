using System;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


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

                if (!WorkSheetIsOpenInOtherApp(Globals.ExcelWorkBookFullFileSpec))
                {
                    Application.Run(new RentRosterApp());
                }
                else
                {
                    ReportOpen();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("An unexpected error occurred: " + e.Message);
            }
        }

        public static void ReportOpen()
        {
            string alreadyOpen = "The excel workbook " + Globals.ExcelWorkBookFullFileSpec +
                    " is alread open in another application. \n" +
                    "Please save your changes in the other application and close the " +
                    " workbook and then restart this application.";

            MessageBox.Show(alreadyOpen);
        }

        // Check if there is any instance of excel open using the workbook.
        public static bool WorkSheetIsOpenInOtherApp(string workBook)
        {
            Excel.Application TestOnly = null;
            bool isOpened = true;
            // There are 2 possible exceptions here, GetActiveObject will throw
            // an exception if no instance of excel is running, and
            // workbooks.get_Item throws an exception if the sheetname isn't found.
            // Both of these exceptions indicate that the workbook isn't open.
            try
            {
                TestOnly = (Excel.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application");
                int lastSlash = workBook.LastIndexOf('\\');
                string fileNameOnly = workBook.Substring(lastSlash + 1);
                TestOnly.Workbooks.get_Item(fileNameOnly);
                TestOnly.Quit();
                TestOnly = null;
            }
            catch (Exception)
            {
                isOpened = false;
                if (TestOnly != null)
                {
                    TestOnly = null;
                }
            }
            return isOpened;
        }

    }
}
