using System;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace TenantRosterAutomation
{
    public partial class RentRosterApp : Form
    {
        private bool globalsInitialized;
        public RentRosterApp()
        {
            globalsInitialized = Globals.InitializeAllModels();
            InitializeComponent();
        }

        public void InitializedNowResetButtons()
        {
            globalsInitialized = true;
            PrintMailboxLists_Button.Enabled = false;
            AddNewResident_Button.Enabled = false;
            DeleteRenter_Button.Enabled = false;
        }

        private void RentRosterApp_Load(object sender, EventArgs e)
        {
            RR_Quit_BTN.BackColor = Color.Red;
            if (!globalsInitialized)
            {
                PrintMailboxLists_Button.Enabled = false;
                AddNewResident_Button.Enabled = false;
                DeleteRenter_Button.Enabled = false;

                using (EditPreferencesDlg preferences_dlg = new EditPreferencesDlg())
                {
                    if (preferences_dlg.ShowDialog() == DialogResult.OK)
                    {
                        PrintMailboxLists_Button.Enabled = true;
                        AddNewResident_Button.Enabled = true;
                        DeleteRenter_Button.Enabled = true;
                    }
                }
            }
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
            try
            {
                Globals.Save();
                Globals.ReleaseAllModels();
                Close();
            }
            catch (AlreadyOpenInExcelException ao)
            {
                MessageBox.Show(ao.Message);
            }
        }

        private void RR_SAVEEDITS_BTN_Click(object sender, EventArgs e)
        {
            try
            {
                Globals.SaveTenantData();
            }
            catch (AlreadyOpenInExcelException ao)
            {
                MessageBox.Show(ao.Message);
            }
        }

        private void RentRosterApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save all changes before quiting.
            Globals.Save();
            Globals.ReleaseAllModels();
        }
    }
}
