using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minkin_Lab02
{
    internal class TimeMachine
    {
        public string Path { get; private set; }
        private Dictionary<string, DateTime> LastChangeOfFile { get; set; }

        public TimeMachine(string path)
        {
            Path = path;
            Watcher(path, FileType.File);
            Watcher(path, FileType.Folder);
        }

        private void Watcher(string path, FileType type)
        {
            string Filter = string.Empty;
            NotifyFilters notifyFilters;
            switch (type)
            {
                case FileType.Folder:
                    Filter = "*";
                    notifyFilters = NotifyFilters.DirectoryName;
                    break;
                default:
                    Filter = "*.txt";
                    notifyFilters = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName;
                    break;
            }
            LastChangeOfFile = new Dictionary<string, DateTime>();
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = Path;
            watcher.NotifyFilter = notifyFilters;
            watcher.Filter = Filter;
            watcher.IncludeSubdirectories = true;
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);
            watcher.EnableRaisingEvents = true;
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            if (Directory.Exists(e.FullPath)){ Console.WriteLine("This is sparta!"); }
            Console.WriteLine("File: {0} is {1}", e.FullPath, e.ChangeType);
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File: {0} is {1}", e.FullPath, e.ChangeType);
            
        }
    }
}
