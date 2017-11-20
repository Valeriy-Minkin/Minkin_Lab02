using System.Collections.Generic;
using System.IO;

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
            FileSystemWatcher watcher;
            switch (type)
            {
                case FileType.Folder:
                    watcher = CreateFolderWatcher();
                    break;
                default:
                    watcher = CreateFileWatcher();
                    break;
            }
            watcher.Path = Path;
            watcher.InternalBufferSize = int.MaxValue / 2;
            watcher.IncludeSubdirectories = true;
            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;
            watcher.Renamed += OnRenamed;
            watcher.EnableRaisingEvents = true;
        }

        private FileSystemWatcher CreateFileWatcher()
        {
            FileSystemWatcher watcher = new FileSystemWatcher
            {
                Filter = Constants.TxtMask,
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.CreationTime
            };
            watcher.Deleted += OnDeleted;
            return watcher;
        }

        private FileSystemWatcher CreateFolderWatcher()
        {
            FileSystemWatcher watcher = new FileSystemWatcher
            {
                Filter = "*",
                NotifyFilter = NotifyFilters.DirectoryName
            };
            watcher.Deleted += OnDeletedFolder;
            return watcher;
        }

        private void OnDeletedFolder(object sender, FileSystemEventArgs e)
        {
            BackupMachine backup = new BackupMachine(e.FullPath);
                backup.BackupWithoutFile();
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            BackupMachine backup = new BackupMachine(e.FullPath);
                backup.BackupWithoutFile();
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            if (!Directory.Exists(e.FullPath))
            {
                BackupMachine backup = new BackupMachine(e.FullPath.Substring(0, e.FullPath.LastIndexOf('\\')));
                    backup.BackupFile(e.FullPath);
            }
            else
            {
                BackupMachine backup = new BackupMachine(e.FullPath);
                backup.BackupRenamedFolder(e.OldFullPath);
            }

        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            string temp = e.FullPath.Substring(0, e.FullPath.LastIndexOf("\\"));
            if (!Directory.Exists(e.FullPath))
            {
                BackupMachine backup = new BackupMachine(e.FullPath.Substring(0, e.FullPath.LastIndexOf("\\")));
                backup.BackupFile(e.FullPath);
            }
            else
            {
                IEnumerable<string> list = Directory.EnumerateFiles(e.FullPath, Constants.TxtMask, SearchOption.AllDirectories);
                CurrentFilesCondition log = new CurrentFilesCondition(list);
                foreach(var file in log.Files)
                {
                    Cache.Instance.CurrentLog.Files.Add(file);
                    Cache.Instance.ChangedFiles.Files.Add(file);
                }
            }
        }
    }
}
