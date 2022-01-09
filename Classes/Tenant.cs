namespace TenantRosterAutomation
{
    // Models a tenant in an apartment.
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
            InitAllFields();
        }

        public Tenant(string lastName, string homePhone)
        {
            InitAllFields();
            LastName = lastName;
            HomePhone = homePhone;
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

        private void InitAllFields()
        {
            FirstName = "";
            LastName = "";
            CoTenantLastName = "";
            CoTenantFirstName = "";
            Email = "";
            HomePhone = "";
            RentersInsurancePolicy = "";
            LeaseStart = "";
            LeaseEnd = "";
        }
    }
}
