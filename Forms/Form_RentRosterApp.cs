using System;
using System.Drawing;
using System.Windows.Forms;

namespace RentRosterAutomation
{
    public partial class Form_RentRosterApp : Form
    {
        private CExcelInteropMethods excelInteropMethods;

        public CUserPreferences MyPersonalPreferences { get; set; }

        public Form_RentRosterApp()
        {
            excelInteropMethods = Program.excelInteropMethods;
            MyPersonalPreferences = Program.preferences;
            InitializeComponent();
        }

        private void Form_RentRosterApp_Load(object sender, EventArgs e)
        {
            if (excelInteropMethods.Complex == null)
            {
                PrintMailboxLists_Button.Enabled = false;
                AddNewResident_Button.Enabled = false;
                DeleteRenter_Button.Enabled = false;
            }
            RR_Quit_BTN.BackColor = Color.Red;
        }

        private void PrintMailboxLists_Button_Click(object sender, EventArgs e)
        {
            Form_PrintMailboxLists printMailboxLists_dialog = new Form_PrintMailboxLists();
            printMailboxLists_dialog.Show();
        }

        private void AddNewResident_Button_Click(object sender, EventArgs e)
        {
            Form_ApartmentNumberVerifier verifier_Form = new Form_ApartmentNumberVerifier();
            verifier_Form.NextAction = Form_ApartmentNumberVerifier.NextActionEnum.ADD;
            verifier_Form.Show();
        }

        private void DeleteRenter_Button_Click(object sender, EventArgs e)
        {
            Form_ApartmentNumberVerifier verifier_Form = new Form_ApartmentNumberVerifier();
            verifier_Form.NextAction = Form_ApartmentNumberVerifier.NextActionEnum.DELETE;
            verifier_Form.Show();
        }

        private void EditPreferences_BTN_Click(object sender, EventArgs e)
        {
            Form_EditPreferences preferences_dlg = new Form_EditPreferences();
            preferences_dlg.Show();
        }

        private void RR_Quit_BTN_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RR_SAVEEDITS_BTN_Click(object sender, EventArgs e)
        {
            CExcelInteropMethods excelInteropMethods = Program.excelInteropMethods;
            if (excelInteropMethods.HaveEditsToSave)
            {
                excelInteropMethods.SaveEditsCloseWorkbookExitExcel();
            }
        }
    }
}
