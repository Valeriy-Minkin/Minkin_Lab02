using Minkin_Lab02.Properties;
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
            FileSystemWatcher watcher;
            switch (type)
            {
                case FileType.Folder:
                    watcher = CreateFolderWather();
                    break;
                default:
                    watcher = CreateFileWather();
                    break;
            }
            watcher.Path = Path;
            watcher.InternalBufferSize = int.MaxValue / 2;
            watcher.IncludeSubdirectories = true;
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);
            watcher.EnableRaisingEvents = true;
        }

        private FileSystemWatcher CreateFileWather()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Filter = Resources.TxtMask;
            watcher.NotifyFilter = NotifyFilters.DirectoryName;
            watcher.Deleted += new FileSystemEventHandler(OnDeleted);
            return watcher;
        }

        private FileSystemWatcher CreateFolderWather()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Filter = "*";
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.CreationTime;
            watcher.Deleted += new FileSystemEventHandler(OnDeletedFolder);
            return watcher;
        }

        private void OnDeletedFolder(object sender, FileSystemEventArgs e)
        {
            BackupMachine backup = new BackupMachine(e.FullPath.Substring(0, e.FullPath.LastIndexOf('\\')));
            try
            {
                backup.BackupWithoutFile();
            }
            catch
            {

            }
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            BackupMachine backup = new BackupMachine(e.FullPath.Substring(0, e.FullPath.LastIndexOf('\\')));
            try
            {
                backup.BackupWithoutFile();
            }
            catch
            {

            }
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            if (!Directory.Exists(e.FullPath))
            {
                BackupMachine backup = new BackupMachine(e.FullPath.Substring(0, e.FullPath.LastIndexOf('\\')));
                try
                {
                    backup.BackupFile(e.FullPath);
                }
                catch
                {

                }
            }
            else
            {
                BackupMachine backup = new BackupMachine(e.FullPath.Substring(0, e.FullPath.LastIndexOf('\\'))); //
                backup.BackupWithoutFile();
            }

        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (!Directory.Exists(e.FullPath))
            {
                BackupMachine backup = new BackupMachine(e.FullPath.Substring(0, e.FullPath.LastIndexOf('\\')));
                backup.BackupFile(e.FullPath);
            }
        }
    }
}
