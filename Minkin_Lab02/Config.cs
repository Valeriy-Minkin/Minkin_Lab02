using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Minkin_Lab02
{
    [Serializable]
    public class Config
    {
        private static string WorkFolder = Assembly.GetExecutingAssembly().Location.Substring(0, Assembly.GetExecutingAssembly().Location.LastIndexOf('\\'));
        private string ConfigPath;

        public string BackupFolder { get; set; }
        public List<string> MonitorableFolders { get; set; }

        public Config()
        {            
            ConfigPath = Path.Combine(WorkFolder, "config.xml");
            MonitorableFolders = new List<string>();
        }

        public bool ConfigExist()
        {
            return File.Exists(ConfigPath);
        }

        public static Config ReadConfig()
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

        public void WriteConfig()
        {
            BackupFolder = Path.Combine(WorkFolder, "backup\\");
            //MonitorableFolders.Add(String.Empty);
            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            using (FileStream filestream = new FileStream("config.xml", FileMode.OpenOrCreate))
            {
                serializer.Serialize(filestream, this);
            }
        }
    }
}
