using System;
using System.Drawing;
using System.Windows.Forms;

namespace RentRosterAutomation
{
    public partial class Form_ApartmentNumberVerifier : Form
    {
        private CPropertyComplex complex;

        public enum NextActionEnum
        {
            ADD,
            DELETE,
            EDIT
        }

        public int ApartmentNumber;
        public CRenter TenantData { get; private set; }
        public NextActionEnum NextAction { get; set; }

        public Form_ApartmentNumberVerifier()
        {
            complex = Program.excelInteropMethods.Complex;
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
            string aptNumberString = ANV_ApartmentNumber_TB.Text;
            int aptNumber = 0;
            int minAptNo = complex.MinApartmentNumber;
            int maxAptNo = complex.MaxApartmentNumber;
            bool IsNumeric;
            if (!string.IsNullOrEmpty(aptNumberString))
            {
                IsNumeric = int.TryParse(aptNumberString, out aptNumber);
            }
            else
            {
                MessageBox.Show("Please enter a number in the box.");
                ANV_ApartmentNumber_TB.BackColor = Color.Yellow;
                ActiveControl = ANV_ApartmentNumber_TB;
                return;
            }

            if (aptNumber < complex.MinApartmentNumber || aptNumber > complex.MaxApartmentNumber)
            {
                string msg = "The apartment is out of range[" + minAptNo.ToString() +
                    ", " + maxAptNo.ToString() + "] please enter a valid apartment number.";
                MessageBox.Show(msg);
                ANV_ApartmentNumber_TB.BackColor = Color.Yellow;
                ActiveControl = ANV_ApartmentNumber_TB;
                return;
            }

            int found = 0;
            found = complex.AllApartmentNumbers.Find(x => x == aptNumber);
            if (found == 0)
            {
                MessageBox.Show("The number entered: " + aptNumber +
                    " was not found in the list of apartments.");
                ANV_ApartmentNumber_TB.BackColor = Color.Yellow;
                ActiveControl = ANV_ApartmentNumber_TB;
                return;
            }

            TenantData = Program.excelInteropMethods.GetTenant(aptNumber);

            switch (NextAction)
            {
                case NextActionEnum.EDIT:
                case NextActionEnum.ADD:
                    Form_AddOrEditResident addNewResident = new Form_AddOrEditResident();
                    addNewResident.currentTenant = TenantData;
                    addNewResident.ApartmentNumber = aptNumber;
                    addNewResident.Show();
                    break;

                case NextActionEnum.DELETE:
                    Form_DeleteRenter deleteRenter_dlg = new Form_DeleteRenter();
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
