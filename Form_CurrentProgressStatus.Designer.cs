
namespace RentRosterAutomation
{
    partial class Form_CurrentProgressStatus
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
            this.CPS_Message_TB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // CPS_Message_TB
            // 
            this.CPS_Message_TB.Location = new System.Drawing.Point(40, 40);
            this.CPS_Message_TB.Multiline = true;
            this.CPS_Message_TB.Name = "CPS_Message_TB";
            this.CPS_Message_TB.ReadOnly = true;
            this.CPS_Message_TB.Size = new System.Drawing.Size(531, 255);
            this.CPS_Message_TB.TabIndex = 0;
            // 
            // Form_CurrentProgressStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 349);
            this.Controls.Add(this.CPS_Message_TB);
            this.Name = "Form_CurrentProgressStatus";
            this.Text = "Please Wait";
            this.Load += new System.EventHandler(this.Form_CurrentProgressStatus_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox CPS_Message_TB;
    }
}