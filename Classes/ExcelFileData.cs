using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DataTable ActiveWorksheetContents { get { return GetActiveWorkSheetContents(); } }

        public ExcelFileData(string fullFileSpec, string activeWorkSheet)
        {
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
                using (ExcelInterface excelInterface = new ExcelInterface(ActiveWorkbookFullFileSpec, ActiveWorkSheet))
                {
                    try
                    {
                        WorkSheets = excelInterface.GetSheetNames();
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
                using (ExcelInterface excelInterface = new ExcelInterface(ActiveWorkbookFullFileSpec, ActiveWorkSheet))
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

        private DataTable GetActiveWorkSheetContents()
        {
            if (worksheetContents == null)
            {
                using (ExcelInterface excelInterface = new ExcelInterface(ActiveWorkbookFullFileSpec, ActiveWorkSheet))
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
    }
}
