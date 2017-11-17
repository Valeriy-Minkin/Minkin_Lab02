using Minkin_Lab02.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Minkin_Lab02
{
    internal class ConfigManager
    {
        public string ConfigPath { get; private set; }

        public ConfigManager()
        {
            ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Resources.ConfigFileName);
        }

        public bool Exist()
        {
            return File.Exists(ConfigPath);
        }

        public void ReadConfig()
        {
            Cache.getInstance().CurrentConfig = new Config();
            if (this.Exist())
            {
                try
                {
                    Cache.getInstance().CurrentConfig = this.ReadFromFile();
                }
                catch (Exception ex)
                {
                    throw new Exception(Resources.ReadConfigError);
                }
                LogManager logManager = new LogManager();
                if (Cache.getInstance().CurrentConfig.Logs.Count != 0)
                {
                    try
                    {
                        Cache.getInstance().CurrentLog = logManager.ReadFromFile(Cache.getInstance().CurrentConfig.Logs.Last().Path);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else
                {
                    Cache.getInstance().CurrentLog = new CurrentFilesCondition();
                }
            }
            else
            {
                try
                {
                    this.WriteToFile(Cache.getInstance().CurrentConfig);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
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
                catch
                {
                    throw new Exception(Resources.ReadConfigError);
                }
            }
            return config;
        }

        public void WriteToFile(Config config)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            using (FileStream filestream = new FileStream(Resources.ConfigFileName, FileMode.OpenOrCreate))
            {
                try
                {
                    serializer.Serialize(filestream, config);
                }
                catch
                {
                    throw new Exception(Resources.WriteConfigError);
                }
            }
        }
    }
}
