using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Minkin_Lab02
{
    class Backup
    {
        public string Path { get; set; }

        public Backup()
        {
            Path = "\\";
        }

        public Backup(string path)
        {
            Path = path;
        }

        public void BackupAllFolder(Config config)
        {
            IEnumerable<string> list = Directory.EnumerateFiles(Path, "*", SearchOption.AllDirectories);
            Log log = new Log(list);
            log.WriteToFile(config.BackupFolder);

            Console.Write("");

            //CopyFile
        }
    }
}
