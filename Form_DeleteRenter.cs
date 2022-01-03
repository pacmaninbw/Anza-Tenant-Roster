using System;
using System.Drawing;
using System.Windows.Forms;

namespace RentRosterAutomation
{
    public partial class Form_DeleteRenter : Form
    {
        public int ApartmentNumber { get; set; }
        public CRenter CurrentTenant { get; set; }

        public Form_DeleteRenter()
        {
            ApartmentNumber = 0;
            CurrentTenant = null;
            InitializeComponent();
        }

        private void DR_DeleteRenter_BTN_Click(object sender, EventArgs e)
        {
            Program.excelInteropMethods.DeleteTenant(ApartmentNumber);
            Close();
        }

        private void DeleteRenter_Load(object sender, EventArgs e)
        {
            if (ApartmentNumber != 0 && CurrentTenant != null)
            {
                SetUpApartmentAddressLabel();
                DR_DeleteRenter_BTN.Enabled = false;
                DR_DeleteRenter_BTN.BackColor = Color.Red;
                DR_Cancel_BTN.BackColor = Color.Green;
                DR_TenantName_TB.Enabled = false;

                DR_TenantName_TB.Text = CurrentTenant.mergedName();
            }
        }

        private void SetUpApartmentAddressLabel()
        {
            string apartmentFullAddress = ApartmentNumber.ToString();
            string buildingAddress =
                Program.excelInteropMethods.Complex.FindBuildingByApartment(ApartmentNumber);
            if (!string.IsNullOrEmpty(buildingAddress))
            {
                apartmentFullAddress = buildingAddress + " Apartment " + ApartmentNumber.ToString();
                DR_AptNumber_LAB.Text = apartmentFullAddress;
            }
            else
            {
                DR_AptNumber_LAB.Text = "Apartment Number:      " + apartmentFullAddress;
            }
            DR_AptNumber_LAB.Font = new Font("Arial", 12, FontStyle.Bold);
        }



        private void DR_Renter2DeleteYes_RB_CheckedChanged(object sender, EventArgs e)
        {
            DR_DeleteRenter_BTN.Enabled = true;
            DR_DeleteRenter_BTN.BackColor = Color.Green;
            DR_Cancel_BTN.BackColor = Color.Red;
        }

        private void DR_Renter2DeleteNo_RB_CheckedChanged(object sender, EventArgs e)
        {
            DR_DeleteRenter_BTN.Enabled = false;
            DR_DeleteRenter_BTN.BackColor = Color.Red;
            DR_Cancel_BTN.BackColor = Color.Green;
        }

        private void DR_Cancel_BTN_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
