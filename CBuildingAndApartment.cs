namespace RentRosterAutomation
{
    // One of these is contructed for each apartment in the complex (5 buildings, 177 apartments).
    // The list is used during the construction of the apartment complex object
    class CBuildingAndApartment
    {
        public int building;
        public int apartment;
        public string fullStreetAddress;

        public CBuildingAndApartment(int streetNumber, int Apartment, string FullStreetAddress)
        {
            building = streetNumber;
            apartment = Apartment;
            fullStreetAddress = FullStreetAddress;
        }

    }
}
