using System;
using System.Collections.Generic;
using System.IO;

namespace Minkin_Lab02
{
    [Serializable]
    public class Config
    {
        public string ConfigPath { get; set; }
        public string FolderForLogs { get; set; }
        public string BackupFolder { get; set; }

        public List<string> MonitorableFolders { get; set; }
        public List<Folder> Logs { get; set; }

        public Config()
        {
            ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constants.ConfigFileName);
            FolderForLogs = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constants.FolderNameForLogs);
            BackupFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constants.FolderNameForBackups);
            MonitorableFolders = new List<string>();
            Logs = new List<Folder>();
        }
    }
}
