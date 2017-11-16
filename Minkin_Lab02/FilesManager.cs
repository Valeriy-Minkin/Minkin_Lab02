using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minkin_Lab02
{
    public class FilesManager
    {
        public void WriteFileList(Log log, string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            foreach (var file in log.files)
            {
                File.Copy(file.Path, Path.Combine(path, file.BackupName));
            }
        }
    }
}
