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
    public class CurrentFilesCondition
    {
        public List<FileData> files { get; set; } 

        public CurrentFilesCondition()
        {
            files = new List<FileData>();
        }

        public CurrentFilesCondition(IEnumerable<string> list)
        {
            files = new List<FileData>();
            foreach (string file in list)
            {
                FileData data = new FileData();
                data.Path = file;
                data.Name = file.Substring(file.LastIndexOf('\\') + 1);
                data.DateOfChange = DateTime.Now;
                data.BackupName = data.Name + data.DateOfChange.Ticks.ToString();
                files.Add(data);
            }
        }
    }
}
