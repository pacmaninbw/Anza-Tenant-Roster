using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace TenantRosterAutomation
{
    public partial class EditPreferencesDlg : Form
    {
        private UserPreferences localPreferences;

        public EditPreferencesDlg()
        {
            localPreferences = new UserPreferences();
            localPreferences.CopyValues(Globals.Preferences, true);
            InitializeComponent();
        }

        private void EditPreferencesDlg_Load(object sender, EventArgs e)
        {
            EP_DefaultFileFolder_TB.Text = localPreferences.DefaultSaveDirectory;
            EP_RentRosterLocation_TB.Text = localPreferences.ExcelWorkBookFullFileSpec;
            EP_SheetName_TB.Text = localPreferences.ExcelWorkSheetName;
            switch (localPreferences.PrintSaveOptions)
            {
                case PrintSavePreference.PrintSave.PrintAndSave:
                    EP_PrintAndSave_RB.Checked = true;
                    break;
                case PrintSavePreference.PrintSave.SaveOnly:
                    EP_SavelOnly_RB.Checked = true;
                    break;
                default:
                    EP_PrintOnly_RB.Checked = true;
                    break;
            }
            EP_SheetName_TB.Enabled = false;
            EP_RentRosterSheetName_LISTBOX.Visible = false;
        }

        private void EP_PrintAndSave_RB_CheckedChanged(object sender, EventArgs e)
        {
            localPreferences.PrintSaveOptions = PrintSavePreference.PrintSave.PrintAndSave;
        }

        private void EP_SavelOnly_RB_CheckedChanged(object sender, EventArgs e)
        {
            localPreferences.PrintSaveOptions = PrintSavePreference.PrintSave.SaveOnly;
        }

        private void EP_PrintOnly_RB_CheckedChanged(object sender, EventArgs e)
        {
            localPreferences.PrintSaveOptions = PrintSavePreference.PrintSave.PrintOnly;
        }

        private void EP_DefaultFileFolder_TB_TextChanged(object sender, EventArgs e)
        {
            localPreferences.DefaultSaveDirectory = EP_DefaultFileFolder_TB.Text;
        }

        private void findDefaultFolderLocationExecute(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    EP_DefaultFileFolder_TB.Text = fbd.SelectedPath;
                    localPreferences.DefaultSaveDirectory = fbd.SelectedPath;
                }
            }
        }

        private void EP_DefaultFileFolder_TB_Click(object sender, EventArgs e)
        {
            findDefaultFolderLocationExecute(sender, e);
        }

        private void EP_BrowseFolderLocation_BTN_Click(object sender, EventArgs e)
        {
            findDefaultFolderLocationExecute(sender, e);
        }

        private void EP_RentRosterLocation_TB_TextChanged(object sender, EventArgs e)
        {
            localPreferences.ExcelWorkBookFullFileSpec = EP_RentRosterLocation_TB.Text;
        }

        private void findTenantRosterExcelFile(object sender, EventArgs e)
        {
            string tenantRosterFile = "";
            OpenFileDialog FindTenantRoster = new OpenFileDialog();
            FindTenantRoster.InitialDirectory = "c:\\";
            FindTenantRoster.Filter = "Excel Files| *.xls; *.xlsx; *.xlsm";
            if (FindTenantRoster.ShowDialog() == DialogResult.OK)
            {
                tenantRosterFile = FindTenantRoster.FileName;
                EP_RentRosterLocation_TB.Text = tenantRosterFile;
                localPreferences.ExcelWorkBookFullFileSpec = tenantRosterFile;
            }
        }

        private void fillSheetSelectorListBox()
        {
            List<string> sheetNames = null;
            if (!string.IsNullOrEmpty(localPreferences.ExcelWorkBookFullFileSpec))
            {
                ExcelFileData ExcelFile = new ExcelFileData(localPreferences.ExcelWorkBookFullFileSpec,
                    localPreferences.ExcelWorkSheetName);
                sheetNames = ExcelFile.GetWorkSheetCollection();
            }

            if (sheetNames == null)
            {
                return;
            }

            EP_RentRosterSheetName_LISTBOX.DataSource = sheetNames;
            EP_RentRosterSheetName_LISTBOX.Visible = true;
            EP_SheetName_TB.Enabled = true;
        }
        private void EP_FindRenterRoster_BTN_Click(object sender, EventArgs e)
        {
            findTenantRosterExcelFile(sender, e);
            fillSheetSelectorListBox();
        }

        private void EP_RentRosterLocation_TB_Click(object sender, EventArgs e)
        {
            findTenantRosterExcelFile(sender, e);
            fillSheetSelectorListBox();
        }

        private void EP_SavePreferences_BTN_Click(object sender, EventArgs e)
        {
            Globals.Preferences.CopyValues(localPreferences);
            Globals.SavePreferences();
            Close();
        }

        private void EP_RentRosterSheetName_LISTBOX_SelectedIndexChanged(object sender, EventArgs e)
        {
            localPreferences.ExcelWorkSheetName = EP_RentRosterSheetName_LISTBOX.SelectedItem.ToString();
            EP_SheetName_TB.Text = localPreferences.ExcelWorkSheetName;
        }

        private void EP_SheetName_TB_Click(object sender, EventArgs e)
        {
            fillSheetSelectorListBox();
        }
    }
}
