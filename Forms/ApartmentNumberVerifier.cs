using System;
using System.Drawing;
using System.Windows.Forms;

namespace TenantRosterAutomation
{
    public partial class ApartmentNumberVerifier : Form
    {
        public enum NextActionEnum
        {
            ADD,
            DELETE,
            EDIT
        }

        public int ApartmentNumber;
        public Tenant TenantData { get; private set; }
        public NextActionEnum NextAction { get; set; }

        public ApartmentNumberVerifier()
        {
            TenantData = null;
            InitializeComponent();
        }

        private void APV_FindApartment_BTN_Click(object sender, EventArgs e)
        {
            VerifyApartmentNumber();
        }

        private void ANV_ApartmentNumber_TB_TextChanged(object sender, EventArgs e)
        {
            ANV_FindApartment_BTN.Enabled = true;
        }

        private void ApartmentNumberVerifier_Form_Load(object sender, EventArgs e)
        {
            ANV_FindApartment_BTN.Enabled = false;
            ANV_ApartmentNumber_TB.KeyDown += new KeyEventHandler(ANV_ApartmentNumber_TB_KeyDown);
        }

        private void ANV_ApartmentNumber_TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                VerifyApartmentNumber();
            }
        }

        private void VerifyApartmentNumber()
        {
            int aptNumber = 0;
            PropertyComplex.ApartmentNumberValid validApartmentId =
                Globals.Complex.VerifyApartmentNumber(ANV_ApartmentNumber_TB.Text, out aptNumber);

            switch (validApartmentId)
            {
                case PropertyComplex.ApartmentNumberValid.APARTMENT_NUMBER_NONNUMERIC:
                    MessageBox.Show("Please enter a number in the box.");
                    ANV_ApartmentNumber_TB.BackColor = Color.Yellow;
                    ActiveControl = ANV_ApartmentNumber_TB;
                    return;

                case PropertyComplex.ApartmentNumberValid.APARTMENT_NUMBER_OUT_OF_RANGE:
                    int minAptNo = Globals.Complex.MinApartmentNumber;
                    int maxAptNo = Globals.Complex.MaxApartmentNumber;
                    string msg = "The apartment is out of range[" + minAptNo.ToString() +
                        ", " + maxAptNo.ToString() + "] please enter a valid apartment number.";
                    MessageBox.Show(msg);
                    ANV_ApartmentNumber_TB.BackColor = Color.Yellow;
                    ActiveControl = ANV_ApartmentNumber_TB;
                    return;

                case PropertyComplex.ApartmentNumberValid.APARTMENT_NUMBER_NOT_FOUND:
                    MessageBox.Show("The number entered: " + aptNumber +
                        " was not found in the list of apartments.");
                    ANV_ApartmentNumber_TB.BackColor = Color.Yellow;
                    ActiveControl = ANV_ApartmentNumber_TB;
                    return;
            }

            TenantData = Globals.TenantRoster.GetTenant(aptNumber);

            switch (NextAction)
            {
                case NextActionEnum.EDIT:
                case NextActionEnum.ADD:
                    AddOrEditResidentDlg addNewResident = new AddOrEditResidentDlg();
                    addNewResident.CurrentTenant = TenantData;
                    addNewResident.ApartmentNumber = aptNumber;
                    addNewResident.Show();
                    break;

                case NextActionEnum.DELETE:
                    DeleteRenterDlg deleteRenter_dlg = new DeleteRenterDlg();
                    deleteRenter_dlg.CurrentTenant = TenantData;
                    deleteRenter_dlg.ApartmentNumber = aptNumber;
                    deleteRenter_dlg.Show();
                    break;

                default:
                    MessageBox.Show("Programmer error: NextAction = " +
                        NextAction.ToString() +
                        " this action is not implemented.");
                    break;
            }
            Close();
        }
    }
}
