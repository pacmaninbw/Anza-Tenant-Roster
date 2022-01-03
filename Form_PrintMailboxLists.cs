﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RentRosterAutomation
{
    public partial class Form_PrintMailboxLists : Form
    {
        private readonly CUserPreferences preferences;
        private CPrintSavePreference.PrintSave printSave;
        private bool addDateToFileName = false;
        private bool addDateToTitle = false;
        private string selectedBuildings;
        private CPropertyComplex propertyComplex;
        private CWordInteropMethods wordInteropMethods;

        public Form_PrintMailboxLists()
        {
            InitializeComponent();
            preferences = Program.preferences;
            propertyComplex = Program.excelInteropMethods.Complex;
            wordInteropMethods = new CWordInteropMethods(preferences);
        }

        private void PrintMailboxLists_Form_Load(object sender, EventArgs e)
        {
            List<string> buildings = propertyComplex.BuildingAddressList;

            foreach (string building in buildings)
            {
                SelectBuilding2Print_listBox.Items.Add(building);
            }
            SelectBuilding2Print_listBox.Items.Add("All Buildings");

            if (preferences.HavePreferenceData)
            {
                printSave = preferences.PrintSaveOptions;
                PrintSaveChange();
            }

            AddDateToFileName_CB.Checked = addDateToFileName;
            AddDateUnderAddress_CB.Checked = addDateToTitle;
            PML_SaveAndPrint_Button.Enabled = false;
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
                List<int> StreetNumbers = propertyComplex.StreetNumbers;

                foreach (int streetNumber in StreetNumbers)
                {
                    printAndOrSaveMailList(streetNumber);
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
                printAndOrSaveMailList(iStreetNumber);
            }
            else
            {
                MessageBox.Show("Non Numeric string passed into PrintMailboxLists_Form::printAndOrSaveMailList().");
            }
        }

        private void PML_PrintOnly_RB_CheckedChanged(object sender, EventArgs e)
        {
            printSave = CPrintSavePreference.PrintSave.PrintOnly;
        }

        private void PML_SavelOnly_RB_CheckedChanged(object sender, EventArgs e)
        {
            printSave = CPrintSavePreference.PrintSave.SaveOnly;
        }

        private void PML_PrintAndSave_RB_CheckedChanged(object sender, EventArgs e)
        {
            printSave = CPrintSavePreference.PrintSave.PrintAndSave;
        }

        private void PrintSaveChange()
        {
            switch (printSave)
            {
                case CPrintSavePreference.PrintSave.PrintAndSave:
                    PML_PrintAndSave_RB.Checked = true;
                    break;

                case CPrintSavePreference.PrintSave.SaveOnly:
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

        private void printAndOrSaveMailList(int streetAddress)
        {
            bool save = ((printSave == CPrintSavePreference.PrintSave.PrintAndSave) ? true :
                (printSave == CPrintSavePreference.PrintSave.SaveOnly) ? true : false);
            bool print = ((printSave == CPrintSavePreference.PrintSave.PrintAndSave) ? true :
                (printSave == CPrintSavePreference.PrintSave.PrintOnly) ? true : false);

            string documentName = "MailboxList_" + streetAddress;

            string statusMessage = (print && save) ? "Printing and Saving " :
                (print) ? "Printing " : "Saving ";
            statusMessage += "the mailbox list for " + streetAddress;

            Form_CurrentProgressStatus psStatus = new Form_CurrentProgressStatus();
            psStatus.MessageText = statusMessage;
            psStatus.Show();

            CBuilding building = propertyComplex.GetBuilding(streetAddress);
            if (building != null)
            {
                CMailboxListData mailboxList = Program.excelInteropMethods.GetMailboxData(building);
                if (mailboxList != null)
                {
                    wordInteropMethods.CreateMailistPrintAndOrSave(documentName,
                        mailboxList, addDateToFileName, addDateToTitle, save, print);
                }
            }

            psStatus.Close();
        }

    }
}