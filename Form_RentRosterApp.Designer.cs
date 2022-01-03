
namespace RentRosterAutomation
{
    partial class Form_RentRosterApp
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
            this.PrintMailboxLists_Button = new System.Windows.Forms.Button();
            this.AddNewResident_Button = new System.Windows.Forms.Button();
            this.DeleteRenter_Button = new System.Windows.Forms.Button();
            this.EditPreferences_BTN = new System.Windows.Forms.Button();
            this.RR_Quit_BTN = new System.Windows.Forms.Button();
            this.RR_SAVEEDITS_BTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PrintMailboxLists_Button
            // 
            this.PrintMailboxLists_Button.Location = new System.Drawing.Point(35, 25);
            this.PrintMailboxLists_Button.Name = "PrintMailboxLists_Button";
            this.PrintMailboxLists_Button.Size = new System.Drawing.Size(134, 23);
            this.PrintMailboxLists_Button.TabIndex = 0;
            this.PrintMailboxLists_Button.Text = "Print Mailbox List(s)";
            this.PrintMailboxLists_Button.UseVisualStyleBackColor = true;
            this.PrintMailboxLists_Button.Click += new System.EventHandler(this.PrintMailboxLists_Button_Click);
            // 
            // AddNewResident_Button
            // 
            this.AddNewResident_Button.Location = new System.Drawing.Point(35, 54);
            this.AddNewResident_Button.Name = "AddNewResident_Button";
            this.AddNewResident_Button.Size = new System.Drawing.Size(134, 23);
            this.AddNewResident_Button.TabIndex = 1;
            this.AddNewResident_Button.Text = "Add or Edit Tenant";
            this.AddNewResident_Button.UseVisualStyleBackColor = true;
            this.AddNewResident_Button.Click += new System.EventHandler(this.AddNewResident_Button_Click);
            // 
            // DeleteRenter_Button
            // 
            this.DeleteRenter_Button.Location = new System.Drawing.Point(35, 83);
            this.DeleteRenter_Button.Name = "DeleteRenter_Button";
            this.DeleteRenter_Button.Size = new System.Drawing.Size(134, 23);
            this.DeleteRenter_Button.TabIndex = 2;
            this.DeleteRenter_Button.Text = "Delete Tenant";
            this.DeleteRenter_Button.UseVisualStyleBackColor = true;
            this.DeleteRenter_Button.Click += new System.EventHandler(this.DeleteRenter_Button_Click);
            // 
            // EditPreferences_BTN
            // 
            this.EditPreferences_BTN.Location = new System.Drawing.Point(35, 141);
            this.EditPreferences_BTN.Name = "EditPreferences_BTN";
            this.EditPreferences_BTN.Size = new System.Drawing.Size(134, 23);
            this.EditPreferences_BTN.TabIndex = 4;
            this.EditPreferences_BTN.Text = "Edit Preferences";
            this.EditPreferences_BTN.UseVisualStyleBackColor = true;
            this.EditPreferences_BTN.Click += new System.EventHandler(this.EditPreferences_BTN_Click);
            // 
            // RR_Quit_BTN
            // 
            this.RR_Quit_BTN.Location = new System.Drawing.Point(35, 170);
            this.RR_Quit_BTN.Name = "RR_Quit_BTN";
            this.RR_Quit_BTN.Size = new System.Drawing.Size(134, 23);
            this.RR_Quit_BTN.TabIndex = 5;
            this.RR_Quit_BTN.Text = "Quit";
            this.RR_Quit_BTN.UseVisualStyleBackColor = true;
            this.RR_Quit_BTN.Click += new System.EventHandler(this.RR_Quit_BTN_Click);
            // 
            // RR_SAVEEDITS_BTN
            // 
            this.RR_SAVEEDITS_BTN.Location = new System.Drawing.Point(35, 112);
            this.RR_SAVEEDITS_BTN.Name = "RR_SAVEEDITS_BTN";
            this.RR_SAVEEDITS_BTN.Size = new System.Drawing.Size(134, 23);
            this.RR_SAVEEDITS_BTN.TabIndex = 3;
            this.RR_SAVEEDITS_BTN.Text = "Save Changes";
            this.RR_SAVEEDITS_BTN.UseVisualStyleBackColor = true;
            this.RR_SAVEEDITS_BTN.Click += new System.EventHandler(this.RR_SAVEEDITS_BTN_Click);
            // 
            // Form_RentRosterApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 221);
            this.Controls.Add(this.RR_SAVEEDITS_BTN);
            this.Controls.Add(this.RR_Quit_BTN);
            this.Controls.Add(this.EditPreferences_BTN);
            this.Controls.Add(this.DeleteRenter_Button);
            this.Controls.Add(this.AddNewResident_Button);
            this.Controls.Add(this.PrintMailboxLists_Button);
            this.Name = "Form_RentRosterApp";
            this.Text = "Anza Victoria Rent Roster Control Panel";
            this.Load += new System.EventHandler(this.Form_RentRosterApp_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button PrintMailboxLists_Button;
        private System.Windows.Forms.Button AddNewResident_Button;
        private System.Windows.Forms.Button DeleteRenter_Button;
        private System.Windows.Forms.Button EditPreferences_BTN;
        private System.Windows.Forms.Button RR_Quit_BTN;
        private System.Windows.Forms.Button RR_SAVEEDITS_BTN;
    }
}

