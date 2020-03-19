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
            FileInfos files = new FileInfos(appSettings.folderPath, "*.csv");
            FileInfo latestFile = files.getLastModified();
            Console.WriteLine("Using file: " + latestFile.FullName + " | " + latestFile.LastWriteTime);

            // truncate table in db
            using (SqlConnection conn = new SqlConnection(appSettings.connectionString))
            {
                conn.Open();
                var text = @"TRUNCATE TABLE transactions;";
                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            //read csv file
            List<Transactions> items;
            CsvFileDescription inputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ';',
                FirstLineHasColumnNames = true,
                EnforceCsvColumnAttribute = true
            };
            CsvContext cc = new CsvContext();
            using (StreamReader sr = File.OpenText(latestFile.FullName))
            {
                items = cc.Read<Transactions>(sr, inputFileDescription).ToList();
            }

            //save to db
            var db = new budgetdbContext();
            foreach (var item in items) db.Transactions.Add(item);
            db.SaveChanges();
        }
    }
}
