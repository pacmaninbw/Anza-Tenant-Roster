using System.Collections.Generic;

namespace RentRosterAutomation
{
    // Represents one building in the apartment Complex
    class CBuilding
    {
        private List<int> aptNumbers;

        public int AddressStreetNumber { get; private set; }
        public string FullStreetAddress { get; private set; }
        public List<int> ApartmentNumbers { get { return aptNumbers; } }

        public CBuilding(int addressStreetNumber, string StreetName, List<int> apartmentNumbers)
        {
            AddressStreetNumber = addressStreetNumber;
            FullStreetAddress = addressStreetNumber.ToString() + " " + StreetName;
            aptNumbers = apartmentNumbers;
            aptNumbers.Sort();
        }

        public CBuilding(int addressStreetNumber, string StreetName)
        {
            AddressStreetNumber = addressStreetNumber;
            FullStreetAddress = addressStreetNumber.ToString() + " " + StreetName;
            aptNumbers = new List<int>();
        }

        public void AddApartmentNumber(int apartmentNumber)
        {
            if (!aptNumbers.Contains(apartmentNumber))
            {
                aptNumbers.Add(apartmentNumber);
            }
        }

        public void SortApartMentNumbers()
        {
            aptNumbers.Sort();
        }

        public bool IsApartmentInThisBuildin(int apartmentNumber)
        {
            return aptNumbers.Contains(apartmentNumber);
        }

        public string BuildingFromApartment(int apartmentNumber)
        {
            return (IsApartmentInThisBuildin(apartmentNumber)) ? FullStreetAddress : null;
        }
    }

}
