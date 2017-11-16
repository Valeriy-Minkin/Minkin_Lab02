using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Configuration;

namespace Minkin_Lab02
{
    [Serializable]
    public class Config
    {
        public string ConfigPath { get; set; }
        public string LogsOfBackupFolder { get; set; }
        public string BackupFolder { get; set; }

        public List<string> MonitorableFolders { get; set; }
        public List<Folder> Logs { get; set; }

        public Config()
        {
            ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.xml");
            LogsOfBackupFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "backup\\");
            BackupFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "backupedFiles\\");
            MonitorableFolders = new List<string>();
            Logs = new List<Folder>();
            // MonitorableFolders.Add(AppDomain.CurrentDomain.BaseDirectory);
            MonitorableFolders.Add(@"C:\Users\ray-s_000\Documents\Lab_01\Task10\TestFolder");
        }
    }
}
