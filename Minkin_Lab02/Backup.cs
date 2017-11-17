using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Minkin_Lab02
{
    public class BackupMachine
    {
        public string Path { get; set; }

        public BackupMachine(string path = "\\")
        {
            Path = path;
        }

        public void BackupFilesFromFolder(CurrentFilesCondition changedfiles)
        {  
            LogManager logManager = new LogManager();
            string fullfilename = logManager.WriteToFile(Cache.getInstance().CurrentLog, Cache.getInstance().CurrentConfig.LogsOfBackupFolder);
            FilesManager filesManager = new FilesManager();
            filesManager.WriteFileList(changedfiles, Cache.getInstance().CurrentConfig.BackupFolder);
            Folder folder = new Folder() { Path = fullfilename, Time = DateTime.Now };
            Cache.getInstance().CurrentConfig.Logs.Add(folder);
        }

        public void BackupFile(string path)
        {
            FileData data = new FileData() { Path = path, Name = path.Substring(path.LastIndexOf('\\') + 1), BackupName = path.Substring(path.LastIndexOf('\\') + 1) + DateTime.Now.Ticks.ToString()+Cache.getInstance().GetCount().ToString(), DateOfChange = DateTime.Now };
            Cache.getInstance().CurrentLog.files.RemoveAll(x => x.Path == data.Path);
            Cache.getInstance().CurrentLog.files.Add(data);
            Cache.getInstance().ChangedFiles.files.RemoveAll(x => x.Path == data.Path);
            Cache.getInstance().ChangedFiles.files.Add(data);
        }

        public void BackupWithoutFile()
        {
            Cache.getInstance().CurrentLog.files.RemoveAll(x => x.Path == Path);
            LogManager logManager = new LogManager();
            string fullfilename = logManager.WriteToFile(Cache.getInstance().CurrentLog, Cache.getInstance().CurrentConfig.LogsOfBackupFolder);
            Folder folder = new Folder() { Path = fullfilename, Time = DateTime.Now };
            Cache.getInstance().CurrentConfig.Logs.Add(folder);
            Cache.getInstance().HasChanges = true;
        }
    }
}
