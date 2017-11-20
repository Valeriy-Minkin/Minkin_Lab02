using Minkin_Lab02.Properties;
using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Minkin_Lab02
{
    internal class ConfigManager
    {
        public string ConfigPath { get;}

        public ConfigManager()
        {
            ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constants.ConfigFileName);
        }

        public bool Exist()
        {
            return File.Exists(ConfigPath);
        }

        public void ReadConfig()
        {
            Cache.Instance.CurrentConfig = new Config();
            if (this.Exist())
            {
                Cache.Instance.CurrentConfig = this.ReadFromFile();
                LogManager logManager = new LogManager();
                if (Cache.Instance.CurrentConfig.Logs.Count != 0)
                {
                    Cache.Instance.CurrentLog = logManager.ReadFromFile(Cache.Instance.CurrentConfig.Logs.Last().Path);
                }
                else
                {
                    Cache.Instance.CurrentLog = new CurrentFilesCondition();
                }
            }
            else
            {
                this.WriteToFile(Cache.Instance.CurrentConfig);
            }
        }

        public Config ReadFromFile()
        {
            Config config = new Config();
            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            using (FileStream filestream = new FileStream(ConfigPath, FileMode.OpenOrCreate))
            {
                try
                {
                    config = (Config)serializer.Deserialize(filestream);
                }
                catch (Exception ex)
                {
                    throw new FileSystemError(Resources.ReadConfigError, ex);
                }
            }
            return config;
        }

        public void WriteToFile(Config config)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            using (FileStream filestream = new FileStream(Constants.ConfigFileName, FileMode.OpenOrCreate))
            {
                try
                {
                    serializer.Serialize(filestream, config);
                }
                catch (Exception ex)
                {
                    throw new FileSystemError(Resources.WriteConfigError, ex);
                }
            }
        }
    }
}
