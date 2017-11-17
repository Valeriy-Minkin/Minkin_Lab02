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

        public void BackupAllFolder(Log log, Log changedfiles)
        {  
            LogManager logManager = new LogManager();
            string fullfilename = logManager.WriteToFile(changedfiles, CurrentData.getInstance().CurrentConfig.LogsOfBackupFolder);
            FilesManager filesManager = new FilesManager();
            filesManager.WriteFileList(changedfiles, CurrentData.getInstance().CurrentConfig.BackupFolder);
            Folder folder = new Folder() { Path = fullfilename, Time = DateTime.Now };
            CurrentData.getInstance().CurrentConfig.Logs.Add(folder);
            IEnumerable<string> list = Directory.EnumerateFiles(Path, "*", SearchOption.AllDirectories);
            log = new Log(list);
            CurrentData.getInstance().CurrentLog = log;
        }

        public void BackupFile(string path)
        {
            IEnumerable<string> list = Directory.EnumerateFiles(Path, "*", SearchOption.AllDirectories);
            Log log = new Log(list);
            FileData data = new FileData() { Path = path, Name = path.Substring(path.LastIndexOf('\\') + 1), BackupName = path.Substring(path.LastIndexOf('\\') + 1) + DateTime.Now.Ticks.ToString()+CurrentData.getInstance().GetCount().ToString(), DateOfChange = DateTime.Now };
            
            if (!(CurrentData.getInstance().ChangedFiles.files.Count(x=>x.Path.Equals(data.Path))!=0))
            {
                CurrentData.getInstance().ChangedFiles.files.Add(data);
            }           
            CurrentData.getInstance().CurrentLog = log;
        }

        public void BackupWithoutFile()
        {
            IEnumerable<string> list = Directory.EnumerateFiles(Path, "*", SearchOption.AllDirectories);
            Log log = new Log(list);
            LogManager logManager = new LogManager();
            string fullfilename = logManager.WriteToFile(log, CurrentData.getInstance().CurrentConfig.LogsOfBackupFolder);
            Folder folder = new Folder() { Path = fullfilename, Time = DateTime.Now };
            CurrentData.getInstance().CurrentConfig.Logs.Add(folder);
            CurrentData.getInstance().CurrentLog = log;
        }
    }
}
