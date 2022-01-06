
namespace RentRosterAutomation
{
    partial class ApartmentNumberVerifier
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
            this.ANV_FindApartment_BTN = new System.Windows.Forms.Button();
            this.ANV_ApartmentNumber_TB = new System.Windows.Forms.TextBox();
            this.ANV_ApartmentNumber_LAB = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ANV_FindApartment_BTN
            // 
            this.ANV_FindApartment_BTN.Location = new System.Drawing.Point(42, 53);
            this.ANV_FindApartment_BTN.Name = "ANV_FindApartment_BTN";
            this.ANV_FindApartment_BTN.Size = new System.Drawing.Size(100, 23);
            this.ANV_FindApartment_BTN.TabIndex = 2;
            this.ANV_FindApartment_BTN.Text = "Show Apartment";
            this.ANV_FindApartment_BTN.UseVisualStyleBackColor = true;
            this.ANV_FindApartment_BTN.Click += new System.EventHandler(this.APV_FindApartment_BTN_Click);
            // 
            // ANV_ApartmentNumber_TB
            // 
            this.ANV_ApartmentNumber_TB.Location = new System.Drawing.Point(42, 27);
            this.ANV_ApartmentNumber_TB.Name = "ANV_ApartmentNumber_TB";
            this.ANV_ApartmentNumber_TB.Size = new System.Drawing.Size(50, 20);
            this.ANV_ApartmentNumber_TB.TabIndex = 1;
            this.ANV_ApartmentNumber_TB.TextChanged += new System.EventHandler(this.ANV_ApartmentNumber_TB_TextChanged);
            // 
            // ANV_ApartmentNumber_LAB
            // 
            this.ANV_ApartmentNumber_LAB.AutoSize = true;
            this.ANV_ApartmentNumber_LAB.Location = new System.Drawing.Point(32, 6);
            this.ANV_ApartmentNumber_LAB.Name = "ANV_ApartmentNumber_LAB";
            this.ANV_ApartmentNumber_LAB.Size = new System.Drawing.Size(123, 13);
            this.ANV_ApartmentNumber_LAB.TabIndex = 2;
            this.ANV_ApartmentNumber_LAB.Text = "Enter Apartment Number";
            // 
            // Form_ApartmentNumberVerifier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(180, 100);
            this.Controls.Add(this.ANV_ApartmentNumber_LAB);
            this.Controls.Add(this.ANV_ApartmentNumber_TB);
            this.Controls.Add(this.ANV_FindApartment_BTN);
            this.Name = "Form_ApartmentNumberVerifier";
            this.Text = "Apartment Number";
            this.Load += new System.EventHandler(this.ApartmentNumberVerifier_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ANV_FindApartment_BTN;
        private System.Windows.Forms.TextBox ANV_ApartmentNumber_TB;
        private System.Windows.Forms.Label ANV_ApartmentNumber_LAB;
    }
}