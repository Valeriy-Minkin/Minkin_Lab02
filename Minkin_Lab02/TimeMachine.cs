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
            FileSystemWatcher watcher = new FileSystemWatcher();
            switch (type)
            {
                case FileType.Folder:
                    Filter = "*";
                    notifyFilters = NotifyFilters.DirectoryName;
                    watcher.Deleted += new FileSystemEventHandler(OnDeletedFolder);
                    break;
                default:
                    Filter = "*.txt"; //
                    notifyFilters = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.CreationTime;
                    watcher.Deleted += new FileSystemEventHandler(OnDeleted);
                    break;
            }

            watcher.Path = Path;
            watcher.InternalBufferSize = 100000000;
            watcher.NotifyFilter = notifyFilters;
            watcher.Filter = Filter;
            watcher.IncludeSubdirectories = true;
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);

            watcher.Renamed += new RenamedEventHandler(OnRenamed);
            watcher.EnableRaisingEvents = true;
        }

        private void OnDeletedFolder(object sender, FileSystemEventArgs e)
        {
            BackupMachine backup = new BackupMachine(e.FullPath.Substring(0, e.FullPath.LastIndexOf('\\')));
            backup.BackupWithoutFile();
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            BackupMachine backup = new BackupMachine(e.FullPath.Substring(0, e.FullPath.LastIndexOf('\\')));
            backup.BackupWithoutFile();
            Console.WriteLine("File: {0} is {1}", e.FullPath, e.ChangeType);
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            if (!Directory.Exists(e.FullPath))
            {
                BackupMachine backup = new BackupMachine(e.FullPath.Substring(0, e.FullPath.LastIndexOf('\\')));
                backup.BackupFile(e.FullPath);
                Console.WriteLine("File: {0} is file {1}", e.FullPath, e.ChangeType);
            }
            else
            {
                BackupMachine backup = new BackupMachine(e.FullPath.Substring(0, e.FullPath.LastIndexOf('\\'))); //
                backup.BackupWithoutFile();
                Console.WriteLine("File: {0} is folder {1}", e.FullPath, e.ChangeType);
            }
            
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (!Directory.Exists(e.FullPath))
            {
                BackupMachine backup = new BackupMachine(e.FullPath.Substring(0, e.FullPath.LastIndexOf('\\')));
                backup.BackupFile(e.FullPath);
            }
            Console.WriteLine("File: {0} is {1}", e.FullPath, e.ChangeType);
        }
    }
}
