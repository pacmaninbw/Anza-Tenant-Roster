
namespace RentRosterAutomation
{
    partial class Form_PrintMailboxLists
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
            this.SelectBuilding2Print_listBox = new System.Windows.Forms.ListBox();
            this.AddDateToFileName_CB = new System.Windows.Forms.CheckBox();
            this.AddDateUnderAddress_CB = new System.Windows.Forms.CheckBox();
            this.PML_SaveAndPrint_Button = new System.Windows.Forms.Button();
            this.PML_PrintOnly_RB = new System.Windows.Forms.RadioButton();
            this.PML_PrintSave_GB = new System.Windows.Forms.GroupBox();
            this.PML_SavelOnly_RB = new System.Windows.Forms.RadioButton();
            this.PML_PrintAndSave_RB = new System.Windows.Forms.RadioButton();
            this.PML_PrintSave_GB.SuspendLayout();
            this.SuspendLayout();
            // 
            // SelectBuilding2Print_listBox
            // 
            this.SelectBuilding2Print_listBox.FormattingEnabled = true;
            this.SelectBuilding2Print_listBox.Location = new System.Drawing.Point(40, 28);
            this.SelectBuilding2Print_listBox.Name = "SelectBuilding2Print_listBox";
            this.SelectBuilding2Print_listBox.Size = new System.Drawing.Size(180, 95);
            this.SelectBuilding2Print_listBox.TabIndex = 1;
            this.SelectBuilding2Print_listBox.SelectedIndexChanged += new System.EventHandler(this.SelectBuilding2Print_listBox_SelectedIndexChanged);
            // 
            // AddDateToFileName_CB
            // 
            this.AddDateToFileName_CB.AutoSize = true;
            this.AddDateToFileName_CB.Location = new System.Drawing.Point(249, 28);
            this.AddDateToFileName_CB.Name = "AddDateToFileName_CB";
            this.AddDateToFileName_CB.Size = new System.Drawing.Size(133, 17);
            this.AddDateToFileName_CB.TabIndex = 3;
            this.AddDateToFileName_CB.Text = "Add Date to File Name";
            this.AddDateToFileName_CB.UseVisualStyleBackColor = true;
            this.AddDateToFileName_CB.CheckedChanged += new System.EventHandler(this.AddDateToFileName_CB_CheckedChanged);
            // 
            // AddDateUnderAddress_CB
            // 
            this.AddDateUnderAddress_CB.AutoSize = true;
            this.AddDateUnderAddress_CB.Location = new System.Drawing.Point(249, 49);
            this.AddDateUnderAddress_CB.Name = "AddDateUnderAddress_CB";
            this.AddDateUnderAddress_CB.Size = new System.Drawing.Size(144, 17);
            this.AddDateUnderAddress_CB.TabIndex = 4;
            this.AddDateUnderAddress_CB.Text = "Add Date Under Address";
            this.AddDateUnderAddress_CB.UseVisualStyleBackColor = true;
            this.AddDateUnderAddress_CB.CheckedChanged += new System.EventHandler(this.AddDateUnderAddress_CB_CheckedChanged);
            // 
            // PML_SaveAndPrint_Button
            // 
            this.PML_SaveAndPrint_Button.Location = new System.Drawing.Point(151, 164);
            this.PML_SaveAndPrint_Button.Name = "PML_SaveAndPrint_Button";
            this.PML_SaveAndPrint_Button.Size = new System.Drawing.Size(120, 23);
            this.PML_SaveAndPrint_Button.TabIndex = 2;
            this.PML_SaveAndPrint_Button.Text = "Print and/or Save";
            this.PML_SaveAndPrint_Button.UseVisualStyleBackColor = true;
            this.PML_SaveAndPrint_Button.Click += new System.EventHandler(this.PML_SaveAndPrint_Button_Click);
            // 
            // PML_PrintOnly_RB
            // 
            this.PML_PrintOnly_RB.AutoSize = true;
            this.PML_PrintOnly_RB.Checked = true;
            this.PML_PrintOnly_RB.Location = new System.Drawing.Point(6, 19);
            this.PML_PrintOnly_RB.Name = "PML_PrintOnly_RB";
            this.PML_PrintOnly_RB.Size = new System.Drawing.Size(70, 17);
            this.PML_PrintOnly_RB.TabIndex = 5;
            this.PML_PrintOnly_RB.TabStop = true;
            this.PML_PrintOnly_RB.Text = "Print Only";
            this.PML_PrintOnly_RB.UseVisualStyleBackColor = true;
            this.PML_PrintOnly_RB.CheckedChanged += new System.EventHandler(this.PML_PrintOnly_RB_CheckedChanged);
            // 
            // PML_PrintSave_GB
            // 
            this.PML_PrintSave_GB.Controls.Add(this.PML_SavelOnly_RB);
            this.PML_PrintSave_GB.Controls.Add(this.PML_PrintAndSave_RB);
            this.PML_PrintSave_GB.Controls.Add(this.PML_PrintOnly_RB);
            this.PML_PrintSave_GB.Location = new System.Drawing.Point(243, 69);
            this.PML_PrintSave_GB.Name = "PML_PrintSave_GB";
            this.PML_PrintSave_GB.Size = new System.Drawing.Size(115, 77);
            this.PML_PrintSave_GB.TabIndex = 8;
            this.PML_PrintSave_GB.TabStop = false;
            this.PML_PrintSave_GB.Text = "Printing and Saving";
            // 
            // PML_SavelOnly_RB
            // 
            this.PML_SavelOnly_RB.AutoSize = true;
            this.PML_SavelOnly_RB.Location = new System.Drawing.Point(6, 51);
            this.PML_SavelOnly_RB.Name = "PML_SavelOnly_RB";
            this.PML_SavelOnly_RB.Size = new System.Drawing.Size(74, 17);
            this.PML_SavelOnly_RB.TabIndex = 7;
            this.PML_SavelOnly_RB.TabStop = true;
            this.PML_SavelOnly_RB.Text = "Save Only";
            this.PML_SavelOnly_RB.UseVisualStyleBackColor = true;
            this.PML_SavelOnly_RB.CheckedChanged += new System.EventHandler(this.PML_SavelOnly_RB_CheckedChanged);
            // 
            // PML_PrintAndSave_RB
            // 
            this.PML_PrintAndSave_RB.AutoSize = true;
            this.PML_PrintAndSave_RB.Location = new System.Drawing.Point(6, 35);
            this.PML_PrintAndSave_RB.Name = "PML_PrintAndSave_RB";
            this.PML_PrintAndSave_RB.Size = new System.Drawing.Size(95, 17);
            this.PML_PrintAndSave_RB.TabIndex = 6;
            this.PML_PrintAndSave_RB.TabStop = true;
            this.PML_PrintAndSave_RB.Text = "Print and Save";
            this.PML_PrintAndSave_RB.UseVisualStyleBackColor = true;
            this.PML_PrintAndSave_RB.CheckedChanged += new System.EventHandler(this.PML_PrintAndSave_RB_CheckedChanged);
            // 
            // Form_PrintMailboxLists
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 210);
            this.Controls.Add(this.PML_PrintSave_GB);
            this.Controls.Add(this.PML_SaveAndPrint_Button);
            this.Controls.Add(this.AddDateUnderAddress_CB);
            this.Controls.Add(this.AddDateToFileName_CB);
            this.Controls.Add(this.SelectBuilding2Print_listBox);
            this.Name = "Form_PrintMailboxLists";
            this.Text = "Print Mailbox List(s)";
            this.Load += new System.EventHandler(this.PrintMailboxLists_Form_Load);
            this.PML_PrintSave_GB.ResumeLayout(false);
            this.PML_PrintSave_GB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox SelectBuilding2Print_listBox;
        private System.Windows.Forms.CheckBox AddDateToFileName_CB;
        private System.Windows.Forms.CheckBox AddDateUnderAddress_CB;
        private System.Windows.Forms.Button PML_SaveAndPrint_Button;
        private System.Windows.Forms.RadioButton PML_PrintOnly_RB;
        private System.Windows.Forms.GroupBox PML_PrintSave_GB;
        private System.Windows.Forms.RadioButton PML_SavelOnly_RB;
        private System.Windows.Forms.RadioButton PML_PrintAndSave_RB;
    }
}