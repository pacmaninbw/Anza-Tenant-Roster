using System;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace TenantRosterAutomation
{
    public class CheckExcelWorkBookOpen
    {
        // Check if there is any instance of excel open using the workbook.
        public static bool IsOpen(string workBook)
        {
            Excel.Application TestOnly = null;
            bool isOpened = true;
            // There are 2 possible exceptions here, GetActiveObject will throw
            // an exception if no instance of excel is running, and
            // workbooks.get_Item throws an exception if the sheetname isn't found.
            // Both of these exceptions indicate that the workbook isn't open.
            try
            {
                TestOnly = (Excel.Application)Marshal.GetActiveObject("Excel.Application");
                int lastSlash = workBook.LastIndexOf('\\');
                string fileNameOnly = workBook.Substring(lastSlash + 1);
                TestOnly.Workbooks.get_Item(fileNameOnly);
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

        // Common error message to use when the excel file is op in another app.
        public string ReportOpen(bool atStartUp)
        {
            string alreadyOpen = "The excel workbook " +
                Globals.Preferences.ExcelWorkBookFullFileSpec +
                    " is alread open in another application. \n" +
                    "Please save your changes in the other application and close the " +
                    "workbook and then" +
                    (atStartUp ? " try this operation again." : " restart this application.");

            return alreadyOpen;
        }

        public void TestAndThrowIfOpen(string workBook, bool atStartUp)
        {
            if (IsOpen(workBook))
            {
                AlreadyOpenInExcelException alreadOpen =
                    new AlreadyOpenInExcelException(ReportOpen(atStartUp));
                throw alreadOpen;
            }
        }
    }
}
