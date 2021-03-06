using System;
using System.Collections.Generic;
using System.Data;

namespace TenantRosterAutomation
{
    public class TenantDataTable
    {
        private bool dataChanged;
        private DataTable tenantData;
        // Updates are stored to a list to be written to back to the excel
        // file when the user clicks the save button in the UI or when the
        // user exits the program. Writing the entire DataTable back is
        // undesirable for 2 reasons, one is performance and the other is
        // to prevent corruption of the excel file.
        private List<Apartment> tenantUpdates;
        private ExcelFileData ExcelFile;

        public bool DataChanged { get { return dataChanged; } }
        public DataTable TenantRoster { get { return tenantData; } }

        public TenantDataTable(ExcelFileData excelFile)
        {
            ExcelFile = excelFile;
            tenantData = excelFile.GetActiveWorkSheetContents();
            dataChanged = false;
            tenantUpdates = new List<Apartment>();
        }

        public Tenant GetTenant(int apartmentNumber)
        {
            Tenant tenant = null;
            try
            {
                if (tenantData != null)
                {
                    string searchString = "UnitNo = '" + apartmentNumber.ToString() + "'";
                    DataRow[] aptTenantData = tenantData.Select(searchString);
                    tenant = FillTenantFromDataRow(aptTenantData);
                }
            }
            catch (Exception e)
            {
                Exception Tdt = new Exception("Exception in TenantDataTable::GetTenant(): ", e);
                throw Tdt;
            }

            return tenant;
        }

        public bool SaveChanges()
        {
            bool successChange = true;
            if (dataChanged)
            {
                ExcelFile.SaveChanges(tenantUpdates);
                dataChanged = false;
                tenantUpdates.Clear();
            }

            return successChange;
        }

        public void DeleteTenant(int apartmentNumber)
        {
            Tenant tenant = new Tenant();
            tenantUpdates.Add(new Apartment(apartmentNumber, tenant));
            dataChanged = UdateTenantDataTable(apartmentNumber, tenant);
        }

        public void AddEditTenant(int apartmentNumber, Tenant tenant)
        {
            tenantUpdates.Add(new Apartment(apartmentNumber, tenant));
            dataChanged = UdateTenantDataTable(apartmentNumber, tenant);
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
                string searchString = "UnitNo = '" + apartmentNumber.ToString() + "'";
                DataRow[] aptTenantData = tenantData.Select(searchString);
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
                Exception Tdt = new Exception(
                    "Exception in TenantDataTable::updateDataTable(): ", e);
                throw Tdt;
            }

            return updated;
        }
    }
}
