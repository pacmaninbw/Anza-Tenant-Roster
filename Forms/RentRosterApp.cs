using System;
using System.Drawing;
using System.Windows.Forms;

namespace RentRosterAutomation
{
    public partial class RentRosterApp : Form
    {
        private ExcelInterface excelInteropMethods;

        public UserPreferences MyPersonalPreferences { get; set; }

        public RentRosterApp()
        {
            excelInteropMethods = Program.excelInterface;
            MyPersonalPreferences = Program.userPreferences;
            InitializeComponent();
        }

        private void Form_RentRosterApp_Load(object sender, EventArgs e)
        {
            if (excelInteropMethods.Complex == null)
            {
                PrintMailboxLists_Button.Enabled = false;
                AddNewResident_Button.Enabled = false;
                DeleteRenter_Button.Enabled = false;
            }
            RR_Quit_BTN.BackColor = Color.Red;
        }

        private void PrintMailboxLists_Button_Click(object sender, EventArgs e)
        {
            PrintMailboxListsDlg printMailboxLists_dialog = new PrintMailboxListsDlg();
            printMailboxLists_dialog.Show();
        }

        private void AddNewResident_Button_Click(object sender, EventArgs e)
        {
            ApartmentNumberVerifier verifier_Form = new ApartmentNumberVerifier();
            verifier_Form.NextAction = ApartmentNumberVerifier.NextActionEnum.ADD;
            verifier_Form.Show();
        }

        private void DeleteRenter_Button_Click(object sender, EventArgs e)
        {
            ApartmentNumberVerifier verifier_Form = new ApartmentNumberVerifier();
            verifier_Form.NextAction = ApartmentNumberVerifier.NextActionEnum.DELETE;
            verifier_Form.Show();
        }

        private void EditPreferences_BTN_Click(object sender, EventArgs e)
        {
            EditPreferencesDlg preferences_dlg = new EditPreferencesDlg();
            preferences_dlg.Show();
        }

        private void RR_Quit_BTN_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RR_SAVEEDITS_BTN_Click(object sender, EventArgs e)
        {
            ExcelInterface excelInteropMethods = Program.excelInterface;
            if (excelInteropMethods.HaveEditsToSave)
            {
                excelInteropMethods.SaveEditsCloseWorkbookExitExcel();
            }
        }
    }
}
