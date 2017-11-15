using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Minkin_Lab02
{
    class Config
    {
        private string ConfigPath = Path.Combine(Assembly.GetExecutingAssembly().Location.Substring(0, Assembly.GetExecutingAssembly().Location.LastIndexOf('\\')), "config.txt");

        public bool ConfigExist()
        {
            return File.Exists(ConfigPath);
        }

        public void ReadConfig()
        {

        }

        public void WriteConfig()
        {

        }
    }
}
