using System;
using System.Drawing;
using System.Windows.Forms;

namespace TenantRosterAutomation
{
    public partial class RentRosterApp : Form
    {
        private bool globalsInitialized;
        public RentRosterApp()
        {
            ReportCurrentStatusWindow statusReport = new ReportCurrentStatusWindow();
            statusReport.MessageText = "Loading preferences and Tenant Data From Excel.";
            statusReport.Show();

            try
            {
                globalsInitialized = Globals.InitializeAllModels();
            }
            catch (PreferenceFileException pfE)
            {
                switch (pfE.PFEType)
                {
                    case PFEType.PFE_VERSION_ID_OLD:
                        MessageBox.Show(pfE.Message, "Preference File Out of Date: ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        // Since we want to rethrow the exception repackage all the inner
                        // data
                        PreferenceFileException fileVersion = new PreferenceFileException(
                            "Preference File Error: " + pfE.Message, pfE.PFEType,
                            pfE.InnerException);
                        throw;
                }
            }
            catch (Exception)
            {
                statusReport.Close();
                throw;
            }

            statusReport.Close();

            InitializeComponent();
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
            try
            {
                printMailboxLists_dialog.Show();
            }
            catch (Exception ex)
            {
                printMailboxLists_dialog.Close();
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                MessageBox.Show(ao.Message, "Excel File Already Open: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RR_SAVEEDITS_BTN_Click(object sender, EventArgs e)
        {
            ReportCurrentStatusWindow SaveStatus = new ReportCurrentStatusWindow();
            SaveStatus.MessageText = "Saving updated tenants and apartments to Excel.";
            SaveStatus.Show();

            try
            {
                Globals.SaveTenantData();
                SaveStatus.Close();
            }
            catch (AlreadyOpenInExcelException ao)
            {
                SaveStatus.Close();
                MessageBox.Show(ao.Message, "Excel File Already Open: ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RentRosterApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save all changes before quiting.
            ReportCurrentStatusWindow SaveStatus = new ReportCurrentStatusWindow();
            SaveStatus.MessageText = "Saving updated tenants and apartments to Excel.";
            SaveStatus.Show();
            Globals.Save();
            Globals.ReleaseAllModels();
            SaveStatus.Close();
        }
    }
}
