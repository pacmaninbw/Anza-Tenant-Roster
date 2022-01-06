using System;
using System.Drawing;
using System.Windows.Forms;

namespace TenantRosterAutomation
{
    public partial class ReportCurrentStatusWindow : Form
    {
        public string MessageText { get; set; }
        public ReportCurrentStatusWindow()
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
        }
    }
}
