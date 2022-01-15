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
                statusReport.Close();
                InitializeComponent();
            }
            catch (PreferenceFileException pfE)
            {
                statusReport.Close();
                switch (pfE.PFEType)
                {
                    // This error is recoverable by editing the preferences and
                    // then saving the preferenecs again.
                    case PFEType.PFE_VERSION_ID_OLD:
                        pfE.PfeReporter();
                        break;

                    // nonrecoverale errors
                    default:
                        pfE.PfeReporter();
                        Close();
                        break;                }
            }
            catch (Exception)
            {
                statusReport.Close();
                throw;
            }
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
            try {
                preferences_dlg.Show();
            }
            catch (PreferenceFileException pfE)
            {
                preferences_dlg.Close();
                pfE.PfeReporter();
                Close();
            }
        }

        private void RR_Quit_BTN_Click(object sender, EventArgs e)
        {
            ReportCurrentStatusWindow SaveStatus = new ReportCurrentStatusWindow();
            SaveStatus.MessageText = "Saving updated tenants and apartments to" +
                " Excel. Exiting Application";
            SaveStatus.Show();

            try
            {
                Globals.Save();
                SaveStatus.Close();
                Globals.ReleaseAllModels();
                Close();
            }
            catch (AlreadyOpenInExcelException ao)
            {
                SaveStatus.Close();
                // Recoverable error if the excel file is closed.
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
                // Recoverable error if the excel file is closed.
                MessageBox.Show(ao.Message, "Excel File Already Open: ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
