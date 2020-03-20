using System;
using System.Collections.Generic;
using System.IO;
using csvToDb.EF;
using Serilog;

namespace csvToDb
{
    class Program
    {
        static void Main(string[] args)
        {
            try {
                // initializing log
                Log.Logger = new LoggerConfiguration()
                                .MinimumLevel.Debug()
                                .WriteTo.Console()
                                .WriteTo.File("log\\csvToDb.log", rollingInterval: RollingInterval.Month)
                                .CreateLogger();

                // read appsettings.json
                AppSettings appSettings = new AppSettings("appsettings.json");

                // find last modified file
                Log.Information($"Searching '{appSettings.folderPath}'...");
                FileInfos files = new FileInfos(appSettings.folderPath, "*.csv");
                FileInfo latestFile = files.getLastModified();

                // read csv file            
                Log.Information($"Reading CSV file: {latestFile.FullName} | {latestFile.LastWriteTime}");
                List<Transactions> items = Functions.readCSV<Transactions>(latestFile.FullName);
                Log.Information($"Read {items.Count} items");

                // truncate table
                Log.Information("Truncating table");
                var db = new budgetdbContext();
                db.truncateTable(typeof(Transactions));
                db.Transactions.UpdateRange(db.Transactions);

                // save to db
                Log.Information("Saving data to db");
                foreach (var item in items) db.Transactions.Add(item);
                db.SaveChanges();

                Log.Information("Successfully Finished Execution");
            }
            catch (Exception ex) {
                Log.Error($"An error was found: {Functions.getExceptionMessages(ex)} ");
            }
        }
    }
}
