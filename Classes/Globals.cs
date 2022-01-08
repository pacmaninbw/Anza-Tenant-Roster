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
        public static List<Apartment> TenantUpdates;
        public static string ExcelWorkBookFullFileSpec;
        public static string ExcelWorkSheetName;
        public static PrintSavePreference.PrintSave PrintSave;
        public static string DefaultSaveDirectory;
        public static PropertyComplex Complex;
        public static bool HavePreferenceData;
        public static UserPreferences Preferences;
        public static ExcelFileData ExcelFile;

        public static bool InitializeAll()
        {
            bool everthingInitialized = false;
            ReleaseAllObjects();
            Preferences = new UserPreferences(preferencesFileName);
            TransferPrefernces(Preferences);
            if (!string.IsNullOrEmpty(ExcelWorkBookFullFileSpec))
            {
                ExcelFile = new ExcelFileData(ExcelWorkBookFullFileSpec, ExcelWorkSheetName);
            }
            if (!string.IsNullOrEmpty(ExcelWorkSheetName))
            {
                TenantRoster = new TenantDataTable(ExcelFile);
                ConstructComplexAndReport();
                TenantUpdates = new List<Apartment>();
                everthingInitialized = true;
            }
            return everthingInitialized;
        }

        public static void ReleaseAllObjects()
        {
            Complex = null;
            DefaultSaveDirectory = null;
            ExcelFile = null;
            ExcelWorkBookFullFileSpec = null;
            ExcelWorkSheetName = null;
            Preferences = null;
            TenantRoster = null;
            TenantUpdates = null;
        }

        public static void SavePreferences()
        {
            if (Preferences == null)
            {
                Preferences = new UserPreferences(preferencesFileName);
            }
            Preferences.DefaultSaveDirectory = DefaultSaveDirectory;
            Preferences.RentRosterFile = ExcelWorkBookFullFileSpec;
            Preferences.RentRosterSheet = ExcelWorkSheetName;
            Preferences.PrintSaveOptions = PrintSave;
            Preferences.SavePreferencesToFile();
        }

        private static void TransferPrefernces(UserPreferences preferences)
        {
            HavePreferenceData = preferences.HavePreferenceData;
            DefaultSaveDirectory = preferences.DefaultSaveDirectory;
            ExcelWorkBookFullFileSpec = preferences.RentRosterFile;
            ExcelWorkSheetName = preferences.RentRosterSheet;
            PrintSave = preferences.PrintSaveOptions;
        }

        private static void ConstructComplexAndReport()
        {
            if (Globals.TenantRoster == null)
            {
                return;
            }
            ReportCurrentStatusWindow statusReport = new ReportCurrentStatusWindow();
            statusReport.MessageText = "Constructing Apartment Complex Data.";
            statusReport.Show();
            Complex = new PropertyComplex("Anza Victoria Apartments, LLC", TenantRoster.TenantRoster);
            statusReport.Close();
        }

    }
}
