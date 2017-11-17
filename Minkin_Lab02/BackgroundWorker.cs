using Minkin_Lab02.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minkin_Lab02
{
    internal class BackgroundWorker
    {
        public void CacheWrite()
        {
            if (Cache.getInstance().ChangedFiles.files != null && Cache.getInstance().ChangedFiles.files.Count != 0)
            {
                BackupMachine backup = new BackupMachine();
                backup.BackupFilesFromFolder(Cache.getInstance().ChangedFiles);
                ConfigManager configManager = new ConfigManager();
                configManager.WriteToFile(Cache.getInstance().CurrentConfig);
                Cache.getInstance().ChangedFiles.files = new List<FileData>();
            }
            if (Cache.getInstance().HasChanges)
            {
                ConfigManager configManager = new ConfigManager();
                configManager.WriteToFile(Cache.getInstance().CurrentConfig);
                Cache.getInstance().HasChanges = false;
            }
        }

        public void CheckFirstStart()
        {
            if (!Directory.Exists(Cache.getInstance().CurrentConfig.LogsOfBackupFolder))
            {
                BackupMachine backup = new BackupMachine();
                backup.Path = Cache.getInstance().CurrentConfig.MonitorableFolders.First();
                IEnumerable<string> list = Directory.EnumerateFiles(backup.Path, Resources.TxtMask, SearchOption.AllDirectories);
                CurrentFilesCondition log = new CurrentFilesCondition(list);
                Cache.getInstance().CurrentLog = log;
                backup.BackupFilesFromFolder(log);
                ConfigManager configManager = new ConfigManager();
                configManager.WriteToFile(Cache.getInstance().CurrentConfig);
            }
        }
    }
}
