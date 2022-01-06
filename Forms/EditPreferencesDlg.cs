using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace TenantRosterAutomation
{
    public partial class EditPreferencesDlg : Form
    {
        private UserPreferences preferences;
        private ExcelInterface excelInterface;

        public EditPreferencesDlg()
        {
            preferences = Program.userPreferences;
            excelInterface = Program.excelInterface;
            if (excelInterface == null)
            {
                excelInterface = new ExcelInterface(preferences.RentRosterFile,
                    preferences.RentRosterSheet);
                Program.excelInterface = excelInterface;
            }
            InitializeComponent();
        }

        private void Form_EditPreferences_Load(object sender, EventArgs e)
        {
            if (preferences.HavePreferenceData)
            {
                EP_DefaultFileFolder_TB.Text = preferences.DefaultSaveDirectory;
                EP_RentRosterLocation_TB.Text = preferences.RentRosterFile;
                EP_SheetName_TB.Text = preferences.RentRosterSheet;
                switch (preferences.PrintSaveOptions)
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
            }
            else
            {
                EP_PrintOnly_RB.Checked = true;
                EP_SheetName_TB.Enabled = false;
            }
            EP_RentRosterSheetName_LISTBOX.Visible = false;
        }

        private void EditPreferences_Form_Activated(object sender, EventArgs e)
        {
        }

        private void EP_PrintAndSave_RB_CheckedChanged(object sender, EventArgs e)
        {
            preferences.PrintSaveOptions = PrintSavePreference.PrintSave.PrintAndSave;
        }

        private void EP_SavelOnly_RB_CheckedChanged(object sender, EventArgs e)
        {
            preferences.PrintSaveOptions = PrintSavePreference.PrintSave.SaveOnly;
        }

        private void EP_PrintOnly_RB_CheckedChanged(object sender, EventArgs e)
        {
            preferences.PrintSaveOptions = PrintSavePreference.PrintSave.PrintOnly;
        }

        private void EP_DefaultFileFolder_TB_TextChanged(object sender, EventArgs e)
        {
            preferences.DefaultSaveDirectory = EP_DefaultFileFolder_TB.Text;
        }

        private void findDefaultFolderLocationExecute(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    EP_DefaultFileFolder_TB.Text = fbd.SelectedPath;
                    preferences.DefaultSaveDirectory = fbd.SelectedPath;
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
            preferences.RentRosterFile = EP_RentRosterLocation_TB.Text;
            excelInterface.WorkbookName = preferences.RentRosterFile;
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
                preferences.RentRosterFile = rentRosterFile;
            }
        }

        private void fillSheetSelectorListBox()
        {
            List<string> sheetNames = excelInterface.GetSheetNames();
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
            preferences.SavePreferencesToFile("./MyPersonalRentRosterPreferences.txt");
            Program.excelInterface.PreferencesUpdated(preferences);
            Close();
        }

        private void EP_RentRosterSheetName_LISTBOX_SelectedIndexChanged(object sender, EventArgs e)
        {
            preferences.RentRosterSheet = EP_RentRosterSheetName_LISTBOX.SelectedItem.ToString();
            EP_SheetName_TB.Text = preferences.RentRosterSheet;
        }

        private void EP_SheetName_TB_Click(object sender, EventArgs e)
        {
            fillSheetSelectorListBox();
        }
    }
}
