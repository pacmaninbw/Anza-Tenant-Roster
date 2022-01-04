using System;
using System.Collections.Generic;
using System.Windows;

namespace RentRosterAutomation
{
    // Contains all the information about the apartment complex (5 buildings)
    class CPropertyComplex
    {
        private string propertyName;
        private List<int> allApartmentNumbers;
        private List<CBuilding> buildingList = new List<CBuilding>();

        public string PropertyName { get { return propertyName; } }
        public List<CBuilding> Buildings { get; private set; }
        public List<string> BuildingAddressList { get; private set; }
        public List<int> StreetNumbers { get; private set; }
        public List<int> AllApartmentNumbers { get { return allApartmentNumbers; } }
        public int MinApartmentNumber { get; private set; }
        public int MaxApartmentNumber { get; private set; }

        public CPropertyComplex(string PropertyName, List<CBuildingAndApartment> bldsAntApts)
        {
            propertyName = PropertyName;
            BuildingAddressList = new List<string>();
            allApartmentNumbers = new List<int>();
            StreetNumbers = new List<int>();
            CreateBuildingList(bldsAntApts);

            foreach (CBuilding building in Buildings)
            {
                BuildingAddressList.Add(building.FullStreetAddress);
                allApartmentNumbers.AddRange(building.ApartmentNumbers);
            }

            allApartmentNumbers.Sort();

            MinApartmentNumber = allApartmentNumbers[0];
            MaxApartmentNumber = allApartmentNumbers[allApartmentNumbers.Count - 1];
        }

        public CBuilding GetBuilding(int streetNumber)
        {
            CBuilding building;

            building = buildingList.Find(x => x.AddressStreetNumber == streetNumber);

            return building;
        }

        public CBuilding GetBuilding(string streetNumber)
        {
            int iStreetNumber = 0;

            try
            {
                if (Int32.TryParse(streetNumber, out iStreetNumber))
                {
                    return GetBuilding(iStreetNumber);
                }
                else
                {
                    MessageBox.Show("Non Numeric string passed into CBuilding::GetBuilding().");
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string FindBuildingByApartment(int apartmentNumber)
        {
            string buildingAddress = null;

            foreach (CBuilding building in Buildings)
            {
                buildingAddress = building.BuildingFromApartment(apartmentNumber);
                if (!string.IsNullOrEmpty(buildingAddress))
                {
                    return buildingAddress;
                }
            }

            return buildingAddress;
        }

        private void CreateBuildingList(List<CBuildingAndApartment> buildingAptList)
        {
            buildingList = new List<CBuilding>();
            string streetName = "Anza Avenue";

            foreach (CBuildingAndApartment entry in buildingAptList)
            {
                CBuilding found = buildingList.Find(x => x.AddressStreetNumber == entry.building);
                if (found != null)
                {
                    found.AddApartmentNumber(entry.apartment);
                }
                else
                {
                    CBuilding newBuilding = new CBuilding(entry.building, streetName);
                    newBuilding.AddApartmentNumber(entry.apartment);
                    buildingList.Add(newBuilding);
                }
            }

            foreach (CBuilding building in buildingList)
            {
                building.SortApartMentNumbers();
            }

            foreach (CBuilding building in buildingList)
            {
                StreetNumbers.Add(building.AddressStreetNumber);
            }

            Buildings = buildingList;
        }


    }
}
