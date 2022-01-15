using System;
using System.Collections.Generic;
using System.Data;

namespace TenantRosterAutomation
{
    public class ExcelFileData
    {
        private DataTable worksheetContents;
        private List<string> WorkSheets;

        public string ActiveWorkbookFullFileSpec { get; private set; }
        public string ActiveWorkSheet { get; private set; }
        public bool WorkbookRead { get; private set; }
        public bool WorkbookOpen { get; private set; }

        public ExcelFileData(string fullFileSpec, string activeWorkSheet)
        {
            if (string.IsNullOrEmpty(fullFileSpec))
            {
                ExcelFileException efe =
                    new ExcelFileException("Attempted to create ExcelFileData " +
                    "Object without Excel Data File Name.");
                throw efe;
            }
            ActiveWorkbookFullFileSpec = fullFileSpec;
            ActiveWorkSheet = activeWorkSheet;
            WorkSheets = null;
            worksheetContents = null;
            WorkbookOpen = false;
            WorkbookRead = false;
        }

        public List<string> GetWorkSheetCollection()
        {
            if (WorkSheets == null)
            {

                using (ExcelInterface excelInterface = new ExcelInterface(
                    ActiveWorkbookFullFileSpec, ActiveWorkSheet))
                {
                    try
                    {
                        WorkSheets = excelInterface.GetWorkSheetNames();
                    }
                    catch (Exception)
                    {
                        excelInterface.Dispose();
                        throw;
                    }
                }
            }

            return WorkSheets;
        }

        public void SaveChanges(List<Apartment> tenantEdits)
        {
            if (tenantEdits.Count > 0)
            {
                if (string.IsNullOrEmpty(ActiveWorkSheet))
                {
                    ExcelFileException efe =
                        new ExcelFileException("Attempted to Save Excel changes " +
                        "without Excel worksheet name.");
                    throw efe;
                }

                using (ExcelInterface excelInterface = new ExcelInterface(
                    ActiveWorkbookFullFileSpec, ActiveWorkSheet))
                {
                    try
                    {
                        excelInterface.SaveEdits(tenantEdits);
                    }
                    catch (Exception)
                    {
                        excelInterface.Dispose();
                        throw;
                    }
                }
            }
        }

        public DataTable GetActiveWorkSheetContents()
        {
            if (worksheetContents == null)
            {
                if (string.IsNullOrEmpty(ActiveWorkSheet))
                {
                    ExcelFileException efe =
                        new ExcelFileException("Attempted to get Excel worksheet " +
                        "data without Excel worksheet name.");
                    throw efe;
                }
                using (ExcelInterface excelInterface = new ExcelInterface(
                    ActiveWorkbookFullFileSpec, ActiveWorkSheet))
                {
                    try
                    {
                        worksheetContents = excelInterface.GetWorkSheetContents();
                    }
                    catch (Exception)
                    {
                        excelInterface.Dispose();
                        throw;
                    }

                }
            }

            return worksheetContents;
        }
    }
}
