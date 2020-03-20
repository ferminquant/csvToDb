using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using LINQtoCSV;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using csvToDb.EF;

namespace csvToDb
{
    class Program
    {
        static void Main(string[] args)
        {
            // read appsettings.json
            AppSettings appSettings = new AppSettings("appsettings.json");

            // find last modified file
            Console.WriteLine($"Searching '{appSettings.folderPath}'...");
            FileInfos files = new FileInfos(appSettings.folderPath, "*.csv");
            FileInfo latestFile = files.getLastModified();

            // read csv file            
            Console.WriteLine($"Reading CSV file: {latestFile.FullName} | {latestFile.LastWriteTime}");
            List<Transactions> items = Functions.readCSV<Transactions>(latestFile.FullName);
            Console.WriteLine($"Read {items.Count} items");

            // truncate table
            Console.WriteLine("Truncating table");
            var db = new budgetdbContext();
            db.truncateTable(typeof(Transactions));
            db.Transactions.UpdateRange(db.Transactions);

            // save to db
            Console.WriteLine("Saving data to db");
            foreach (var item in items) db.Transactions.Add(item);
            db.SaveChanges();
        }
    }
}
