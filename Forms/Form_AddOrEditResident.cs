using System;
using System.Drawing;
using System.Windows.Forms;

namespace RentRosterAutomation
{
    public partial class Form_AddOrEditResident : Form
    {
        private CExcelInteropMethods interopMethods;
        public int ApartmentNumber { get; set; }
        public CRenter currentTenant { get; set; }

        public Form_AddOrEditResident()
        {
            interopMethods = Program.excelInteropMethods;
            InitializeComponent();
        }

        private void ANR_SaveNewTenant_BTN_Click(object sender, EventArgs e)
        {
            currentTenant.LastName = ANR_TenantLastName_TB.Text;
            currentTenant.FirstName = ANR_TenantFirstName_TB.Text;
            currentTenant.LeaseStart = ANR_MoveInDate_TB.Text;
            currentTenant.LeaseEnd = ANR_LeaseEnd_TB.Text;
            currentTenant.HomePhone = ANR_HomePhone_TB.Text;
            currentTenant.CoTenantLastName = ANR_CoTenantLastName_TB.Text;
            currentTenant.CoTenantFirstName = ANR_AdditionalOccupantFirstName_TB.Text;
            currentTenant.RentersInsurancePolicy = ANR_RenterInsurance_TB.Text;
            currentTenant.Email = ANR_AlternateContact_TB.Text;
            interopMethods.AddEditTenant(ApartmentNumber, currentTenant);
            Close();
        }

        private void AddNewResident_Form_Load(object sender, EventArgs e)
        {
            SetUpApartmentAddressLabel();
            ANR_SaveNewTenant_BTN.BackColor = Color.Green;
            ANR_Cancel_BTN.BackColor = Color.Red;
            if (currentTenant != null)
            {
                ANR_TenantLastName_TB.Text = currentTenant.LastName;
                ANR_TenantFirstName_TB.Text = currentTenant.FirstName;
                ANR_HomePhone_TB.Text = currentTenant.HomePhone;
                ANR_MoveInDate_TB.Text = currentTenant.LeaseStart;
                ANR_LeaseEnd_TB.Text = currentTenant.LeaseEnd;
                ANR_CoTenantLastName_TB.Text = currentTenant.CoTenantLastName;
                ANR_AdditionalOccupantFirstName_TB.Text = currentTenant.CoTenantFirstName;
                ANR_AlternateContact_TB.Text = currentTenant.Email;
                ANR_RenterInsurance_TB.Text = currentTenant.RentersInsurancePolicy;
            }
        }

        private void SetUpApartmentAddressLabel()
        {
            string apartmentFullAddress = ApartmentNumber.ToString();
            string buildingAddress =
                interopMethods.Complex.FindBuildingByApartment(ApartmentNumber);
            if (!string.IsNullOrEmpty(buildingAddress))
            {
                apartmentFullAddress = buildingAddress + " Apartment " + ApartmentNumber.ToString();
                ANR_ApartmentNumber_Label.Text = apartmentFullAddress;
            }
            else
            {
                ANR_ApartmentNumber_Label.Text = "Apartment Number:      " + apartmentFullAddress;
            }
            ANR_ApartmentNumber_Label.Font = new Font("Arial", 12, FontStyle.Bold);
        }

        private void ANR_Cancel_BTN_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
