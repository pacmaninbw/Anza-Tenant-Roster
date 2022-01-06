using System;
using System.Drawing;
using System.Windows.Forms;

namespace TenantRosterAutomation
{
    public partial class AddOrEditResidentDlg : Form
    {
        private ExcelInterface interopMethods;
        public int ApartmentNumber { get; set; }
        public Tenant CurrentTenant { get; set; }

        public AddOrEditResidentDlg()
        {
            interopMethods = Program.excelInterface;
            InitializeComponent();
        }

        private void ANR_SaveNewTenant_BTN_Click(object sender, EventArgs e)
        {
            CurrentTenant.LastName = ANR_TenantLastName_TB.Text;
            CurrentTenant.FirstName = ANR_TenantFirstName_TB.Text;
            CurrentTenant.LeaseStart = ANR_MoveInDate_TB.Text;
            CurrentTenant.LeaseEnd = ANR_LeaseEnd_TB.Text;
            CurrentTenant.HomePhone = ANR_HomePhone_TB.Text;
            CurrentTenant.CoTenantLastName = ANR_CoTenantLastName_TB.Text;
            CurrentTenant.CoTenantFirstName = ANR_AdditionalOccupantFirstName_TB.Text;
            CurrentTenant.RentersInsurancePolicy = ANR_RenterInsurance_TB.Text;
            CurrentTenant.Email = ANR_AlternateContact_TB.Text;
            interopMethods.AddEditTenant(ApartmentNumber, CurrentTenant);
            Close();
        }

        private void AddNewResident_Form_Load(object sender, EventArgs e)
        {
            SetUpApartmentAddressLabel();
            ANR_SaveNewTenant_BTN.BackColor = Color.Green;
            ANR_Cancel_BTN.BackColor = Color.Red;
            if (CurrentTenant != null)
            {
                ANR_TenantLastName_TB.Text = CurrentTenant.LastName;
                ANR_TenantFirstName_TB.Text = CurrentTenant.FirstName;
                ANR_HomePhone_TB.Text = CurrentTenant.HomePhone;
                ANR_MoveInDate_TB.Text = CurrentTenant.LeaseStart;
                ANR_LeaseEnd_TB.Text = CurrentTenant.LeaseEnd;
                ANR_CoTenantLastName_TB.Text = CurrentTenant.CoTenantLastName;
                ANR_AdditionalOccupantFirstName_TB.Text = CurrentTenant.CoTenantFirstName;
                ANR_AlternateContact_TB.Text = CurrentTenant.Email;
                ANR_RenterInsurance_TB.Text = CurrentTenant.RentersInsurancePolicy;
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
