using System;
using System.Collections.Generic;
using LINQtoCSV;

namespace csvToDb.EF
{
    public partial class Transactions
    {
        [CsvColumn(Name = "Date")]
        public DateTime Date { get; set; }

        [CsvColumn(Name = "Title")]
        public string Title { get; set; }

        [CsvColumn(Name = "Comment")]
        public string Comment { get; set; }

        [CsvColumn(Name = "Main category")]
        public string MainCategory { get; set; }

        [CsvColumn(Name = "Subcategory")]
        public string SubCategory { get; set; }

        [CsvColumn(Name = "Account")]
        public string Account { get; set; }

        [CsvColumn(Name = "Amount")]
        public decimal Amount { get; set; }
        
        public int Id { get; set; }
    }
}
