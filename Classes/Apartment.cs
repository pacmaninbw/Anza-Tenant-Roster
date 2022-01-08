namespace TenantRosterAutomation
{
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
