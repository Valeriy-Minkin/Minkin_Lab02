using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minkin_Lab02
{
    class ConfigManager
    {
        public string ConfigPath { get; private set; }

        public ConfigManager()
        {
            ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.xml");
        }

        public bool Exist()
        {
            return File.Exists(ConfigPath);
        }
    }
}
