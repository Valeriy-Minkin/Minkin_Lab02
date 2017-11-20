using System;

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
            string fullfilename = logManager.WriteToFile(Cache.Instance.CurrentLog, Cache.Instance.CurrentConfig.FolderForLogs);
            Folder folder = new Folder() { Path = fullfilename, Time = DateTime.Now };
            Cache.Instance.CurrentConfig.Logs.Add(folder);
            FilesManager filesManager = new FilesManager();
            filesManager.WriteFileList(changedfiles, Cache.Instance.CurrentConfig.BackupFolder);
        }

        public void BackupFile(string path)
        {
            FileData data = new FileData()
            {
                Path = path,
                Name = path.Substring(path.LastIndexOf('\\') + 1),
                BackupName = path.Substring(path.LastIndexOf('\\') + 1) + DateTime.Now.Ticks.ToString() + Cache.Instance.GetCount().ToString(),
                DateOfChange = DateTime.Now
            };
            Cache.Instance.CurrentLog.Files.RemoveAll(x => x.Path == data.Path);
            Cache.Instance.CurrentLog.Files.Add(data);
            Cache.Instance.ChangedFiles.Files.RemoveAll(x => x.Path == data.Path);
            Cache.Instance.ChangedFiles.Files.Add(data);
        }

        public void BackupWithoutFile()
        {
            Cache.Instance.CurrentLog.Files.RemoveAll(x => x.Path.Contains(Path));
            LogManager logManager = new LogManager();
            string fullfilename = logManager.WriteToFile(Cache.Instance.CurrentLog, Cache.Instance.CurrentConfig.FolderForLogs);
            Folder folder = new Folder() { Path = fullfilename, Time = DateTime.Now };
            Cache.Instance.CurrentConfig.Logs.Add(folder);
            Cache.Instance.HasChanges = true;
        }
    }
}
