# Anza-Tenant-Roster

I'm trying to help the apartment complex I live in by automating some of the tasks the management office performs on a daily basis.

The tenant roster is a Microsoft Excel spreadsheet. I initially started and completed this project using VBA for excel, however, the excel application they are using is a starter edition and does not support VBA. I therefore created an app using C# and the Microsoft Interop libraries for Excel and Word. The user interface is using Window Forms.

Testing is currently completely manual. There are no unit tests written yet.

The code is now pending code review on [Code Review](https://codereview.stackexchange.com/questions/272954/follow-up-refactored-c-tool-to-generate-ms-word-document-mailbox-list-from-ms).

The final step will be to package this program so that the apartment management office can install it.
