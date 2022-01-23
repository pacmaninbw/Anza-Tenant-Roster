using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TenantRosterAutomation
{
    public class ExcelFileLocatorGroupBox : GroupBox
    {
        private ListBox SheetNameSelector_LB;
        private TextBox Excel_SheetName_TB;
        private Label ExcelSheetName_LAB;
        private Button FindExcelFile_BTN;
        private TextBox TenantRosterLocator_TB;
        private Label TenantRosterLocator_LAB;

        public string ExcelFileName { get; set; }
        public string ExcelWorkSheetName { get; set; }

        public ExcelFileLocatorGroupBox(int firstTabStop)
        {
            Width = 507;
            Height = 215;
            Text = "Tenant Roster";
            Name = "Common_Tenant_Roster_File_GB";

            TenantRosterLocator_TB = InitTenantRosterLocatorTextBox(1);
            Controls.Add(TenantRosterLocator_TB);

            FindExcelFile_BTN = InitFindExcelFileButton(2);
            Controls.Add(FindExcelFile_BTN);

            TenantRosterLocator_LAB = InitTenantFile_LAB();
            Controls.Add(TenantRosterLocator_LAB);

            SheetNameSelector_LB = InitSheetSelector(3);
            Controls.Add(SheetNameSelector_LB);

            Excel_SheetName_TB = InitSheetName(4);
            Controls.Add(Excel_SheetName_TB);

            ExcelSheetName_LAB = InitSpreadSheetNameLabel();
            Controls.Add(ExcelSheetName_LAB);
        }

        private TextBox InitTenantRosterLocatorTextBox(int tabStopId)
        {
            TextBox TenantRosterLocator_TB = new TextBox
            {
                Location = new Point(35, 49),
                Name = "TenantRosterLocator_TB",
                Size = new Size(450, 20),
                TabIndex = tabStopId
            };
            TenantRosterLocator_TB.Click += new EventHandler(TenantRosterLocator_TB_Click);
            TenantRosterLocator_TB.TextChanged += new EventHandler(
                TenantRosterLocator_TB_TextChanged);

            return TenantRosterLocator_TB;
        }

        private Label InitTenantFile_LAB()
        {
            Label TenantFile_LAB = new Label
            {
                AutoSize = true,
                Location = new Point(7, 24),
                Name = "TenantRosterLocator_LAB",
                Size = new Size(94, 13),
                Text = "Tenant Roster File"
            };

            return TenantFile_LAB;
        }

        private ListBox InitSheetSelector(int tabStopId)
        {
            ListBox SheetSelector_LB = new ListBox
            {
                FormattingEnabled = true,
                Location = new Point(339, 109),
                Name = "SheetNameSelector_LB",
                Size = new Size(120, 95),
                TabIndex = tabStopId
            };
            SheetSelector_LB.SelectedIndexChanged += new EventHandler(
                SheetNameSelector_LB_SelectedIndexChanged);

            return SheetSelector_LB;
        }

        private TextBox InitSheetName(int tabStopId)
        {
            TextBox SheetName_TB = new TextBox()
            {
                Location = new Point(143, 118),
                Name = "Excel_SheetName_TB",
                Size = new Size(139, 20),
                TabIndex = tabStopId,
                TabStop = false
            };

            SheetName_TB.Click += new EventHandler(ExcelSheetName_TB_Click);

            return SheetName_TB;
        }

        private Label InitSpreadSheetNameLabel()
        {
            Label SpreadSheetName_LAB = new Label()
            {
                AutoSize = true,
                Location = new Point(7, 120),
                Name = "ExcelSheetName_LAB",
                Size = new Size(137, 13),
                Text = "Tenant Roster Sheet Name"
            };

            return SpreadSheetName_LAB;
        }

        private Button InitFindExcelFileButton(int tabIndexId)
        {
            Button FindExcelFile_BTN = new Button()
            {
                Location = new Point(7, 84),
                Name = "FindExcelFile_BTN",
                Size = new Size(110, 23),
                TabIndex = tabIndexId,
                TabStop = false,
                Text = "Browse Folders",
                UseVisualStyleBackColor = true
            };
            Click += new System.EventHandler(EP_FindRenterRoster_BTN_Click);

            return FindExcelFile_BTN;
        }

        private void TenantRosterLocator_TB_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TenantRosterLocator_TB.Text))
            {
                FindTenantRosterExcelFile(sender, e);
            }
            FillSheetSelectorListBox();
        }

        private void EP_FindRenterRoster_BTN_Click(object sender, EventArgs e)
        {
            FindTenantRosterExcelFile(sender, e);
            FillSheetSelectorListBox();
        }

        private void SheetNameSelector_LB_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExcelWorkSheetName = SheetNameSelector_LB.SelectedItem.ToString();
            Excel_SheetName_TB.Text = ExcelWorkSheetName;
        }

        private void ExcelSheetName_TB_Click(object sender, EventArgs e)
        {
            FillSheetSelectorListBox();
        }

        private void TenantRosterLocator_TB_TextChanged(object sender, EventArgs e)
        {
            ExcelFileName = TenantRosterLocator_TB.Text;
        }

        private void FindTenantRosterExcelFile(object sender, EventArgs e)
        {
            OpenFileDialog FindTenantRoster = new OpenFileDialog()
            {
                InitialDirectory = "c:\\",
                Filter = "Excel Files| *.xls; *.xlsx; *.xlsm"
            };

            if (FindTenantRoster.ShowDialog() == DialogResult.OK)
            {
                ExcelFileName = FindTenantRoster.FileName;
                TenantRosterLocator_TB.Text = ExcelFileName;
            }
        }

        private void FillSheetSelectorListBox()
        {
            List<string> sheetNames = null;
            if (!string.IsNullOrEmpty(ExcelFileName))
            {
                ExcelFileData ExcelFile = new ExcelFileData(ExcelFileName, null);
                sheetNames = ExcelFile.GetWorkSheetCollection();
            }

            if (sheetNames == null)
            {
                return;
            }

            SheetNameSelector_LB.DataSource = sheetNames;
            SheetNameSelector_LB.Visible = true;
            SheetNameSelector_LB.Enabled = true;
        }
    }
}
