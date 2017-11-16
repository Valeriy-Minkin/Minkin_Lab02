using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Minkin_Lab02
{
    internal class LogManager
    {
        public string WriteToFile(Log log, string path)
        {
            string fullfilepath = Path.Combine(path, DateTime.Now.Ticks.ToString()+ CurrentData.getInstance().GetCount().ToString() + ".xml");
            XmlSerializer serializer = new XmlSerializer(typeof(Log));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream filestream = new FileStream(fullfilepath, FileMode.OpenOrCreate))
            {
                serializer.Serialize(filestream, log);
            }
            return fullfilepath;
        }

        public Log ReadFromFile(string path)
        {
            Log log = new Log();
            XmlSerializer serializer = new XmlSerializer(typeof(Log));
            using (FileStream filestream = new FileStream(path, FileMode.OpenOrCreate))
            {
                log = (Log)serializer.Deserialize(filestream);
            }
            return log;
        }
    }
}
