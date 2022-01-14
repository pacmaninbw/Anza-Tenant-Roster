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

        public void AddOrChangeWorkSheets(List<string> workSheets)
        {
            if (WorkSheets != null)
            {
                WorkSheets = null;
            }
            WorkSheets = workSheets;
        }

        public void ChangeActiveWorkbook(string fullFileSpec)
        {
            ActiveWorkbookFullFileSpec = null;
            ActiveWorkbookFullFileSpec = fullFileSpec;
        }

        public void ChangeActiveWorksheet(string newActiveWorkSheet)
        {
            ActiveWorkSheet = null;
            ActiveWorkSheet = newActiveWorkSheet;
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
                        excelInterface.Dispose();
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
                        excelInterface.Dispose();
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
                    ReportCurrentStatusWindow statusReport = new ReportCurrentStatusWindow();
                    statusReport.MessageText =
                        "Starting Excel and Loading Tenant Data From Excel.";
                    statusReport.Show();
                    try
                    {
                        worksheetContents = excelInterface.GetWorkSheetContents();
                        excelInterface.Dispose();
                    }
                    catch (Exception)
                    {
                        statusReport.Close();
                        excelInterface.Dispose();
                        throw;
                    }

                    statusReport.Close();
                }
            }

            return worksheetContents;
        }
    }
}
