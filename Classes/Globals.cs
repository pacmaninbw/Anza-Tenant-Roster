using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantRosterAutomation
{
    // Perhaps this class and file should be renamed to GlobalModels
    // or just Modles. For performance reasons the classes provided
    // by this static class are created once at the beginning of the
    // program execution and deleted at the end of program execution.
    public static class Globals
    {
        private static string preferencesFileName = "./MyPersonalRentRosterPreferences.txt";
        public static TenantDataTable TenantRoster;
        public static PropertyComplex Complex;
        public static UserPreferences Preferences;
        public static ExcelFileData ExcelFile;

        public static bool InitializeAllModels()
        {
            bool everthingInitialized = false;
            ReleaseAllModels();

            Preferences = new UserPreferences(preferencesFileName);
            everthingInitialized = AdvancedInitialization();

            return everthingInitialized;
        }

        // Restart with new preferences
        public static bool ReInitizeAllModels(UserPreferences preferences)
        {
            bool everthingInitialized = false;
            ReleaseAllModels(false);

            Preferences.CopyValues(preferences);
            everthingInitialized = AdvancedInitialization();

            return everthingInitialized;
        }

        public static void ReleaseAllModels(bool releasePrefernces = true)
        {
            Complex = null;
            ExcelFile = null;
            TenantRoster = null;
            if (releasePrefernces)
            {
                Preferences = null;
            }
        }

        public static void Save()
        {
            SavePreferences();
            SaveTenantData();
        }

        public static void SavePreferences()
        {
            if (Preferences != null)
            {
                Preferences.SavePreferencesToFile();
            }
        }

        public static void SaveTenantData()
        {
            if (TenantRoster != null && TenantRoster.DataChanged)
            {
                TenantRoster.SaveChanges();
            }
        }

        private static bool AdvancedInitialization()
        {
            bool everthingInitialized = false;

            if (!string.IsNullOrEmpty(Preferences.ExcelWorkBookFullFileSpec) &&
                !string.IsNullOrEmpty(Preferences.ExcelWorkSheetName))
            {
                ExcelFile = CreateExcelDataFile();
                if (ExcelFile != null)
                {
                    TenantRoster = new TenantDataTable(ExcelFile);
                    if (TenantRoster != null && TenantRoster.TenantRoster != null)
                    {
                        ConstructComplexAndReport(TenantRoster);
                    }
                    everthingInitialized = true;
                }
            }

            return everthingInitialized;
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
            if (!string.IsNullOrEmpty(Preferences.ExcelWorkBookFullFileSpec))
            {
                CheckExcelWorkBookOpen testOpen = new CheckExcelWorkBookOpen();
                testOpen.TestAndThrowIfOpen(Preferences.ExcelWorkBookFullFileSpec, true);
                testOpen = null;
                excelFile = new ExcelFileData(Preferences.ExcelWorkBookFullFileSpec,
                    Preferences.ExcelWorkSheetName);
            }

            return excelFile;
        }
    }
}
