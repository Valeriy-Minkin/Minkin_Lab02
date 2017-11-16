using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Minkin_Lab02
{
    internal class TimeMachine
    {
        public string Path { get; private set; }

        private string lastchangedfile;

        public TimeMachine(string path)
        {
            Path = path;
            Watcher(path, FileType.File);
            Watcher(path, FileType.Folder);
            lastchangedfile = string.Empty;
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
                    Filter = "*"; //.txt
                    notifyFilters = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.CreationTime;
                    break;
            }
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = Path;
            watcher.InternalBufferSize = 100000000;
            watcher.NotifyFilter = notifyFilters;
            watcher.Filter = Filter;
            watcher.IncludeSubdirectories = true;
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnDeleted);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);
            watcher.EnableRaisingEvents = true;
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            if (!Directory.Exists(e.FullPath))
            {//фаил
                CurrentData.getInstance().fuck++;
            }

            Console.WriteLine("File: {0} is {1}", e.FullPath, e.ChangeType);
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (!Directory.Exists(e.FullPath))
            {//фаил
                Backup backup = new Backup(e.FullPath.Substring(0,e.FullPath.LastIndexOf('\\')));
                backup.BackupFile(e.FullPath);
                CurrentData.getInstance().fuck++;
            }
            int iii = CurrentData.getInstance().fuck;
            Console.WriteLine("File: {0} is {1}", e.FullPath, e.ChangeType);
        }
    }
}
