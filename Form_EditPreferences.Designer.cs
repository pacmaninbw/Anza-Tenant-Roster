
namespace RentRosterAutomation
{
    partial class Form_EditPreferences
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EP_PrintSave_GB = new System.Windows.Forms.GroupBox();
            this.EP_SavelOnly_RB = new System.Windows.Forms.RadioButton();
            this.EP_PrintAndSave_RB = new System.Windows.Forms.RadioButton();
            this.EP_PrintOnly_RB = new System.Windows.Forms.RadioButton();
            this.EP_SavePreferences_BTN = new System.Windows.Forms.Button();
            this.EP_RentRosterFile_GB = new System.Windows.Forms.GroupBox();
            this.EP_RentRosterSheetName_LISTBOX = new System.Windows.Forms.ListBox();
            this.EP_SheetName_TB = new System.Windows.Forms.TextBox();
            this.EP_SpreadSheetName_LAB = new System.Windows.Forms.Label();
            this.EP_FindTenantRoster_BTN = new System.Windows.Forms.Button();
            this.EP_RentRosterLocation_TB = new System.Windows.Forms.TextBox();
            this.EP_RentRosterLocation_Lab = new System.Windows.Forms.Label();
            this.EP_OutputFolder_GB = new System.Windows.Forms.GroupBox();
            this.EP_BrowseFolderLocation_BTN = new System.Windows.Forms.Button();
            this.EP_DefaultFileFolder_TB = new System.Windows.Forms.TextBox();
            this.EP_PrintSave_GB.SuspendLayout();
            this.EP_RentRosterFile_GB.SuspendLayout();
            this.EP_OutputFolder_GB.SuspendLayout();
            this.SuspendLayout();
            // 
            // EP_PrintSave_GB
            // 
            this.EP_PrintSave_GB.Controls.Add(this.EP_SavelOnly_RB);
            this.EP_PrintSave_GB.Controls.Add(this.EP_PrintAndSave_RB);
            this.EP_PrintSave_GB.Controls.Add(this.EP_PrintOnly_RB);
            this.EP_PrintSave_GB.Location = new System.Drawing.Point(40, 20);
            this.EP_PrintSave_GB.Name = "EP_PrintSave_GB";
            this.EP_PrintSave_GB.Size = new System.Drawing.Size(150, 77);
            this.EP_PrintSave_GB.TabIndex = 9;
            this.EP_PrintSave_GB.TabStop = false;
            this.EP_PrintSave_GB.Text = "Printing and Saving Default";
            // 
            // EP_SavelOnly_RB
            // 
            this.EP_SavelOnly_RB.AutoSize = true;
            this.EP_SavelOnly_RB.Location = new System.Drawing.Point(6, 51);
            this.EP_SavelOnly_RB.Name = "EP_SavelOnly_RB";
            this.EP_SavelOnly_RB.Size = new System.Drawing.Size(74, 17);
            this.EP_SavelOnly_RB.TabIndex = 3;
            this.EP_SavelOnly_RB.Text = "Save Only";
            this.EP_SavelOnly_RB.UseVisualStyleBackColor = true;
            this.EP_SavelOnly_RB.CheckedChanged += new System.EventHandler(this.EP_SavelOnly_RB_CheckedChanged);
            // 
            // EP_PrintAndSave_RB
            // 
            this.EP_PrintAndSave_RB.AutoSize = true;
            this.EP_PrintAndSave_RB.Location = new System.Drawing.Point(6, 35);
            this.EP_PrintAndSave_RB.Name = "EP_PrintAndSave_RB";
            this.EP_PrintAndSave_RB.Size = new System.Drawing.Size(95, 17);
            this.EP_PrintAndSave_RB.TabIndex = 2;
            this.EP_PrintAndSave_RB.Text = "Print and Save";
            this.EP_PrintAndSave_RB.UseVisualStyleBackColor = true;
            this.EP_PrintAndSave_RB.CheckedChanged += new System.EventHandler(this.EP_PrintAndSave_RB_CheckedChanged);
            // 
            // EP_PrintOnly_RB
            // 
            this.EP_PrintOnly_RB.AutoSize = true;
            this.EP_PrintOnly_RB.Checked = true;
            this.EP_PrintOnly_RB.Location = new System.Drawing.Point(6, 19);
            this.EP_PrintOnly_RB.Name = "EP_PrintOnly_RB";
            this.EP_PrintOnly_RB.Size = new System.Drawing.Size(70, 17);
            this.EP_PrintOnly_RB.TabIndex = 1;
            this.EP_PrintOnly_RB.TabStop = true;
            this.EP_PrintOnly_RB.Text = "Print Only";
            this.EP_PrintOnly_RB.UseVisualStyleBackColor = true;
            this.EP_PrintOnly_RB.CheckedChanged += new System.EventHandler(this.EP_PrintOnly_RB_CheckedChanged);
            // 
            // EP_SavePreferences_BTN
            // 
            this.EP_SavePreferences_BTN.Location = new System.Drawing.Point(40, 450);
            this.EP_SavePreferences_BTN.Name = "EP_SavePreferences_BTN";
            this.EP_SavePreferences_BTN.Size = new System.Drawing.Size(120, 23);
            this.EP_SavePreferences_BTN.TabIndex = 10;
            this.EP_SavePreferences_BTN.Text = "Save Preferences";
            this.EP_SavePreferences_BTN.UseVisualStyleBackColor = true;
            this.EP_SavePreferences_BTN.Click += new System.EventHandler(this.EP_SavePreferences_BTN_Click);
            // 
            // EP_RentRosterFile_GB
            // 
            this.EP_RentRosterFile_GB.Controls.Add(this.EP_RentRosterSheetName_LISTBOX);
            this.EP_RentRosterFile_GB.Controls.Add(this.EP_SheetName_TB);
            this.EP_RentRosterFile_GB.Controls.Add(this.EP_SpreadSheetName_LAB);
            this.EP_RentRosterFile_GB.Controls.Add(this.EP_FindTenantRoster_BTN);
            this.EP_RentRosterFile_GB.Controls.Add(this.EP_RentRosterLocation_TB);
            this.EP_RentRosterFile_GB.Controls.Add(this.EP_RentRosterLocation_Lab);
            this.EP_RentRosterFile_GB.Location = new System.Drawing.Point(40, 201);
            this.EP_RentRosterFile_GB.Name = "EP_RentRosterFile_GB";
            this.EP_RentRosterFile_GB.Size = new System.Drawing.Size(507, 215);
            this.EP_RentRosterFile_GB.TabIndex = 18;
            this.EP_RentRosterFile_GB.TabStop = false;
            this.EP_RentRosterFile_GB.Text = "Tenant Roster";
            // 
            // EP_RentRosterSheetName_LISTBOX
            // 
            this.EP_RentRosterSheetName_LISTBOX.FormattingEnabled = true;
            this.EP_RentRosterSheetName_LISTBOX.Location = new System.Drawing.Point(339, 109);
            this.EP_RentRosterSheetName_LISTBOX.Name = "EP_RentRosterSheetName_LISTBOX";
            this.EP_RentRosterSheetName_LISTBOX.Size = new System.Drawing.Size(120, 95);
            this.EP_RentRosterSheetName_LISTBOX.TabIndex = 9;
            this.EP_RentRosterSheetName_LISTBOX.SelectedIndexChanged += new System.EventHandler(this.EP_RentRosterSheetName_LISTBOX_SelectedIndexChanged);
            // 
            // EP_SheetName_TB
            // 
            this.EP_SheetName_TB.Location = new System.Drawing.Point(143, 118);
            this.EP_SheetName_TB.Name = "EP_SheetName_TB";
            this.EP_SheetName_TB.Size = new System.Drawing.Size(139, 20);
            this.EP_SheetName_TB.TabIndex = 8;
            this.EP_SheetName_TB.Click += new System.EventHandler(this.EP_SheetName_TB_Click);
            // 
            // EP_SpreadSheetName_LAB
            // 
            this.EP_SpreadSheetName_LAB.AutoSize = true;
            this.EP_SpreadSheetName_LAB.Location = new System.Drawing.Point(7, 120);
            this.EP_SpreadSheetName_LAB.Name = "EP_SpreadSheetName_LAB";
            this.EP_SpreadSheetName_LAB.Size = new System.Drawing.Size(137, 13);
            this.EP_SpreadSheetName_LAB.TabIndex = 21;
            this.EP_SpreadSheetName_LAB.Text = "Tenant Roster Sheet Name";
            // 
            // EP_FindTenantRoster_BTN
            // 
            this.EP_FindTenantRoster_BTN.Location = new System.Drawing.Point(7, 84);
            this.EP_FindTenantRoster_BTN.Name = "EP_FindTenantRoster_BTN";
            this.EP_FindTenantRoster_BTN.Size = new System.Drawing.Size(110, 23);
            this.EP_FindTenantRoster_BTN.TabIndex = 7;
            this.EP_FindTenantRoster_BTN.Text = "Browse Folders";
            this.EP_FindTenantRoster_BTN.UseVisualStyleBackColor = true;
            this.EP_FindTenantRoster_BTN.Click += new System.EventHandler(this.EP_FindRenterRoster_BTN_Click);
            // 
            // EP_RentRosterLocation_TB
            // 
            this.EP_RentRosterLocation_TB.Location = new System.Drawing.Point(35, 49);
            this.EP_RentRosterLocation_TB.Name = "EP_RentRosterLocation_TB";
            this.EP_RentRosterLocation_TB.Size = new System.Drawing.Size(450, 20);
            this.EP_RentRosterLocation_TB.TabIndex = 6;
            this.EP_RentRosterLocation_TB.Click += new System.EventHandler(this.EP_RentRosterLocation_TB_Click);
            this.EP_RentRosterLocation_TB.TextChanged += new System.EventHandler(this.EP_RentRosterLocation_TB_TextChanged);
            // 
            // EP_RentRosterLocation_Lab
            // 
            this.EP_RentRosterLocation_Lab.AutoSize = true;
            this.EP_RentRosterLocation_Lab.Location = new System.Drawing.Point(7, 24);
            this.EP_RentRosterLocation_Lab.Name = "EP_RentRosterLocation_Lab";
            this.EP_RentRosterLocation_Lab.Size = new System.Drawing.Size(94, 13);
            this.EP_RentRosterLocation_Lab.TabIndex = 18;
            this.EP_RentRosterLocation_Lab.Text = "Tenant Roster File";
            // 
            // EP_OutputFolder_GB
            // 
            this.EP_OutputFolder_GB.Controls.Add(this.EP_BrowseFolderLocation_BTN);
            this.EP_OutputFolder_GB.Controls.Add(this.EP_DefaultFileFolder_TB);
            this.EP_OutputFolder_GB.Location = new System.Drawing.Point(40, 100);
            this.EP_OutputFolder_GB.Name = "EP_OutputFolder_GB";
            this.EP_OutputFolder_GB.Size = new System.Drawing.Size(507, 100);
            this.EP_OutputFolder_GB.TabIndex = 19;
            this.EP_OutputFolder_GB.TabStop = false;
            this.EP_OutputFolder_GB.Text = "Default Folder to Save Files";
            // 
            // EP_BrowseFolderLocation_BTN
            // 
            this.EP_BrowseFolderLocation_BTN.Location = new System.Drawing.Point(11, 71);
            this.EP_BrowseFolderLocation_BTN.Name = "EP_BrowseFolderLocation_BTN";
            this.EP_BrowseFolderLocation_BTN.Size = new System.Drawing.Size(110, 23);
            this.EP_BrowseFolderLocation_BTN.TabIndex = 5;
            this.EP_BrowseFolderLocation_BTN.Text = "Browse Folders";
            this.EP_BrowseFolderLocation_BTN.UseVisualStyleBackColor = true;
            this.EP_BrowseFolderLocation_BTN.Click += new System.EventHandler(this.EP_BrowseFolderLocation_BTN_Click);
            // 
            // EP_DefaultFileFolder_TB
            // 
            this.EP_DefaultFileFolder_TB.Location = new System.Drawing.Point(35, 40);
            this.EP_DefaultFileFolder_TB.Name = "EP_DefaultFileFolder_TB";
            this.EP_DefaultFileFolder_TB.Size = new System.Drawing.Size(450, 20);
            this.EP_DefaultFileFolder_TB.TabIndex = 4;
            this.EP_DefaultFileFolder_TB.Click += new System.EventHandler(this.EP_DefaultFileFolder_TB_Click);
            this.EP_DefaultFileFolder_TB.TextChanged += new System.EventHandler(this.EP_DefaultFileFolder_TB_TextChanged);
            // 
            // Form_EditPreferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 530);
            this.Controls.Add(this.EP_OutputFolder_GB);
            this.Controls.Add(this.EP_RentRosterFile_GB);
            this.Controls.Add(this.EP_SavePreferences_BTN);
            this.Controls.Add(this.EP_PrintSave_GB);
            this.Name = "Form_EditPreferences";
            this.Text = "Edit Preferences";
            this.Activated += new System.EventHandler(this.EditPreferences_Form_Activated);
            this.Load += new System.EventHandler(this.Form_EditPreferences_Load);
            this.EP_PrintSave_GB.ResumeLayout(false);
            this.EP_PrintSave_GB.PerformLayout();
            this.EP_RentRosterFile_GB.ResumeLayout(false);
            this.EP_RentRosterFile_GB.PerformLayout();
            this.EP_OutputFolder_GB.ResumeLayout(false);
            this.EP_OutputFolder_GB.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox EP_PrintSave_GB;
        private System.Windows.Forms.RadioButton EP_SavelOnly_RB;
        private System.Windows.Forms.RadioButton EP_PrintAndSave_RB;
        private System.Windows.Forms.RadioButton EP_PrintOnly_RB;
        private System.Windows.Forms.Button EP_SavePreferences_BTN;
        private System.Windows.Forms.GroupBox EP_RentRosterFile_GB;
        private System.Windows.Forms.ListBox EP_RentRosterSheetName_LISTBOX;
        private System.Windows.Forms.TextBox EP_SheetName_TB;
        private System.Windows.Forms.Label EP_SpreadSheetName_LAB;
        private System.Windows.Forms.Button EP_FindTenantRoster_BTN;
        private System.Windows.Forms.TextBox EP_RentRosterLocation_TB;
        private System.Windows.Forms.Label EP_RentRosterLocation_Lab;
        private System.Windows.Forms.GroupBox EP_OutputFolder_GB;
        private System.Windows.Forms.Button EP_BrowseFolderLocation_BTN;
        private System.Windows.Forms.TextBox EP_DefaultFileFolder_TB;
    }
}