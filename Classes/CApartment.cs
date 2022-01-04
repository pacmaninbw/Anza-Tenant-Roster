namespace RentRosterAutomation
{
    class CApartment
    {
        private CExcelInteropMethods excelInteropMethods = Program.excelInteropMethods;

        public int ApartmentNumber { get; private set; }
        public CRenter renter { get; private set; }

        public CApartment(int apartmentNumber)
        {
            ApartmentNumber = apartmentNumber;
            renter = excelInteropMethods.GetTenant(apartmentNumber);
        }

        public CApartment(int apartmentNumber, CRenter Renter)
        {
            ApartmentNumber = apartmentNumber;
            renter = Renter;
        }
    }
}
