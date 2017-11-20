using Minkin_Lab02.Properties;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Minkin_Lab02
{
    internal class LogManager
    {
        public string WriteToFile(CurrentFilesCondition log, string path)
        {
            string fullfilepath = Path.Combine(path, DateTime.Now.Ticks.ToString() + Cache.Instance.GetCount().ToString() + Constants.XmlExtension);
            XmlSerializer serializer = new XmlSerializer(typeof(CurrentFilesCondition));
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception ex)
                {
                    throw new FileSystemError(string.Format(Resources.CreateFolderError, path), ex);
                }
            }
            using (FileStream filestream = new FileStream(fullfilepath, FileMode.OpenOrCreate))
            {
                try
                {
                    serializer.Serialize(filestream, log);
                }
                catch (Exception ex)
                {
                    throw new FileSystemError(Resources.WriteLogError, ex);
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
                catch (Exception ex)
                {
                    throw new FileSystemError(Resources.ReadLogError, ex);
                }
            }
            return log;
        }
    }
}
