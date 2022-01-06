namespace RentRosterAutomation
{
    public class Tenant
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string CoTenantLastName { get; set; }
        public string CoTenantFirstName { get; set; }
        public string Email { get; set; }
        public string HomePhone { get; set; }
        public string RentersInsurancePolicy { get; set; }
        public string LeaseStart { get; set; }
        public string LeaseEnd { get; set; }

        public Tenant()
        {
            FirstName = "";
            LastName = "";
            CoTenantLastName = "";
            CoTenantFirstName = "";
            Email = "";
            HomePhone = "";
            RentersInsurancePolicy = "";
            LeaseStart = "";
        }

        public Tenant(string lastName, string homePhone)
        {
            LastName = lastName;
            HomePhone = homePhone;
            FirstName = "";
            CoTenantLastName = "";
            CoTenantFirstName = "";
            Email = "";
            RentersInsurancePolicy = "";
            LeaseStart = "";
        }

        public string MailboxListOccupantEntry()
        {
            string fullTenantNameString = LastName;

            if (!string.IsNullOrEmpty(CoTenantLastName))
            {
                fullTenantNameString = fullTenantNameString + " // " + CoTenantLastName;
            }

            return fullTenantNameString;
        }

        public string mergedName()
        {
            return (!string.IsNullOrEmpty(FirstName)) ? FirstName + " " + LastName : LastName;
        }

    }
}
