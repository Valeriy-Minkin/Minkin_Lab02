using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Minkin_Lab02
{
    public class BackupMachine
    {
        public string Path { get; set; }

        public BackupMachine(string path = "\\")
        {
            Path = path;
        }

        public void BackupFilesFromFolder(CurrentFilesCondition changedfiles)
        {
            LogManager logManager = new LogManager();
            try
            {
                string fullfilename = logManager.WriteToFile(Cache.getInstance().CurrentLog, Cache.getInstance().CurrentConfig.FolderForLogs);
                Folder folder = new Folder() { Path = fullfilename, Time = DateTime.Now };
                Cache.getInstance().CurrentConfig.Logs.Add(folder);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            FilesManager filesManager = new FilesManager();
            try
            {
                filesManager.WriteFileList(changedfiles, Cache.getInstance().CurrentConfig.BackupFolder);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public void BackupFile(string path)
        {
            FileData data = new FileData() { Path = path, Name = path.Substring(path.LastIndexOf('\\') + 1), BackupName = path.Substring(path.LastIndexOf('\\') + 1) + DateTime.Now.Ticks.ToString() + Cache.getInstance().GetCount().ToString(), DateOfChange = DateTime.Now };
            Cache.getInstance().CurrentLog.Files.RemoveAll(x => x.Path == data.Path);
            Cache.getInstance().CurrentLog.Files.Add(data);
            Cache.getInstance().ChangedFiles.Files.RemoveAll(x => x.Path == data.Path);
            Cache.getInstance().ChangedFiles.Files.Add(data);
        }

        public void BackupWithoutFile()
        {
            Cache.getInstance().CurrentLog.Files.RemoveAll(x => x.Path == Path);
            LogManager logManager = new LogManager();
            try
            {
                string fullfilename = logManager.WriteToFile(Cache.getInstance().CurrentLog, Cache.getInstance().CurrentConfig.FolderForLogs);
                Folder folder = new Folder() { Path = fullfilename, Time = DateTime.Now };
                Cache.getInstance().CurrentConfig.Logs.Add(folder);
                Cache.getInstance().HasChanges = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
