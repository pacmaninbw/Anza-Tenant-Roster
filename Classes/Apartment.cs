namespace TenantRosterAutomation
{
    // The apartment model, currently models only the apartment number and
    // the apartment tenant. This may be expanded in the future to handle
    // lease information, parking and other columns of data in the excel
    // spreadsheet that are not currently modeled by this application but
    // are available in the ExcelFile DataTable.
    public class Apartment
    {
        public int ApartmentNumber { get; private set; }
        public Tenant Tenant { get; private set; }

        public Apartment(int apartmentNumber)
        {
            ApartmentNumber = apartmentNumber;
            Tenant = Globals.TenantRoster.GetTenant(apartmentNumber);
        }

        public Apartment(int apartmentNumber, Tenant tenant)
        {
            ApartmentNumber = apartmentNumber;
            Tenant = tenant;
        }
    }
}
