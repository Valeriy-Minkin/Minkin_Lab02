using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Minkin_Lab02
{
    internal class LogManager
    {
        public void WriteToFile(Log log, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Log));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream filestream = new FileStream(Path.Combine(path, DateTime.Now.Ticks.ToString() + ".xml"), FileMode.OpenOrCreate))
            {
                serializer.Serialize(filestream, log);
            }
        }
    }
}
