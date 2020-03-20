using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using LINQtoCSV;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using csvToDb.EF;
using System.Collections;

namespace csvToDb
{
    class Functions
    {
        public static List<T> readCSV<T>(string filename)  where T : class, new()
        {
            List<T> items = new List<T>();
            CsvFileDescription inputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ';',
                FirstLineHasColumnNames = true,
                EnforceCsvColumnAttribute = true
            };
            CsvContext cc = new CsvContext();
            using (StreamReader sr = File.OpenText(filename))
            {
                items = cc.Read<T>(sr, inputFileDescription).ToList();
            }
            return items;
        }
        public static string getExceptionMessages(Exception ex){
            if (ex != null)
            {
                return ex.Message + Environment.NewLine + getExceptionMessages(ex.InnerException);
            }
            return "";
        }
    }
}
