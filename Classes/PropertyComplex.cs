using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace TenantRosterAutomation
{
    // Models the entire apartment complex (5 buildings 177 apartmens)
    public class PropertyComplex
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

        public enum ApartmentNumberValid
        {
            APARTMENT_NUMBER_OUT_OF_RANGE,
            APARTMENT_NUMBER_NONNUMERIC,
            APARTMENT_NUMBER_NOT_FOUND,
            APARTMENT_NUMBER_VALID
        }

        public PropertyComplex(string PropertyName, DataTable tenantRoster)
        {
            propertyName = PropertyName;
            BuildingAddressList = new List<string>();
            allApartmentNumbers = new List<int>();
            StreetNumbers = new List<int>();
            List<BuildingAndApartment> bldsAntApts =
                CreateBuildingAndApartmentsList(tenantRoster);
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

        public ApartmentNumberValid VerifyApartmentNumber(string aptNumberString, out int apartmentNumber)
        {
            ApartmentNumberValid exitStatus = ApartmentNumberValid.APARTMENT_NUMBER_VALID;

            int aptNumber = 0;
            apartmentNumber = aptNumber;
            bool IsNumeric;
            if (!string.IsNullOrEmpty(aptNumberString))
            {
                IsNumeric = int.TryParse(aptNumberString, out aptNumber);
            }
            else
            {
                return ApartmentNumberValid.APARTMENT_NUMBER_NONNUMERIC;
            }

            if (aptNumber < MinApartmentNumber || aptNumber > MaxApartmentNumber)
            {
                return ApartmentNumberValid.APARTMENT_NUMBER_OUT_OF_RANGE;
            }

            int found = 0;
            found = AllApartmentNumbers.Find(x => x == aptNumber);
            if (found == 0)
            {
                return ApartmentNumberValid.APARTMENT_NUMBER_NOT_FOUND;
            }

            apartmentNumber = aptNumber;
            return exitStatus;
        }

        public MailboxData GetMailBoxList(Building building)
        {
            MailboxData mailboxData = new MailboxData(building);
            List<int> apartmentNumbers = building.ApartmentNumbers;
            foreach (int aptNo in apartmentNumbers)
            {
                mailboxData.addApartmentData(new Apartment(aptNo));
            }

            return mailboxData;
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

        private List<BuildingAndApartment> CreateBuildingAndApartmentsList(DataTable tenantRoster)
        {
            if (tenantRoster == null)
            {
                return null;
            }
            List<BuildingAndApartment> buildingAndApartments = new List<BuildingAndApartment>();
            int LastDataRow = tenantRoster.Rows.Count;

            for (int row = 0; row < LastDataRow; row++)
            {
                DataRow dataRow = tenantRoster.Rows[row];
                buildingAndApartments.Add(CreateBuildAndApartmentFromDataRow(dataRow));
            }

            return buildingAndApartments;
        }

        private BuildingAndApartment CreateBuildAndApartmentFromDataRow(DataRow dataRow)
        {
            string streetAddress = dataRow.Field<string>("Street 1").ToString();
            string apartmentNumString = dataRow.Field<string>("UnitNo").ToString();

            int apartmentNumber;
            Int32.TryParse(apartmentNumString, out apartmentNumber);

            int firstSpace = streetAddress.IndexOf(' ');
            string streetNumber = streetAddress.Substring(0, firstSpace);
            int buildingNumber;
            Int32.TryParse(streetNumber, out buildingNumber);

            BuildingAndApartment currentApt = new BuildingAndApartment(buildingNumber,
                apartmentNumber, streetAddress);
            return currentApt;
        }


    }
}
