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
            if (Cache.getInstance().ChangedFiles.Files != null && Cache.getInstance().ChangedFiles.Files.Count != 0)
            {
                BackupMachine backup = new BackupMachine();
                try
                {
                    backup.BackupFilesFromFolder(Cache.getInstance().ChangedFiles);
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                ConfigManager configManager = new ConfigManager();
                try
                {
                    configManager.WriteToFile(Cache.getInstance().CurrentConfig);
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                Cache.getInstance().ChangedFiles.Files = new List<FileData>();
            }
            if (Cache.getInstance().HasChanges)
            {
                ConfigManager configManager = new ConfigManager();
                try
                {
                    configManager.WriteToFile(Cache.getInstance().CurrentConfig);
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                Cache.getInstance().HasChanges = false;
            }
        }

        public void CheckFirstStart()
        {
            if (!Directory.Exists(Cache.getInstance().CurrentConfig.FolderForLogs))
            {
                BackupMachine backup = new BackupMachine();
                backup.Path = Cache.getInstance().CurrentConfig.MonitorableFolders.First();
                IEnumerable<string> list = Directory.EnumerateFiles(backup.Path, Resources.TxtMask, SearchOption.AllDirectories);
                CurrentFilesCondition log = new CurrentFilesCondition(list);
                Cache.getInstance().CurrentLog = log;
                try
                {
                    backup.BackupFilesFromFolder(log);
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                ConfigManager configManager = new ConfigManager();
                try
                {
                    configManager.WriteToFile(Cache.getInstance().CurrentConfig);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Resources.FolderForMonitorableData)))
            {
                try
                {
                    Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Resources.FolderForMonitorableData));
                }
                catch
                {
                    throw new Exception(Resources.CreateFolderError);
                }
            }
        }
    }
}
