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

        public Backup(string path = "\\")
        {
            Path = path;
        }

        public void BackupAllFolder()
        {
            IEnumerable<string> list = Directory.EnumerateFiles(Path, "*", SearchOption.AllDirectories);
            Log log = new Log(list);
            LogManager logManager = new LogManager();
            string fullfilename = logManager.WriteToFile(log, CurrentData.getInstance().CurrentConfig.LogsOfBackupFolder);
            FilesManager filesManager = new FilesManager();
            filesManager.WriteFileList(log, CurrentData.getInstance().CurrentConfig.BackupFolder);
            Folder folder = new Folder() { Path = fullfilename, Time = DateTime.Now };
            CurrentData.getInstance().CurrentConfig.Logs.Add(folder);
            CurrentData.getInstance().CurrentLog = log;
        }

        public void BackupFile(string path)
        {
            IEnumerable<string> list = Directory.EnumerateFiles(Path, "*", SearchOption.AllDirectories);
            Log log = new Log(list);
            LogManager logManager = new LogManager();
            string fullfilename = logManager.WriteToFile(log, CurrentData.getInstance().CurrentConfig.LogsOfBackupFolder);
            FilesManager filesManager = new FilesManager(); 
            FileData data = new FileData() { Path = path, Name = path.Substring(path.LastIndexOf('\\') + 1), BackupName = path.Substring(path.LastIndexOf('\\') + 1) + DateTime.Now.Ticks.ToString()+CurrentData.getInstance().GetCount().ToString(), DateOfChange = DateTime.Now };
            filesManager.WriteFile(data, CurrentData.getInstance().CurrentConfig.BackupFolder);
            Folder folder = new Folder() { Path = fullfilename, Time = DateTime.Now };
            CurrentData.getInstance().CurrentConfig.Logs.Add(folder);
            CurrentData.getInstance().CurrentLog = log;
        }
    }
}
