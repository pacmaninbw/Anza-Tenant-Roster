using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace RentRosterAutomation
{
    // Provides the interface between the rest of the program and Microsoft excel
    class ExcelInterface
    {
        // Excel objects
        private Excel.Application xlApp;
        private Excel.Workbook xlWorkbook;
        private Excel.Worksheet tenantRoster;

        // Classes written for this project
        private PropertyComplex complex;
        private List<Apartment> TenantUpdates;     // Stored updates to be written
                                                    // by save button or program exit
        private bool worksheetChanged;
        private string tenantRosterName;
        private DataTable localTenantRoster;        // Local copy of excel worksheet
        private const int ApartmentColumn = 0;
        private const int TenantLastNameColumn = 3;
        private const int CoTenantLastNameColumn = 5;

        public PropertyComplex Complex { get { return complex; } }
        public bool AlreadyOpenOtherApp { get; private set; }
        public bool HaveEditsToSave { get { return (TenantUpdates.Count > 0); } }
        public string WorkbookName { get; set; }

        public ExcelInterface(string workBookName, string workSheetName)
        {
            worksheetChanged = false;
            WorkbookName = workBookName;
            tenantRosterName = workSheetName;
            TenantUpdates = new List<Apartment>();

            try
            {
                AlreadyOpenOtherApp = false;
                if (!string.IsNullOrEmpty(WorkbookName))
                {
                    AlreadyOpenOtherApp = WorkSheetIsOpenInOtherApp(WorkbookName);
                    if (!AlreadyOpenOtherApp)
                    {
                        GetExcelDataAndReportProgress();
                        ConstructComplexAndReport();
                        CloseWorkbookExitExcel();
                    }
                }
            }
            catch (Exception e)
            {
                string emsg = "ExcelInterface Constructor failed while building Complex:" + e.Message;
                MessageBox.Show(emsg);
            }
        }

        public void SaveEditsCloseWorkbookExitExcel()
        {
            if (worksheetChanged)
            {
                SaveWorkBookEdits();
            }

            CloseWorkbookExitExcel();
        }

        public void CloseWorkbookExitExcel()
        {
            if (xlWorkbook == null || xlApp == null)
            {
                return;
            }

            xlWorkbook.Close();
            xlWorkbook = null;

            xlApp.Quit();
            xlApp = null;
        }

        public List<string> GetSheetNames()
        {
            List<string> sheetNames = new List<string>();

            StartExcelOpenWorkbook(false);
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

        public void PreferencesUpdated(UserPreferences preferences)
        {
            WorkbookName = preferences.RentRosterFile;
            tenantRosterName = preferences.RentRosterSheet;
            StartExcelOpenWorkbook(true);
        }

        public MailboxData GetMailboxData(Building building)
        {
            MailboxData mailboxData = new MailboxData(building);
            List<int> apartmentNumbers = building.ApartmentNumbers;
            foreach (int aptNo in apartmentNumbers)
            {
                mailboxData.addApartmentData(new Apartment(aptNo));
            }

            return mailboxData;
        }

        public void DeleteTenant(int apartmentNumber)
        {
            Tenant tenant = new Tenant();
            TenantUpdates.Add(new Apartment(apartmentNumber, tenant));
            worksheetChanged = UdateTenantDataTable(apartmentNumber, tenant);
        }

        public void AddEditTenant(int apartmentNumber, Tenant tenant)
        {
            TenantUpdates.Add(new Apartment(apartmentNumber, tenant));
            worksheetChanged = UdateTenantDataTable(apartmentNumber, tenant);
        }

        public Tenant GetTenant(int apartmentNumber)
        {
            Tenant tenant = null;
            try
            {
                DataTable lTenantRoster = GetLocalTenantRoster();
                if (localTenantRoster != null)
                {
                    string searchString = "UnitNo = '" + apartmentNumber.ToString() + "'";
                    DataRow[] aptTenantData = lTenantRoster.Select(searchString);
                    tenant = FillTenantFromDataRow(aptTenantData);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception in ExcelInterface::GetTenant(): " + e.Message);
            }

            return tenant;
        }

        private void ConstructComplexAndReport()
        {
            if (localTenantRoster == null)
            {
                return;
            }
            List<BuildingAndApartment> buildingAndApartments;
            ReportCurrentStatusWindow statusReport = new ReportCurrentStatusWindow();
            statusReport.MessageText = "Constructing Apartment Complex Data.";
            statusReport.Show();
            buildingAndApartments = CreateBuildingAndApartmentsList();
            complex = new PropertyComplex("Anza Victoria Apartments, LLC", buildingAndApartments);
            statusReport.Close();
        }

        private void GetExcelDataAndReportProgress()
        {
            ReportCurrentStatusWindow statusReport = new ReportCurrentStatusWindow();
            statusReport.MessageText =
                "Starting Excel and Loading Tenant Data From Excel.";
            statusReport.Show();
            StartExcelOpenWorkbook(false);
            localTenantRoster = GetLocalTenantRoster();
            statusReport.Close();

        }

        // Creates data for a single tenant that can be edited from
        // the local data table.
        private Tenant FillTenantFromDataRow(DataRow[] aptTenantData)
        {
            Tenant tenant = new Tenant();

            tenant.LastName = aptTenantData[0].Field<string>("Last");
            tenant.FirstName = aptTenantData[0].Field<string>("First");
            tenant.CoTenantLastName = aptTenantData[0].Field<string>("Add OCC Last");
            tenant.HomePhone = aptTenantData[0].Field<string>("Ph #");
            tenant.CoTenantFirstName = aptTenantData[0].Field<string>("Add OCC First");
            tenant.RentersInsurancePolicy = aptTenantData[0].Field<string>("Renters Ins");
            tenant.LeaseStart = aptTenantData[0].Field<string>("Lease Start");
            tenant.LeaseEnd = aptTenantData[0].Field<string>("Lease End");
            tenant.Email = aptTenantData[0].Field<string>("Email");

            return tenant;
        }

        // Updates the local version of the data in the application.
        private bool UdateTenantDataTable(int apartmentNumber, Tenant tenant)
        {
            bool updated = false;
            try
            {
                DataTable lTenantRoster = GetLocalTenantRoster();
                string searchString = "UnitNo = '" + apartmentNumber.ToString() + "'";
                DataRow[] aptTenantData = lTenantRoster.Select(searchString);
                DataRow currentApartment = aptTenantData[0];
                currentApartment.BeginEdit();
                currentApartment["First"] = tenant.FirstName;
                currentApartment["Last"] = tenant.LastName;
                currentApartment["Add OCC Last"] = tenant.CoTenantLastName;
                currentApartment["Add OCC First"] = tenant.CoTenantFirstName;
                currentApartment["Ph #"] = tenant.HomePhone;
                currentApartment["Renters Ins"] = tenant.RentersInsurancePolicy;
                currentApartment["Lease Start"] = tenant.LeaseStart;
                currentApartment["Lease End"] = tenant.LeaseEnd;
                currentApartment["Email"] = tenant.Email;
                currentApartment.EndEdit();
                updated = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception in ExcelInterface::updateDataTable(): " + e.Message);
            }

            return updated;
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

        private DataTable GetLocalTenantRoster()
        {
            DataTable tenantRosterDt = localTenantRoster;
            if (tenantRosterDt == null)
            {
                int headerRow = 1;
                int firstColumn = 1;
                tenantRosterDt = ReadExcelIntoDatatble(headerRow, firstColumn);
            }

            return tenantRosterDt;
        }

        private void StartExcelOpenWorkbook(bool showErrorMessage)
        {
            if (xlApp != null && xlWorkbook != null)
            {
                return;
            }

            if (string.IsNullOrEmpty(WorkbookName))
            {
                if (showErrorMessage)
                {
                    MessageBox.Show("Please update your preferences by adding the excel file that contains the tenant roster");
                }
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
                    StartExcelOpenWorkbook(true);
                    List<string> sheetNames = GetSheetNames();
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
                localTenantRoster = null;
                string eMsg = "Function ExcelInterface.openTenantRosterWorkSheet() failed: " + e.Message;
                MessageBox.Show(eMsg);
            }
        }

        // To enhance performance the excel worksheet is read once into a local
        // DataTable.
        private DataTable ReadExcelIntoDatatble(int HeaderLine, int ColumnStart)
        {
            try
            {
                StartExcelOpenWorkbook(true);
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

                // We don't need access to the data in excel while the edits
                // are being made or the word documents are generated.
                CloseWorkbookExitExcel();

                return tenantTable;
            }
            catch (Exception ex)
            {
                string eMsg = "In ReadExcelToDatatble error: " + ex.Message;
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
            // This loop needs to be optimized, it takes almost 8 seconds
            for (int row = headerLine + 1; row <= rowcount; row++)
            {
                DataRow tenantData = tenantTable.NewRow();
                for (int column = firstColumn; column <= columnCount; column++)
                {
                    tenantData[column - firstColumn] =
                        Convert.ToString(TenantDataRange.Cells[row, column].Value2);
                }
                tenantTable.Rows.Add(tenantData);
            }

        }

        private void SaveWorkBookEdits()
        {
            if (!HaveEditsToSave)
            {
                return;
            }
            string eSaveMsg = "Can't save edits to " + WorkbookName;
            try
            {
                ReportCurrentStatusWindow SaveStatus = new ReportCurrentStatusWindow();
                SaveStatus.MessageText = "Saving updated tenants and apartments to Excel.";
                SaveStatus.Show();
                StartExcelOpenWorkbook(false);
                OpenTenantRosterWorkSheet();
                xlApp.Visible = false;
                xlApp.DisplayAlerts = false;

                if (tenantRoster == null)
                {
                    MessageBox.Show(eSaveMsg + " can't open the sheet " + tenantRosterName);
                    return;
                }
                List<string> columnNames = GetColumnNames();
                foreach (Apartment edit in TenantUpdates)
                {
                    UpdateColumnData(edit, columnNames);
                }
                xlWorkbook.Save();
                SaveStatus.Close();
                TenantUpdates.Clear();
            }
            catch (Exception ex)
            {
                string exSaveMsg = eSaveMsg + " : " + ex.Message;
                MessageBox.Show(eSaveMsg);
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
                MessageBox.Show("Exception in ExcelInterface::getRowNumberForSave(): " +
                    ex.Message);
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
                MessageBox.Show("Exception in ExcelInterface::GetUnitColumn(): " +
                    ex.Message);
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
                MessageBox.Show("Exception in ExcelInterface::GetColumnNames(): " +
                    ex.Message);
            }

            return columnNames;
        }

        private List<BuildingAndApartment> CreateBuildingAndApartmentsList()
        {
            if (localTenantRoster == null)
            {
                return null;
            }
            List<BuildingAndApartment> buildingAndApartments = new List<BuildingAndApartment>();
            int LastDataRow = localTenantRoster.Rows.Count;

            for (int row = 0; row < LastDataRow; row++)
            {
                buildingAndApartments.Add(CreateBuildAndApartmentFromDataRow(row));
            }

            return buildingAndApartments;
        }

        private BuildingAndApartment CreateBuildAndApartmentFromDataRow(int row)
        {
            DataRow dataRow = localTenantRoster.Rows[row];
            string streetAddress = dataRow.Field<string>("Street 1").ToString();
            string apartmentNumString = dataRow.Field<string>("UnitNo").ToString();

            int apartmentNumber;
            Int32.TryParse(apartmentNumString, out apartmentNumber);

            int firstSpace = streetAddress.IndexOf(' ');
            string streetNumber = streetAddress.Substring(0, firstSpace);
            int buildingNumber;
            Int32.TryParse(streetNumber, out buildingNumber);

            BuildingAndApartment currentApt = new BuildingAndApartment(buildingNumber,
                apartmentNumber, streetAddress);
            return currentApt;
        }

        // Check if there is any instance of excel open using the workbook.
        private bool WorkSheetIsOpenInOtherApp(string workBook)
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
                int lastSlash = WorkbookName.LastIndexOf('\\');
                string fileNameOnly = WorkbookName.Substring((lastSlash + 1));
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
    }
}
