using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Minkin_Lab02
{
    [Serializable]
    public class Log
    {
        public List<FileData> files { get; set; } 

        public Log()
        {
            files = new List<FileData>();
        }

        public Log(IEnumerable<string> list)
        {
            files = new List<FileData>();
            foreach (string file in list)
            {
                FileData data = new FileData();
                data.Path = file;
                data.DateOfChange = DateTime.Now;
                data.BackupName = file.Substring(file.LastIndexOf('\\')) + data.DateOfChange.Ticks.ToString();
                files.Add(data);
            }
        }

        public void WriteToFile(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Log));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream filestream = new FileStream(Path.Combine(path, DateTime.Now.Ticks.ToString()+".xml"), FileMode.OpenOrCreate))
            {
                serializer.Serialize(filestream, this);
            }
        }
    }
}
