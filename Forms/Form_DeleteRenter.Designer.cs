
namespace RentRosterAutomation
{
    partial class Form_DeleteRenter
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
            this.DR_AptNumber_LAB = new System.Windows.Forms.Label();
            this.DR_Renter2Delete_LAB = new System.Windows.Forms.Label();
            this.DR_TenantName_TB = new System.Windows.Forms.TextBox();
            this.DR_RenterToDeleteYN_GB = new System.Windows.Forms.GroupBox();
            this.DR_Renter2DeleteNo_RB = new System.Windows.Forms.RadioButton();
            this.DR_Renter2DeleteYes_RB = new System.Windows.Forms.RadioButton();
            this.DR_DeleteRenter_BTN = new System.Windows.Forms.Button();
            this.DR_Cancel_BTN = new System.Windows.Forms.Button();
            this.DR_RenterToDeleteYN_GB.SuspendLayout();
            this.SuspendLayout();
            // 
            // DR_AptNumber_LAB
            // 
            this.DR_AptNumber_LAB.AutoSize = true;
            this.DR_AptNumber_LAB.Location = new System.Drawing.Point(40, 20);
            this.DR_AptNumber_LAB.Name = "DR_AptNumber_LAB";
            this.DR_AptNumber_LAB.Size = new System.Drawing.Size(95, 13);
            this.DR_AptNumber_LAB.TabIndex = 0;
            this.DR_AptNumber_LAB.Text = "Apartment Number";
            // 
            // DR_Renter2Delete_LAB
            // 
            this.DR_Renter2Delete_LAB.AutoSize = true;
            this.DR_Renter2Delete_LAB.Location = new System.Drawing.Point(40, 43);
            this.DR_Renter2Delete_LAB.Name = "DR_Renter2Delete_LAB";
            this.DR_Renter2Delete_LAB.Size = new System.Drawing.Size(135, 13);
            this.DR_Renter2Delete_LAB.TabIndex = 2;
            this.DR_Renter2Delete_LAB.Text = "Is this the tenant to delete?";
            // 
            // DR_TenantName_TB
            // 
            this.DR_TenantName_TB.Location = new System.Drawing.Point(181, 43);
            this.DR_TenantName_TB.Name = "DR_TenantName_TB";
            this.DR_TenantName_TB.Size = new System.Drawing.Size(214, 20);
            this.DR_TenantName_TB.TabIndex = 3;
            // 
            // DR_RenterToDeleteYN_GB
            // 
            this.DR_RenterToDeleteYN_GB.Controls.Add(this.DR_Renter2DeleteNo_RB);
            this.DR_RenterToDeleteYN_GB.Controls.Add(this.DR_Renter2DeleteYes_RB);
            this.DR_RenterToDeleteYN_GB.Location = new System.Drawing.Point(50, 60);
            this.DR_RenterToDeleteYN_GB.Name = "DR_RenterToDeleteYN_GB";
            this.DR_RenterToDeleteYN_GB.Size = new System.Drawing.Size(67, 58);
            this.DR_RenterToDeleteYN_GB.TabIndex = 4;
            this.DR_RenterToDeleteYN_GB.TabStop = false;
            // 
            // DR_Renter2DeleteNo_RB
            // 
            this.DR_Renter2DeleteNo_RB.AutoSize = true;
            this.DR_Renter2DeleteNo_RB.Location = new System.Drawing.Point(6, 30);
            this.DR_Renter2DeleteNo_RB.Name = "DR_Renter2DeleteNo_RB";
            this.DR_Renter2DeleteNo_RB.Size = new System.Drawing.Size(39, 17);
            this.DR_Renter2DeleteNo_RB.TabIndex = 1;
            this.DR_Renter2DeleteNo_RB.TabStop = true;
            this.DR_Renter2DeleteNo_RB.Text = "No";
            this.DR_Renter2DeleteNo_RB.UseVisualStyleBackColor = true;
            this.DR_Renter2DeleteNo_RB.CheckedChanged += new System.EventHandler(this.DR_Renter2DeleteNo_RB_CheckedChanged);
            // 
            // DR_Renter2DeleteYes_RB
            // 
            this.DR_Renter2DeleteYes_RB.AutoSize = true;
            this.DR_Renter2DeleteYes_RB.Location = new System.Drawing.Point(6, 10);
            this.DR_Renter2DeleteYes_RB.Name = "DR_Renter2DeleteYes_RB";
            this.DR_Renter2DeleteYes_RB.Size = new System.Drawing.Size(43, 17);
            this.DR_Renter2DeleteYes_RB.TabIndex = 0;
            this.DR_Renter2DeleteYes_RB.TabStop = true;
            this.DR_Renter2DeleteYes_RB.Text = "Yes";
            this.DR_Renter2DeleteYes_RB.UseVisualStyleBackColor = true;
            this.DR_Renter2DeleteYes_RB.CheckedChanged += new System.EventHandler(this.DR_Renter2DeleteYes_RB_CheckedChanged);
            // 
            // DR_DeleteRenter_BTN
            // 
            this.DR_DeleteRenter_BTN.Location = new System.Drawing.Point(40, 118);
            this.DR_DeleteRenter_BTN.Name = "DR_DeleteRenter_BTN";
            this.DR_DeleteRenter_BTN.Size = new System.Drawing.Size(75, 23);
            this.DR_DeleteRenter_BTN.TabIndex = 5;
            this.DR_DeleteRenter_BTN.Text = "Delete";
            this.DR_DeleteRenter_BTN.UseVisualStyleBackColor = true;
            this.DR_DeleteRenter_BTN.Click += new System.EventHandler(this.DR_DeleteRenter_BTN_Click);
            // 
            // DR_Cancel_BTN
            // 
            this.DR_Cancel_BTN.Location = new System.Drawing.Point(145, 118);
            this.DR_Cancel_BTN.Name = "DR_Cancel_BTN";
            this.DR_Cancel_BTN.Size = new System.Drawing.Size(75, 23);
            this.DR_Cancel_BTN.TabIndex = 6;
            this.DR_Cancel_BTN.Text = "Cancel";
            this.DR_Cancel_BTN.UseVisualStyleBackColor = true;
            this.DR_Cancel_BTN.Click += new System.EventHandler(this.DR_Cancel_BTN_Click);
            // 
            // Form_DeleteRenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 160);
            this.Controls.Add(this.DR_Cancel_BTN);
            this.Controls.Add(this.DR_DeleteRenter_BTN);
            this.Controls.Add(this.DR_RenterToDeleteYN_GB);
            this.Controls.Add(this.DR_TenantName_TB);
            this.Controls.Add(this.DR_Renter2Delete_LAB);
            this.Controls.Add(this.DR_AptNumber_LAB);
            this.Name = "Form_DeleteRenter";
            this.Text = "Delete Renter";
            this.Load += new System.EventHandler(this.DeleteRenter_Load);
            this.DR_RenterToDeleteYN_GB.ResumeLayout(false);
            this.DR_RenterToDeleteYN_GB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DR_AptNumber_LAB;
        private System.Windows.Forms.Label DR_Renter2Delete_LAB;
        private System.Windows.Forms.TextBox DR_TenantName_TB;
        private System.Windows.Forms.GroupBox DR_RenterToDeleteYN_GB;
        private System.Windows.Forms.RadioButton DR_Renter2DeleteNo_RB;
        private System.Windows.Forms.RadioButton DR_Renter2DeleteYes_RB;
        private System.Windows.Forms.Button DR_DeleteRenter_BTN;
        private System.Windows.Forms.Button DR_Cancel_BTN;
    }
}