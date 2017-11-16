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
        private static string WorkFolder = Assembly.GetExecutingAssembly().Location.Substring(0, Assembly.GetExecutingAssembly().Location.LastIndexOf('\\'));
        private string ConfigPath;

        public string BackupFolder { get; set; }
        public List<string> MonitorableFolders { get; set; }
        public List<Folder> Logs { get; set; }

        public Config()
        {
            ConfigPath = Path.Combine(WorkFolder, "config.xml");
            MonitorableFolders = new List<string>();
            Logs = new List<Folder>();
            // MonitorableFolders.Add(AppDomain.CurrentDomain.BaseDirectory);
            MonitorableFolders.Add(@"C:\Users\ray-s_000\Documents\Lab_01\Task10\TestFolder");
        }

        public bool Exist()
        {
            return File.Exists(ConfigPath);
        }

        public static Config ReadFromFile()
        {
            Config config = new Config();
            string configPath = Path.Combine(WorkFolder, "config.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            using (FileStream filestream = new FileStream(configPath, FileMode.OpenOrCreate))
            {
                config = (Config)serializer.Deserialize(filestream);
            }
            return config;
        }

        public void WriteToFile()
        {
            BackupFolder = Path.Combine(WorkFolder, "backup\\");
            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            using (FileStream filestream = new FileStream("config.xml", FileMode.OpenOrCreate))
            {
                serializer.Serialize(filestream, this);
            }
        }
    }
}
