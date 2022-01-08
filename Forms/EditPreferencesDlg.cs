using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace TenantRosterAutomation
{
    public partial class EditPreferencesDlg : Form
    {
        public EditPreferencesDlg()
        {
            InitializeComponent();
        }

        private void EditPreferencesDlg_Load(object sender, EventArgs e)
        {
            EP_DefaultFileFolder_TB.Text = Globals.DefaultSaveDirectory;
            EP_RentRosterLocation_TB.Text = Globals.ExcelWorkBookFullFileSpec;
            EP_SheetName_TB.Text = Globals.ExcelWorkSheetName;
            switch (Globals.PrintSave)
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
            if (Globals.Preferences != null)
            {
            }
            else
            {
                EP_PrintOnly_RB.Checked = true;
                EP_SheetName_TB.Enabled = false;
            }
            EP_RentRosterSheetName_LISTBOX.Visible = false;
        }

        private void EP_PrintAndSave_RB_CheckedChanged(object sender, EventArgs e)
        {
            Globals.PrintSave = PrintSavePreference.PrintSave.PrintAndSave;
        }

        private void EP_SavelOnly_RB_CheckedChanged(object sender, EventArgs e)
        {
            Globals.PrintSave = PrintSavePreference.PrintSave.SaveOnly;
        }

        private void EP_PrintOnly_RB_CheckedChanged(object sender, EventArgs e)
        {
            Globals.PrintSave = PrintSavePreference.PrintSave.PrintOnly;
        }

        private void EP_DefaultFileFolder_TB_TextChanged(object sender, EventArgs e)
        {
            Globals.DefaultSaveDirectory = EP_DefaultFileFolder_TB.Text;
        }

        private void findDefaultFolderLocationExecute(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    EP_DefaultFileFolder_TB.Text = fbd.SelectedPath;
                    Globals.DefaultSaveDirectory = fbd.SelectedPath;
                }
            }
            Globals.SavePreferences();
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
            Globals.ExcelWorkBookFullFileSpec = EP_RentRosterLocation_TB.Text;
        }

        private void findTenantRosterExcelFile(object sender, EventArgs e)
        {
            string rentRosterFile = "";
            OpenFileDialog FindTenantRoster = new OpenFileDialog();
            FindTenantRoster.InitialDirectory = "c:\\";
            FindTenantRoster.Filter = "Excel Files| *.xls; *.xlsx; *.xlsm";
            if (FindTenantRoster.ShowDialog() == DialogResult.OK)
            {
                rentRosterFile = FindTenantRoster.FileName;
                EP_RentRosterLocation_TB.Text = rentRosterFile;
                Globals.ExcelWorkBookFullFileSpec = rentRosterFile;
                Globals.SavePreferences();
            }
        }

        private void fillSheetSelectorListBox()
        {
            List<string> sheetNames = null;
            if (Globals.ExcelFile != null)
            {
                sheetNames = Globals.ExcelFile.GetWorkSheetCollection();
            }

            if (sheetNames == null)
            {
                return;
            }

            EP_RentRosterSheetName_LISTBOX.DataSource = sheetNames;
            EP_RentRosterSheetName_LISTBOX.Visible = true;
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
            Globals.SavePreferences();
            Close();
        }

        private void EP_RentRosterSheetName_LISTBOX_SelectedIndexChanged(object sender, EventArgs e)
        {
            Globals.ExcelWorkSheetName = EP_RentRosterSheetName_LISTBOX.SelectedItem.ToString();
            EP_SheetName_TB.Text = Globals.Preferences.RentRosterSheet;
        }

        private void EP_SheetName_TB_Click(object sender, EventArgs e)
        {
            fillSheetSelectorListBox();
        }
    }
}
