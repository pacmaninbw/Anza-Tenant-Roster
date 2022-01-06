namespace RentRosterAutomation
{
    class Apartment
    {
        private ExcelInterface excelInteropMethods = Program.excelInterface;

        public int ApartmentNumber { get; private set; }
        public Tenant Tenant { get; private set; }

        public Apartment(int apartmentNumber)
        {
            ApartmentNumber = apartmentNumber;
            Tenant = excelInteropMethods.GetTenant(apartmentNumber);
        }

        public Apartment(int apartmentNumber, Tenant tenant)
        {
            ApartmentNumber = apartmentNumber;
            Tenant = tenant;
        }
    }
}
