using System.IO;
using Microsoft.Extensions.Configuration;

namespace csvToDb
{
    class AppSettings
    {
        public string connectionString { get; set; }
        public string folderPath { get; set; }

        public AppSettings(string fileName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(fileName, optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            
            this.connectionString = configuration.GetConnectionString("azuresql");
            this.folderPath = configuration.GetSection("Folder:Path").Value;
        }
    }
}
