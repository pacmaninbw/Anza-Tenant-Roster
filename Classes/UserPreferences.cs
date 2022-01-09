using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace TenantRosterAutomation
{
    // Reads and writes the user preferences file.
    public class UserPreferences
    {
        private PrintSavePreference.PrintSave printSaveValue;
        private PrintSavePreference printSavePreference;
        private Dictionary<int, string> FieldNameByIndex;
        private Dictionary<string, int> IndexFromFieldName;
        private const int fileVersion = 1;
        private bool preferenceFileExists;
        private bool preferenceFileRead;
        private string preferencesFileName;

        public bool HavePreferenceData { get { return preferenceFileRead; } }
        public PrintSavePreference.PrintSave PrintSaveOptions { get { return printSaveValue; } set { printSaveValue = value; } }
        public string ExcelWorkBookFullFileSpec { get; set; }
        public string ExcelWorkSheetName { get; set; }
        public string DefaultSaveDirectory { get; set; }

        public UserPreferences()
        {
            CommonInitialization();
        }

        public UserPreferences(string PreferencesFileName)
        {
            CommonInitialization();
            preferencesFileName = PreferencesFileName;

            if (!string.IsNullOrEmpty(preferencesFileName))
            {
                preferenceFileExists = File.Exists(preferencesFileName);
                if (preferenceFileExists)
                {
                    preferenceFileRead = ReadPreferenceFile(preferencesFileName);
                }
            }
        }

        public bool SavePreferencesToFile(string PreferencesFileName = null)
        {
            if (!string.IsNullOrEmpty(PreferencesFileName))
            {
                preferencesFileName = PreferencesFileName;
            }

            StreamWriter Preferencesfile = new StreamWriter(preferencesFileName);
            try
            {
                WritePreferencesToDisc(Preferencesfile);
                Preferencesfile.Flush();
                Preferencesfile.Close();

                preferenceFileExists = true;
                preferenceFileRead = true;

                return true;
            }
            catch (IOException)
            {
                MessageBox.Show("Unable to write to preferences file: " + preferencesFileName);
                return false;
            }
        }

        public void CopyValues(UserPreferences newPreferences, bool copyPreferenceFileName = false)
        {
            PrintSaveOptions = newPreferences.PrintSaveOptions;
            ExcelWorkBookFullFileSpec = newPreferences.ExcelWorkBookFullFileSpec;
            ExcelWorkSheetName = newPreferences.ExcelWorkSheetName;
            DefaultSaveDirectory = newPreferences.DefaultSaveDirectory;
            if (copyPreferenceFileName)
            {
                preferencesFileName = newPreferences.preferencesFileName;
            }
        }

        private void CommonInitialization()
        {
            preferencesFileName = null;
            preferenceFileExists = false;
            preferenceFileRead = false;
            printSavePreference = new PrintSavePreference();
            InitDictionaries();
            SetValuesToUndefinedState();
        }

        private void InitDictionaries()
        {

            FieldNameByIndex = new Dictionary<int, string>();
            int fieldNameCount = fileValueIds.Length;
            for (int i = 0; i < fieldNameCount; ++i)
            {
                FieldNameByIndex.Add(i, fileValueIds[i]);
            }

            IndexFromFieldName = new Dictionary<string, int>();
            for (int i = 0; i < fieldNameCount; ++i)
            {
                IndexFromFieldName.Add(fileValueIds[i], i);
            }

        }

        private void SetValuesToUndefinedState()
        {
            PrintSaveOptions = PrintSavePreference.PrintSave.PrintOnly;
        }

        // These constants and variables are used when reading and writing
        // the preferences to and from the preference file.
        private const int fileVersionId = 0;
        private const int printSaveOptionId = 1;
        private const int defaultSaveDirId = 2;
        private const int rentRosterFileId = 3;
        private const int rentRosterSheetNameId = 4;
        private string[] fileValueIds =
        {
            "FileVersion:",
            "PrintSaveValue:",
            "DefaultSaveDirectory:",
            "RentRosterFile:",
            "RentRosterSheet:"
        };

        private void WritePreferencesToDisc(StreamWriter preferenceFile)
        {
            preferenceFile.WriteLine(fileValueIds[fileVersionId] + " " + fileVersion.ToString());
            preferenceFile.WriteLine(fileValueIds[printSaveOptionId] + " " +
                printSavePreference.ConvertPrintSaveToString(printSaveValue));
            preferenceFile.WriteLine(fileValueIds[defaultSaveDirId] + " " + DefaultSaveDirectory);
            preferenceFile.WriteLine(fileValueIds[rentRosterFileId] + " " + ExcelWorkBookFullFileSpec);
            preferenceFile.WriteLine(fileValueIds[rentRosterSheetNameId] + " " + ExcelWorkSheetName);

        }

        private void ConvertandTestFileVersion(string fileInput)
        {
            try
            {
                int testFileVersion = Int32.Parse(fileInput);
                if (testFileVersion != fileVersion)
                {
                    if (testFileVersion < fileVersion)
                    {
                        MessageBox.Show("Preference file version " + fileInput + " out of date, please edit preferences to add new field values.");
                    }
                    else
                    {
                        MessageBox.Show("This version of the Tenant Roster tool does not support all the features of the tool that generated the file.");
                    }
                }
            }
            catch (FormatException e)
            {
                string eMsg = "Reading preferences File Version failed: " + e.Message;
                MessageBox.Show(eMsg);
            }
        }

        private bool ReadPreferenceFile(string fileName)
        {
            bool fileReadSucceeded = true;
            string[] lines;

            if (File.Exists(fileName))
            {
                try
                {
                    lines = File.ReadAllLines(fileName);
                    fileReadSucceeded = GetPreferenceValues(lines);
                }
                catch (IOException)
                {
                    fileReadSucceeded = false;
                }
            }

            return fileReadSucceeded;
        }

        private bool GetPreferenceValues(string[] lines)
        {
            bool hasAllFields = false;
            int requiredFieldCount = 0;
            int lineCount = lines.Length;
            for (int i = 0; i < lineCount; ++i)
            {
                string[] nameAndValue = lines[i].Split(' ');
                int fieldIndex;
                IndexFromFieldName.TryGetValue(nameAndValue[0], out fieldIndex);
                switch (fieldIndex)
                {
                    case fileVersionId:
                        ConvertandTestFileVersion(nameAndValue[1]);
                        requiredFieldCount++;
                        break;

                    case printSaveOptionId:
                        printSaveValue = printSavePreference.ConvertStringToPrintSave(nameAndValue[1]);
                        requiredFieldCount++;
                        break;

                    case defaultSaveDirId:
                        DefaultSaveDirectory = CorrectForMuliWordNames(nameAndValue);
                        requiredFieldCount++;
                        break;

                    case rentRosterFileId:
                        ExcelWorkBookFullFileSpec = CorrectForMuliWordNames(nameAndValue);
                        requiredFieldCount++;
                        break;

                    case rentRosterSheetNameId:
                        ExcelWorkSheetName = CorrectForMuliWordNames(nameAndValue);
                        requiredFieldCount++;
                        break;

                    default:
                        MessageBox.Show("Reading preference file: Unknown field identity");
                        return false;
                }
            }

            if (requiredFieldCount == fileValueIds.Length)
            {
                hasAllFields = true;
                if (string.IsNullOrEmpty(ExcelWorkSheetName))
                {
                    hasAllFields = false;
                }
            }

            return hasAllFields;
        }

        private string CorrectForMuliWordNames(string[] lineValues)
        {
            string wholeName = lineValues[1];
            for (int i = 2; i < lineValues.Length; i++)
            {
                wholeName += " " + lineValues[i];
            }

            return wholeName;
        }

    }
}
