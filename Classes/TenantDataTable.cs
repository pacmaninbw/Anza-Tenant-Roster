using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace TenantRosterAutomation
{
    public class TenantDataTable
    {
        private bool dataChanged;
        private DataTable tenantData;
        private List<Apartment> tenantUpdates;
        private ExcelFileData ExcelFile;

        public bool DataChanged { get { return dataChanged; } }
        public DataTable TenantRoster { get { return tenantData; } }

        public TenantDataTable(ExcelFileData excelFile)
        {
            ExcelFile = excelFile;
            if (string.IsNullOrEmpty(ExcelFile.ActiveWorkSheet))
            {

            }
            tenantData = excelFile.GetActiveWorkSheetContents(false);
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
#if DEBUG
                MessageBox.Show("Exception in TenantDataTable::GetTenant(): " + e.Message);
#endif
                throw;
            }

            return tenant;
        }

        public bool SaveChanges()
        {
            bool successChange = true;
            if (dataChanged)
            {
                ExcelFile.SaveChanges(tenantUpdates);
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
#if DEBUG
                MessageBox.Show("Exception in TenantDataTable::updateDataTable(): " + e.Message);
#endif
                throw;
            }

            return updated;
        }
    }
}
