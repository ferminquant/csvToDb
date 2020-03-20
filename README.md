# csvToDb

This is a command line app to monitor a folder for csv files, read the newest one and save to a database.
Below a description of each file:
* Program.cs: main program execution. Finds and reads a CSV file and saves to DB.
* Functions.cs: helper functions are here as static functions. 
* FileInfos.cs: an object to represent the list of files in a folder and a method to get the file that was last modified.
* appsettings_template.json: a template file to serve as a guide to create appsettings.json and avoid storing secrets in source control.
* EF folder: classes for entity framework are stored here
  * budgetdbContext.cs: added a function to truncate the table based on the object type.
  * Transactions.cs: added metadata to use LINQtoCSV