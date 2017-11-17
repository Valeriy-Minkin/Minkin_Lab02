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
        public string WriteToFile(CurrentFilesCondition log, string path)
        {
            string fullfilepath = Path.Combine(path, DateTime.Now.Ticks.ToString()+ Cache.getInstance().GetCount().ToString() + ".xml");
            XmlSerializer serializer = new XmlSerializer(typeof(CurrentFilesCondition));
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

        public CurrentFilesCondition ReadFromFile(string path)
        {
            CurrentFilesCondition log = new CurrentFilesCondition();
            XmlSerializer serializer = new XmlSerializer(typeof(CurrentFilesCondition));
            using (FileStream filestream = new FileStream(path, FileMode.OpenOrCreate))
            {
                log = (CurrentFilesCondition)serializer.Deserialize(filestream);
            }
            return log;
        }
    }
}
