using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Minkin_Lab02
{
    internal class BackgroundWorker
    {
        public void CacheWrite()
        {
            if (Cache.Instance.ChangedFiles.Files != null && Cache.Instance.ChangedFiles.Files.Any())
            {
                BackupMachine backup = new BackupMachine();
                backup.BackupFilesFromFolder(Cache.Instance.ChangedFiles);
                ConfigManager configManager = new ConfigManager();
                configManager.WriteToFile(Cache.Instance.CurrentConfig);
                Cache.Instance.ChangedFiles.Files = new List<FileData>();
            }
            if (Cache.Instance.HasChanges)
            {
                ConfigManager configManager = new ConfigManager();
                configManager.WriteToFile(Cache.Instance.CurrentConfig);
                Cache.Instance.HasChanges = false;
            }
        }

        public void CheckFirstStart()
        {
            FilesManager filesManager = new FilesManager();
            filesManager.CheckFolderExistOrCreateNewFolder(Constants.FolderForMonitorableData);
            filesManager.CheckFolderExistOrCreateNewFolder(Constants.FolderNameForBackups);
            if (!Directory.Exists(Cache.Instance.CurrentConfig.FolderForLogs))
            {
                BackupMachine backup = new BackupMachine();
                Cache.Instance.CurrentConfig.MonitorableFolders.Add(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constants.FolderForMonitorableData));
                backup.Path = Cache.Instance.CurrentConfig.MonitorableFolders.First();
                IEnumerable<string> list = Directory.EnumerateFiles(backup.Path, Constants.TxtMask, SearchOption.AllDirectories);
                CurrentFilesCondition log = new CurrentFilesCondition(list);
                Cache.Instance.CurrentLog = log;
                backup.BackupFilesFromFolder(log);
                ConfigManager configManager = new ConfigManager();
                configManager.WriteToFile(Cache.Instance.CurrentConfig);
            }
        }
    }
}
