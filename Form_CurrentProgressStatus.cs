using System;
using System.Drawing;
using System.Windows.Forms;

namespace RentRosterAutomation
{
    public partial class Form_CurrentProgressStatus : Form
    {
        public string MessageText { get; set; }
        public Form_CurrentProgressStatus()
        {
            InitializeComponent();
        }

        private void Form_CurrentProgressStatus_Load(object sender, EventArgs e)
        {
            CPS_Message_TB.Text = MessageText;
            CPS_Message_TB.Font = new Font("Arial", 12, FontStyle.Bold);
            Size size = TextRenderer.MeasureText(CPS_Message_TB.Text, CPS_Message_TB.Font);
            Height = (size.Height * 3) + 84;
            Width = size.Width + 84;
            CPS_Message_TB.Width = size.Width;
            CPS_Message_TB.Height = size.Height * 3;
            CPS_Message_TB.Left = 40;
            CPS_Message_TB.Height = 40;
            //            AutoSize = true;
            //            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            //            CPS_Message_TB.Left = (ClientSize.Width - CPS_Message_TB.Width) / 2;
            //            CPS_Message_TB.Top = (ClientSize.Height - CPS_Message_TB.Height) / 2;
        }
    }
}
