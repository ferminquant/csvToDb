using System.IO;

namespace csvToDb
{
    class FileInfos
    {
        private FileInfo[] files;
        public FileInfos(string folderPath, string searchPattern)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            files = di.GetFiles(searchPattern, SearchOption.TopDirectoryOnly);
        }
        public FileInfo getLastModified()
        {
            FileInfo latestFile = null;
            foreach (FileInfo file in files)
            {
                if (latestFile == null) { latestFile = file; }
                else { if (latestFile.LastWriteTime < file.LastWriteTime) { latestFile = file; } }
            }
            return latestFile;
        }
    }
}
