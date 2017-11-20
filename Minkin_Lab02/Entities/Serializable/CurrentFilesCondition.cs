using System;
using System.Collections.Generic;

namespace Minkin_Lab02
{
    [Serializable]
    public class CurrentFilesCondition
    {
        public List<FileData> Files { get; set; } 

        public CurrentFilesCondition()
        {
            Files = new List<FileData>();
        }

        public CurrentFilesCondition(IEnumerable<string> list)
        {
            Files = new List<FileData>();
            foreach (string file in list)
            {
                FileData data = new FileData
                {
                    Path = file,
                    Name = file.Substring(file.LastIndexOf('\\') + 1),
                    DateOfChange = DateTime.Now
                };
                data.BackupName = data.Name + data.DateOfChange.Ticks.ToString();
                Files.Add(data);
            }
        }
    }
}
