using System.Collections.Generic;

namespace TenantRosterAutomation
{
    public class PrintSavePreference
    {
        private Dictionary<PrintSave, string> PrintSaveToStringDic;
        private Dictionary<string, PrintSave> StringToPrintSaveDic;

        public enum PrintSave
        {
            PrintOnly,
            PrintAndSave,
            SaveOnly
        }

        public PrintSavePreference()
        {
            InitDictionaries();
        }

        public string ConvertPrintSaveToString(PrintSave printSave)
        {
            string printSaveString;

            PrintSaveToStringDic.TryGetValue(printSave, out printSaveString);

            return printSaveString;
        }

        public PrintSave ConvertStringToPrintSave(string printSaveString)
        {
            PrintSave retValue;

            StringToPrintSaveDic.TryGetValue(printSaveString, out retValue);

            return retValue;
        }

        private void InitDictionaries()
        {
            PrintSaveToStringDic = new Dictionary<PrintSave, string>();
            PrintSaveToStringDic.Add(PrintSave.PrintOnly, "Print_Only");
            PrintSaveToStringDic.Add(PrintSave.PrintAndSave, "Print_and_Save");
            PrintSaveToStringDic.Add(PrintSave.SaveOnly, "Save_Only");

            StringToPrintSaveDic = new Dictionary<string, PrintSave>();
            StringToPrintSaveDic.Add("Print_Only", PrintSave.PrintOnly);
            StringToPrintSaveDic.Add("Print_and_Save", PrintSave.PrintAndSave);
            StringToPrintSaveDic.Add("Save_Only", PrintSave.SaveOnly);
        }

    }
}
