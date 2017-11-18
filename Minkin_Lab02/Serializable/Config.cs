using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Configuration;
using Minkin_Lab02.Properties;

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
            ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Resources.ConfigFileName);
            FolderForLogs = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Resources.FolderForLogs);
            BackupFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Resources.FolderForBackup);
            MonitorableFolders = new List<string>();
            Logs = new List<Folder>();
        }
    }
}
