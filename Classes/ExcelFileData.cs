using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

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
            if (CheckOpenApplicationsAndReport())
            {
                return WorkSheets;
            }

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

                if (CheckOpenApplicationsAndReport())
                {
                    return;
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
                    }
                }
            }
        }

        public DataTable GetActiveWorkSheetContents(bool checkIfOPen)
        {
            if (checkIfOPen)
            {
                if (CheckOpenApplicationsAndReport())
                {
                    return worksheetContents;
                }
            }

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
                    }
                    catch (Exception)
                    {
                        excelInterface.Dispose();
                    }

                    statusReport.Close();
                }
            }

            return worksheetContents;
        }

        private bool CheckOpenApplicationsAndReport()
        {
            if (ExcelWorkBookAlreadyOpen.TestIfOpen(ActiveWorkbookFullFileSpec))
            {
                MessageBox.Show(ExcelWorkBookAlreadyOpen.ReportOpen());
                return true;
            }

            return false;
        }
    }
}
