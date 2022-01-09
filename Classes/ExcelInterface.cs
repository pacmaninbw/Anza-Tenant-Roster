using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace TenantRosterAutomation
{
    // Provides the interface between the rest of the program and Microsoft excel.
    // This class is constructed on an as needed basis and should not persist
    // during execution. The distruction of this class releases the excel process
    // of this class creates and prevents orphan excel processes from being
    // created.
    class ExcelInterface : IDisposable 
    {
        private Excel.Application xlApp;
        private Excel.Workbook xlWorkbook;
        private Excel.Worksheet tenantRoster;

        private bool disposed;
        private string tenantRosterName;
        private string WorkbookName;

        public ExcelInterface(string workBookName, string workSheetName)
        {
            if (string.IsNullOrEmpty(workBookName))
            {
                ExcelFileException efe =
                    new ExcelFileException("Attempted to create ExcelInterface " +
                    "Object without Excel Data File Name.");
                throw efe;
            }
            WorkbookName = workBookName;
            tenantRosterName = workSheetName;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SaveEdits(List<Apartment> tenantUpdates)
        {
            string eSaveMsg = "Can't save edits to " + WorkbookName;
            try
            {
                ReportCurrentStatusWindow SaveStatus = new ReportCurrentStatusWindow();
                SaveStatus.MessageText = "Saving updated tenants and apartments to Excel.";
                SaveStatus.Show();
                StartExcelOpenWorkbook();
                OpenTenantRosterWorkSheet();
                xlApp.Visible = false;
                xlApp.DisplayAlerts = false;

                if (tenantRoster == null)
                {
                    MessageBox.Show(eSaveMsg + " can't open the excel worksheet "
                        + tenantRosterName + " to save changes");
                    return;
                }
                List<string> columnNames = GetColumnNames();
                foreach (Apartment edit in tenantUpdates)
                {
                    UpdateColumnData(edit, columnNames);
                }
                xlWorkbook.Save();
                SaveStatus.Close();
            }
            catch (Exception ex)
            {
#if DEBUG
                string exSaveMsg = eSaveMsg + " : " + ex.Message;
                MessageBox.Show(eSaveMsg);
#endif
                throw;
            }
        }

        public List<string> GetWorkSheetNames()
        {
            List<string> sheetNames = new List<string>();

            StartExcelOpenWorkbook();
            if (xlWorkbook == null)
            {
                return null;
            }

            int SheetCount = xlWorkbook.Sheets.Count;
            for (int i = 1; i <= SheetCount; ++i)
            {
                string sheetname = xlWorkbook.Sheets[i].Name;
                sheetNames.Add(sheetname);
            }

            return sheetNames;
        }

        public DataTable GetWorkSheetContents()
        {
            int headerRow = 1;
            int firstColumn = 1;
            DataTable workSheetContents = ReadExcelIntoDatatble(headerRow, firstColumn);

            return workSheetContents;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (xlWorkbook != null)
                    {
                        xlWorkbook.Close();
                        xlWorkbook = null;
                    }

                    if (xlApp != null)
                    {
                        xlApp.Quit();
                        xlApp = null;
                    }
                }
                disposed = true;
            }
        }

        // Updates a row of data in the excel file
        private void UpdateColumnData(Apartment rowEdit, List<string> columnNames)
        {
            Tenant tenant = rowEdit.Tenant;
            Excel.Range currentRow = FindRowInWorkSheetForUpdate(rowEdit.ApartmentNumber);
            UpdateColumn(currentRow, "Last", tenant.LastName, columnNames);
            UpdateColumn(currentRow, "First", tenant.FirstName, columnNames);
            UpdateColumn(currentRow, "Add OCC First", tenant.CoTenantFirstName, columnNames);
            UpdateColumn(currentRow, "Add OCC Last", tenant.CoTenantLastName, columnNames);
            UpdateColumn(currentRow, "Ph #", tenant.HomePhone, columnNames);
            UpdateColumn(currentRow, "Renters Ins", tenant.RentersInsurancePolicy, columnNames);
            UpdateColumn(currentRow, "Lease Start", tenant.LeaseStart, columnNames);
            UpdateColumn(currentRow, "Lease End", tenant.LeaseEnd, columnNames);
            UpdateColumn(currentRow, "Email", tenant.Email, columnNames);
        }

        private void StartExcelOpenWorkbook()
        {
            if (xlApp != null && xlWorkbook != null)
            {
                return;
            }

            if (xlApp == null)
            {
                xlApp = new Excel.Application();
                xlApp.Visible = false;
                xlApp.DisplayAlerts = false;
            }

            if (xlWorkbook == null)
            {
                xlWorkbook = xlApp.Workbooks.Open(WorkbookName);
            }
        }

        private void OpenTenantRosterWorkSheet()
        {
            try
            {
                if (!string.IsNullOrEmpty(tenantRosterName))
                {
                    StartExcelOpenWorkbook();
                    List<string> sheetNames = GetWorkSheetNames();
                    bool exists = sheetNames.Any(x => x.Contains(tenantRosterName));
                    if (!exists)
                    {
                        MessageBox.Show("The workbook " + WorkbookName + " does not contain the worksheet " + tenantRosterName);
                        tenantRoster = null;
                    }
                    else
                    {
                        tenantRoster = xlWorkbook.Worksheets[tenantRosterName];
                    }
                }
            }
            catch (Exception e)
            {
#if DEBUG
                string eMsg = "Function ExcelInterface.OpenTenantRosterWorkSheet() failed: "
                    + e.Message;
                MessageBox.Show(eMsg);
#endif
                throw;
            }
        }

        // To enhance performance the excel worksheet is read once into a local
        // DataTable.
        private DataTable ReadExcelIntoDatatble(int HeaderLine, int ColumnStart)
        {
            try
            {
                StartExcelOpenWorkbook();
                OpenTenantRosterWorkSheet();
                if (tenantRoster == null)
                {
                    return null;
                }

                Excel.Range TenantDataRange = tenantRoster.UsedRange;
                DataTable tenantTable = CreateDataTableAddColumns(TenantDataRange,
                    HeaderLine, ColumnStart);
                AddTenantDataToTenantTable(TenantDataRange, ref tenantTable,
                    HeaderLine, ColumnStart);

                return tenantTable;
            }
            catch (Exception ex)
            {
                string eMsg = "In ExcelInterface::ReadExcelToDatatble error: " + ex.Message;
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private DataTable CreateDataTableAddColumns(Excel.Range TenantDataRange,
            int headerLine, int firstColumn)
        {
            DataTable tenantTable = new DataTable();
            int columnCount = TenantDataRange.Columns.Count;

            for (int column = firstColumn; column <= columnCount; column++)
            {
                tenantTable.Columns.Add(Convert.ToString
                    (TenantDataRange.Cells[headerLine, column].Value2), typeof(string));
            }

            return tenantTable;
        }

        private void AddTenantDataToTenantTable(Excel.Range TenantDataRange,
            ref DataTable tenantTable, int headerLine, int firstColumn)
        {
            int columnCount = TenantDataRange.Columns.Count;
            int rowcount = TenantDataRange.Rows.Count;

            var values = TenantDataRange.Value2;
            for (int row = headerLine + 1; row <= rowcount; row++)
            {
                DataRow tenantData = tenantTable.NewRow();
                for (int column = firstColumn; column <= columnCount; column++)
                {
                    tenantData[column - firstColumn] =
                        Convert.ToString(values[row, column]);
                }
                tenantTable.Rows.Add(tenantData);
            }
        }

        private Excel.Range FindRowInWorkSheetForUpdate(int apartmentNumber)
        {
            Excel.Range currentRow = null;
            object oMissing = Missing.Value;

            try
            {
                Excel.Range UnitNoColumn = GetUnitColumn();
                if (UnitNoColumn != null)
                {
                    currentRow = UnitNoColumn.Find(apartmentNumber.ToString(), oMissing,
                        Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
                        Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
                        oMissing, oMissing);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show("Exception in ExcelInterface::getRowNumberForSave(): " +
                    ex.Message);
#endif
                throw;
            }

            return currentRow;
        }

        void UpdateColumn(Excel.Range currentRow, string columnName, string newValue,
            List<string> columnNames)
        {
            int columnNumber = GetColumnNumber(columnName, columnNames);
            currentRow.Cells[1, columnNumber] = newValue;
        }

        // Get the apartment unit column for searching.
        private Excel.Range GetUnitColumn()
        {
            Excel.Range UnitColumn = null;

            try
            {
                string headerName = "UnitNo";
                Excel.Range headerRow = tenantRoster.UsedRange.Rows[1];

                foreach (Excel.Range cel in headerRow.Cells)
                {
                    if (cel.Text.ToString().Equals(headerName))
                    {
                        UnitColumn = tenantRoster.Range[cel.Address, cel.End[Excel.XlDirection.xlDown]];
                        return UnitColumn;
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show("Exception in ExcelInterface::GetUnitColumn(): " +
                    ex.Message);
#endif
                throw;
            }

            return UnitColumn;
        }

        private int GetColumnNumber(string columnName, List<string> columnNames)
        {
            int columnNumber = 1;

            foreach (string name in columnNames)
            {
                if (name.Equals(columnName))
                {
                    return columnNumber;
                }
                columnNumber++;
            }

            return columnNumber;
        }

        private List<string> GetColumnNames()
        {
            List<string> columnNames = new List<string>();

            try
            {
                Excel.Range headerRow = tenantRoster.UsedRange.Rows[1];
                foreach (Excel.Range cell in headerRow.Cells)
                {
                    columnNames.Add(cell.Text);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show("Exception in ExcelInterface::GetColumnNames(): " +
                    ex.Message);
#endif
                throw;
            }

            return columnNames;
        }

    }
}
