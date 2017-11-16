using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Minkin_Lab02
{
    public class Backup
    {
        public string Path { get; set; }

        public Backup()
        {
            Path = "\\";
        }

        public Backup(string path)
        {
            Path = path;
        }

        public void BackupAllFolder(Config config)
        {
            IEnumerable<string> list = Directory.EnumerateFiles(Path, "*", SearchOption.AllDirectories);
            Log log = new Log(list);
            LogManager logManager = new LogManager();
            logManager.WriteToFile(log, config.LogsOfBackupFolder);
            FilesManager filesManager = new FilesManager();
            filesManager.WriteFileList(log, config.BackupFolder);
            Console.Write("");

            //CopyFile
        }
    }
}
