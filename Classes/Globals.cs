using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantRosterAutomation
{
    public static class Globals
    {
        private static string preferencesFileName = "./MyPersonalRentRosterPreferences.txt";
        public static TenantDataTable TenantRoster;
        public static PropertyComplex Complex;
        public static UserPreferences Preferences;
        public static ExcelFileData ExcelFile;

        public static bool InitializeAll()
        {
            ReleaseAllObjects();
            bool everthingInitialized = false;

            Preferences = new UserPreferences(preferencesFileName);
            if (!string.IsNullOrEmpty(Preferences.ExcelWorkBookFullFileSpec))
            {
                ExcelFile = CreateExcelDataFile();
                if (ExcelFile != null)
                {
                    TenantRoster = new TenantDataTable(ExcelFile);
                    if (TenantRoster != null)
                    {
                        ConstructComplexAndReport(TenantRoster);
                    }
                    everthingInitialized = true;
                }
            }

            return everthingInitialized;
        }

        public static void ReleaseAllObjects()
        {
            Complex = null;
            ExcelFile = null;
            Preferences = null;
            TenantRoster = null;
        }

        public static void SavePreferences()
        {
            if (Preferences != null)
            {
                Preferences.SavePreferencesToFile();
            }
        }

        private static void ConstructComplexAndReport(TenantDataTable TenantRoster)
        {
            ReportCurrentStatusWindow statusReport = new ReportCurrentStatusWindow();
            statusReport.MessageText = "Constructing Apartment Complex Data.";
            statusReport.Show();
            Complex = new PropertyComplex("Anza Victoria Apartments, LLC",
                TenantRoster.TenantRoster);
            statusReport.Close();
        }

        private static ExcelFileData CreateExcelDataFile()
        {
            ExcelFileData excelFile = null;
            if (!string.IsNullOrEmpty(Preferences.ExcelWorkSheetName))
            {
                if (ExcelWorkBookAlreadyOpen.TestIfOpen(Preferences.ExcelWorkBookFullFileSpec))
                {
                    AlreadyOpenInExcelException e =
                        new AlreadyOpenInExcelException(
                            ExcelWorkBookAlreadyOpen.ReportOpen());
                    throw e;
                }
                excelFile = new ExcelFileData(Preferences.ExcelWorkBookFullFileSpec,
                    Preferences.ExcelWorkSheetName);
            }

            return excelFile;
        }


    }
}
