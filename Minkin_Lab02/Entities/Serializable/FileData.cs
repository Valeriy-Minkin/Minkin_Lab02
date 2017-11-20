using System;

namespace Minkin_Lab02
{
    [Serializable]
    public class FileData
    {
        public string Path { get; set; }
        public DateTime DateOfChange { get; set; }
        public string BackupName { get; set; }
        public string Name { get; set; }
    }
}
