using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TenantRosterAutomation
{
    public partial class PrintMailboxListsDlg : Form
    {
        private PrintSavePreference.PrintSave printSave;
        private bool addDateToFileName = false;
        private bool addDateToTitle = false;
        private string selectedBuildings;
        private MSWordInterface wordInteropMethods;

        public PrintMailboxListsDlg()
        {
            InitializeComponent();
            wordInteropMethods = new MSWordInterface(Globals.Preferences);
        }

        private void PrintMailboxLists_Form_Load(object sender, EventArgs e)
        {
            List<string> buildings = Globals.Complex.BuildingAddressList;

            foreach (string building in buildings)
            {
                SelectBuilding2Print_listBox.Items.Add(building);
            }
            SelectBuilding2Print_listBox.Items.Add("All Buildings");

            if (Globals.Preferences.HavePreferenceData)
            {
                printSave = Globals.Preferences.PrintSaveOptions;
                PrintSaveChange();
            }

            AddDateToFileName_CB.Checked = addDateToFileName;
            AddDateUnderAddress_CB.Checked = addDateToTitle;
            PML_SaveAndPrint_Button.Enabled = false;
            PML_Cancel_BTN.BackColor = Color.Red;
        }

        private void AddDateToFileName_CB_CheckedChanged(object sender, EventArgs e)
        {
            addDateToFileName = !addDateToFileName;
        }

        private void AddDateUnderAddress_CB_CheckedChanged(object sender, EventArgs e)
        {
            addDateToTitle = !addDateToTitle;
        }

        private void PML_SaveAndPrint_Button_Click(object sender, EventArgs e)
        {
            if (String.Compare(selectedBuildings, "All Buildings") == 0)
            {
                List<int> StreetNumbers = Globals.Complex.StreetNumbers;

                foreach (int streetNumber in StreetNumbers)
                {
                    PrintAndOrSaveMailList(streetNumber);
                }
            }
            else
            {
                string streetAddress = selectedBuildings.Substring(0, 5);
                printAndOrSaveMailList(streetAddress);
            }

            Close();
        }

        private void printAndOrSaveMailList(string streetAddress)
        {
            int iStreetNumber = 0;
            if (Int32.TryParse(streetAddress, out iStreetNumber))
            {
                PrintAndOrSaveMailList(iStreetNumber);
            }
            else
            {
                MessageBox.Show("Non Numeric string passed into PrintMailboxLists_Form::printAndOrSaveMailList().");
            }
        }

        private void PML_PrintOnly_RB_CheckedChanged(object sender, EventArgs e)
        {
            printSave = PrintSavePreference.PrintSave.PrintOnly;
        }

        private void PML_SavelOnly_RB_CheckedChanged(object sender, EventArgs e)
        {
            printSave = PrintSavePreference.PrintSave.SaveOnly;
        }

        private void PML_PrintAndSave_RB_CheckedChanged(object sender, EventArgs e)
        {
            printSave = PrintSavePreference.PrintSave.PrintAndSave;
        }

        private void PrintSaveChange()
        {
            switch (printSave)
            {
                case PrintSavePreference.PrintSave.PrintAndSave:
                    PML_PrintAndSave_RB.Checked = true;
                    break;

                case PrintSavePreference.PrintSave.SaveOnly:
                    PML_SavelOnly_RB.Checked = true;
                    break;

                default:
                    PML_PrintOnly_RB.Checked = true;
                    break;
            }
        }

        private void SelectBuilding2Print_listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectBuilding2Print_listBox.SelectedItem != null)
            {
                selectedBuildings = SelectBuilding2Print_listBox.SelectedItem.ToString();
                PML_SaveAndPrint_Button.Enabled = true;
            }
        }

        private void PrintAndOrSaveMailList(int streetAddress)
        {
            bool save = ((printSave == PrintSavePreference.PrintSave.PrintAndSave) ? true :
                (printSave == PrintSavePreference.PrintSave.SaveOnly) ? true : false);
            bool print = ((printSave == PrintSavePreference.PrintSave.PrintAndSave) ? true :
                (printSave == PrintSavePreference.PrintSave.PrintOnly) ? true : false);

            string documentName = "MailboxList_" + streetAddress;

            string statusMessage = (print && save) ? "Printing and Saving " :
                (print) ? "Printing " : "Saving ";
            statusMessage += "the mailbox list for " + streetAddress;

            ReportCurrentStatusWindow psStatus = new ReportCurrentStatusWindow();
            psStatus.MessageText = statusMessage;
            psStatus.Show();

            Building building = Globals.Complex.GetBuilding(streetAddress);
            if (building != null)
            {
                MailboxData mailboxList = Globals.Complex.GetMailBoxList(building);
                if (mailboxList != null)
                {
                    try
                    {
                        wordInteropMethods.CreateMailistPrintAndOrSave(documentName,
                            mailboxList, addDateToFileName, addDateToTitle, save, print);
                    }
                    catch (Exception e)
                    {
                        psStatus.Close();
                        string eMsg = "An error occurred while generating the Word Document for "
                            + documentName + " : " + e.Message;
                        WordException wordException = new WordException(eMsg, e);
                        throw wordException;
                    }
                }
            }

            psStatus.Close();
        }

        private void PML_Cancel_BTN_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
