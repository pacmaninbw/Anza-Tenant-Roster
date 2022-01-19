# Anza-Tenant-Roster

I'm trying to help the apartment complex I live in by automating some of the tasks the management office performs on a daily basis.

The tenant roster is a Microsoft Excel spreadsheet. I initially started and completed this project using VBA for excel, however, the excel application they are using is a starter edition and does not support VBA. I therefore created an app using C# and the Microsoft Interop libraries for Excel and Word. The user interface is using Window Forms.

Testing is currently completely manual. There are no unit tests written yet. Bugs found throuh testing are entered as issues.

The sources of most of the issues reported are peer review on the Stack Exchange Code Review Site. There are 2 code reviews of the model ([This one](https://codereview.stackexchange.com/questions/272954/follow-up-refactored-c-tool-to-generate-ms-word-document-mailbox-list-from-ms/273089#273089) by[t3chb0t](https://codereview.stackexchange.com/users/59161/t3chb0t) and [the first](https://codereview.stackexchange.com/questions/272954/follow-up-refactored-c-tool-to-generate-ms-word-document-mailbox-list-from-ms/272980#272980) by [AlanT](https://codereview.stackexchange.com/users/14509/alant)) and [one peer review of the User Interface](https://codereview.stackexchange.com/questions/272958/the-user-interface-code-for-a-simple-tool-that-generates-word-documents-from-exc/273106#273106) by [Peter Csala](https://codereview.stackexchange.com/users/224104/peter-csala).

The final step will be to package this program so that the apartment management office can install it.
