using System;
using System.Collections.Generic;
using System.Windows;

namespace TenantRosterAutomation
{
    // Contains all the information about the apartment complex (5 buildings)
    class PropertyComplex
    {
        private string propertyName;
        private List<int> allApartmentNumbers;
        private List<Building> buildingList = new List<Building>();

        public string PropertyName { get { return propertyName; } }
        public List<Building> Buildings { get; private set; }
        public List<string> BuildingAddressList { get; private set; }
        public List<int> StreetNumbers { get; private set; }
        public List<int> AllApartmentNumbers { get { return allApartmentNumbers; } }
        public int MinApartmentNumber { get; private set; }
        public int MaxApartmentNumber { get; private set; }

        public PropertyComplex(string PropertyName, List<BuildingAndApartment> bldsAntApts)
        {
            propertyName = PropertyName;
            BuildingAddressList = new List<string>();
            allApartmentNumbers = new List<int>();
            StreetNumbers = new List<int>();
            CreateBuildingList(bldsAntApts);

            foreach (Building building in Buildings)
            {
                BuildingAddressList.Add(building.FullStreetAddress);
                allApartmentNumbers.AddRange(building.ApartmentNumbers);
            }

            allApartmentNumbers.Sort();

            MinApartmentNumber = allApartmentNumbers[0];
            MaxApartmentNumber = allApartmentNumbers[allApartmentNumbers.Count - 1];
        }

        public Building GetBuilding(int streetNumber)
        {
            Building building;

            building = buildingList.Find(x => x.AddressStreetNumber == streetNumber);

            return building;
        }

        public Building GetBuilding(string streetNumber)
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
                    MessageBox.Show("Non Numeric string passed into Building::GetBuilding().");
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

            foreach (Building building in Buildings)
            {
                buildingAddress = building.BuildingFromApartment(apartmentNumber);
                if (!string.IsNullOrEmpty(buildingAddress))
                {
                    return buildingAddress;
                }
            }

            return buildingAddress;
        }

        private void CreateBuildingList(List<BuildingAndApartment> buildingAptList)
        {
            buildingList = new List<Building>();
            string streetName = "Anza Avenue";

            foreach (BuildingAndApartment entry in buildingAptList)
            {
                Building found = buildingList.Find(x => x.AddressStreetNumber == entry.building);
                if (found != null)
                {
                    found.AddApartmentNumber(entry.apartment);
                }
                else
                {
                    Building newBuilding = new Building(entry.building, streetName);
                    newBuilding.AddApartmentNumber(entry.apartment);
                    buildingList.Add(newBuilding);
                }
            }

            foreach (Building building in buildingList)
            {
                building.SortApartMentNumbers();
            }

            foreach (Building building in buildingList)
            {
                StreetNumbers.Add(building.AddressStreetNumber);
            }

            Buildings = buildingList;
        }


    }
}
