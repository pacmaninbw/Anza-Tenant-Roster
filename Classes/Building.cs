using System.Collections.Generic;

namespace TenantRosterAutomation
{
    // Represents one building in the apartment Complex
    public class Building
    {
        private List<int> aptNumbers;

        public int AddressStreetNumber { get; private set; }
        public string FullStreetAddress { get; private set; }
        public List<int> ApartmentNumbers { get { return aptNumbers; } }

        public Building(int addressStreetNumber, string StreetName)
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
