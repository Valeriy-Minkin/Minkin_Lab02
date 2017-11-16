using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minkin_Lab02
{
    [Serializable]
    public class FileData
    {
        public string Path { get; set; }
        public DateTime DateOfChange { get; set; }
        public string BackupName { get; set; }
    }
}
