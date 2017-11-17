using Minkin_Lab02.Properties;
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
            string fullfilepath = Path.Combine(path, DateTime.Now.Ticks.ToString()+ Cache.getInstance().GetCount().ToString() + Resources.XmlMask);
            XmlSerializer serializer = new XmlSerializer(typeof(CurrentFilesCondition));
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch
                {
                    throw new Exception(Resources.CreateFolderError);
                }
            }
            using (FileStream filestream = new FileStream(fullfilepath, FileMode.OpenOrCreate))
            {
                try
                {
                    serializer.Serialize(filestream, log);
                }
                catch
                {
                    throw new Exception(Resources.WriteLogError);
                }
            }
            return fullfilepath;
        }

        public CurrentFilesCondition ReadFromFile(string path)
        {
            CurrentFilesCondition log = new CurrentFilesCondition();
            XmlSerializer serializer = new XmlSerializer(typeof(CurrentFilesCondition));
            using (FileStream filestream = new FileStream(path, FileMode.OpenOrCreate))
            {
                try
                {
                    log = (CurrentFilesCondition)serializer.Deserialize(filestream);
                }
                catch
                {
                    throw new Exception(Resources.ReadLogError);
                }
            }
            return log;
        }
    }
}
