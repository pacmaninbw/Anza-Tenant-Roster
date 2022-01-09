using System;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace TenantRosterAutomation
{
    public partial class RentRosterApp : Form
    {
        bool globalsInitialized;
        public RentRosterApp()
        {
            globalsInitialized = Globals.InitializeAllModels();
            InitializeComponent();
        }

        private void RentRosterApp_Load(object sender, EventArgs e)
        {
            if (!globalsInitialized)
            {
                PrintMailboxLists_Button.Enabled = false;
                AddNewResident_Button.Enabled = false;
                DeleteRenter_Button.Enabled = false;
                EditPreferencesDlg preferences_dlg = new EditPreferencesDlg();
                preferences_dlg.Show();
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
            SaveEdits();
        }

        private void RentRosterApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save all changes before quiting.
            Globals.Save();
            Globals.ReleaseAllModels();
        }

        private void SaveEdits()
        {
            Globals.SaveTenantData();
        }
    }
}
