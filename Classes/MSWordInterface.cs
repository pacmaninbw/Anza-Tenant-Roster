﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;

namespace TenantRosterAutomation
{
    // Each building in the complex contains 3 floors of apartments. The documnets
    // generated by Microsoft Word have 3 columns of data, one for each floor.
    // Prints and or saves a Microsoft Word Document for each building.
    class MSWordInterface
    {
        private string defaultSaveFolder;
        private object oMissing = Missing.Value;
        private object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

        public MSWordInterface(UserPreferences inPreferences)
        {
            defaultSaveFolder = inPreferences.DefaultSaveDirectory;
        }

        public bool CreateMailistPrintAndOrSave(string documentName, MailboxData mailboxdata,
                bool addDateToDocName, bool addDateToTitle, bool save, bool print
            )
        {
            bool docGenerated = true;
            if (string.IsNullOrEmpty(documentName))
            {
                return false;
            }

            string fullFilePathName = FullFilePath(documentName, addDateToDocName);

            Word.Application wordApp = new Word.Application();

            try
            {
                Word.Document wordDoc = new Word.Document();
                wordApp.Visible = false;
                wordDoc = wordApp.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                FormatDocMargins(ref wordDoc, wordApp);
                AddTitleToMailBoxList(ref wordDoc, mailboxdata.AddressStreetNumber, addDateToTitle);
                AddTenantTableToMailBoxList(ref wordDoc, mailboxdata, wordApp);

                object DoNotSaveChanges = PrintAndOrSave(wordDoc, save, print, fullFilePathName);
                wordDoc.Close(ref DoNotSaveChanges, ref oMissing, ref oMissing);
            }
            catch (Exception e)
            {
                string eMsg = "An error occurred while generating the Word Document for "
                    + documentName + " : " + e.Message;
                docGenerated = false;
                MessageBox.Show(eMsg);
            }
            wordApp.Quit();

            return docGenerated;
        }

        private object PrintAndOrSave(Word.Document wordDoc, bool save, bool print, string fullFilePathName)
        {
            object DoNotSaveChanges = oMissing;
            if (print)
            {
                wordDoc.PrintOut();
            }

            if (save)
            {
                SaveFile(wordDoc, fullFilePathName);
            }
            else
            {
                DoNotSaveChanges = Word.WdSaveOptions.wdDoNotSaveChanges;
            }
            
            return DoNotSaveChanges;
        }

        private void AddTitleToMailBoxList(ref Word.Document wordDoc, int addressNumber, bool addDateToTitle)
        {
            object oMissing = Missing.Value;

            Word.Paragraph MailboxListTitle = wordDoc.Content.Paragraphs.Add(ref oMissing);
            MailboxListTitle.Range.Text = addressNumber.ToString();
            MailboxListTitle.Range.Font.Size = 78;
            MailboxListTitle.Range.Font.Bold = 1;
            MailboxListTitle.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            MailboxListTitle.SpaceAfter = 0;
            MailboxListTitle.Range.InsertParagraphAfter();
            if (addDateToTitle)
            {
                DateTime todaysDate = DateTime.Today;
                Word.Paragraph todaysDatePara = wordDoc.Content.Paragraphs.Add(ref oMissing);
                todaysDatePara.Range.Text = todaysDate.ToString("MM/dd/yyyy");
                todaysDatePara.Range.Font.Size = 7;
                todaysDatePara.Range.Bold = 0;
                todaysDatePara.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                todaysDatePara.SpaceAfter = 5;
                todaysDatePara.Range.InsertParagraphAfter();
            }
        }

        // Table is 2 columns for first and second floors, 1 column
        // merged for third floor
        private void AddTenantTableToMailBoxList(ref Word.Document wordDoc,
            MailboxData mailboxData, Word.Application wordApp)
        {
            object oMissing = Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            int rowCount = (mailboxData.FirstFloor.Count > mailboxData.SecondFloor.Count) ?
                mailboxData.FirstFloor.Count : mailboxData.SecondFloor.Count;
            int maxDoubleColumn = rowCount;
            rowCount += mailboxData.ThirdFloor.Count;
            rowCount += 1; // One empty row between third floor column and upper colums

            Word.Table tenantTable = CreateAndFormatTenatTable(rowCount, maxDoubleColumn,
                wordDoc, wordApp);

            FillFloor1Stand2Nd(mailboxData, maxDoubleColumn, ref tenantTable);
            FillThirdFloor(mailboxData, maxDoubleColumn, wordApp, ref tenantTable);
        }

        private void FillThirdFloor(MailboxData mailboxData, int maxDoubleColumn, Word.Application wordApp, ref Word.Table tenantTable)
        {
            int row = maxDoubleColumn + 2;
            List<Apartment> ThirdFloor = mailboxData.ThirdFloor;
            for (int i = 0; i < ThirdFloor.Count; i++)
            {
                Word.Cell currentCell = tenantTable.Cell(row, 1);
                tenantTable.Rows[row].Cells[1].Merge(tenantTable.Rows[row].Cells[2]);
                currentCell.LeftPadding = wordApp.InchesToPoints((float)2.5);
                AssignCellValueAndFormat(ref currentCell, MailBoxListEntry(ThirdFloor, i));
                row++;
            }
        }

        private void FillFloor1Stand2Nd(MailboxData mailboxData, int maxDoubleColumn, ref Word.Table tenantTable)
        {
            List<Apartment> FirstFloor = mailboxData.FirstFloor;
            List<Apartment> SecondFloor = mailboxData.SecondFloor;

            int firstFloorCount = FirstFloor.Count;
            int secondFloorCount = SecondFloor.Count;

            int row = 1;
            for (; row <= maxDoubleColumn; row++)
            {
                if (row <= firstFloorCount)
                {
                    Word.Cell currentCell = tenantTable.Cell(row, 1);
                    AssignCellValueAndFormat(ref currentCell, MailBoxListEntry(FirstFloor, row - 1));
                }
                if (row <= secondFloorCount)
                {
                    Word.Cell currentCell = tenantTable.Cell(row, 2);
                    AssignCellValueAndFormat(ref currentCell, MailBoxListEntry(SecondFloor, row - 1));
                }
            }

        }

        private Word.Table CreateAndFormatTenatTable(int rowCount, int maxDoubleColumn,
            Word.Document wordDoc, Word.Application wordApp)
        {
            Word.Table tenantTable;
            Word.Range wrdRng = wordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

            tenantTable = wordDoc.Tables.Add(wrdRng, rowCount, 2, ref oMissing, ref oMissing);
            tenantTable.Range.Font.Size = (maxDoubleColumn < 14) ? 18 : 16;
            tenantTable.Range.Font.Bold = 1;
            tenantTable.Range.Font.Name = "Arial";
            tenantTable.Rows.Alignment = Word.WdRowAlignment.wdAlignRowLeft;
            tenantTable.Columns.SetWidth(wordApp.InchesToPoints((float)3.25), Word.WdRulerStyle.wdAdjustSameWidth);

            return tenantTable;
        }

        private void AssignCellValueAndFormat(ref Word.Cell currentCell, string cellValue)
        {
            currentCell.Range.Text = cellValue;
            currentCell.WordWrap = true;
            currentCell.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
        }

        private string MailBoxListEntry(List<Apartment> apartments, int row)
        {
            string mbListEntry = apartments[row].ApartmentNumber.ToString();
            mbListEntry += "  " + apartments[row].Tenant.MailboxListOccupantEntry();

            return mbListEntry;
        }

        private string FullFilePath(string documentName, bool addDateToFileName)
        {
            string fileToSave = defaultSaveFolder + "\\" + documentName;

            if (addDateToFileName)
            {
                DateTime todaysDate = DateTime.Today;
                string dtNoSlash = todaysDate.ToString("MMddyyyy");
                fileToSave += "_" + dtNoSlash;
            }
            fileToSave += ".docx";

            return fileToSave;
        }

        private void SaveFile(Word.Document wordDoc, string documentName)
        {
            if (!string.IsNullOrEmpty(defaultSaveFolder))
            {
                object oMissing = Missing.Value;
                object newfile = documentName;
                wordDoc.SaveAs(ref newfile, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing);
            }
            else
            {
                wordDoc.Save();
            }
        }

        private void FormatDocMargins(ref Word.Document wordDoc, Word.Application wordApp)
        {
            wordDoc.PageSetup.TopMargin = wordApp.InchesToPoints((float)0.375);
            wordDoc.PageSetup.LeftMargin = wordApp.InchesToPoints((float)1);
            wordDoc.PageSetup.RightMargin = wordApp.InchesToPoints((float)1);
            wordDoc.PageSetup.BottomMargin = wordApp.InchesToPoints((float)0.375);
        }
    }
}
