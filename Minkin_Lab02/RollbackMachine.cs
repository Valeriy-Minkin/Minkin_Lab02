using Minkin_Lab02.Properties;
using System;
using System.IO;
using System.Linq;

namespace Minkin_Lab02
{
    public class RollbackMachine
    {
        private DateTime rollbacktime;

        public RollbackMachine()
        {
            rollbacktime = DateTime.Now;
        }

        public RollbackMachine(DateTime time)
        {
            rollbacktime = time;
        }

        public void Rollback(DateTime time)
        {
            bool done = false;
            if (Cache.Instance.CurrentConfig.Logs.Count != 0)
            {
                if (Cache.Instance.CurrentConfig.Logs.Count == 1)
                {
                    if (Cache.Instance.CurrentConfig.Logs.Last().Time > time)
                    {
                        RewriteFiles(Cache.Instance.CurrentConfig.Logs.Last());
                        done = true;
                    }
                }
                else
                {
                    if (Cache.Instance.CurrentConfig.Logs.Last().Time < time && !done)
                    {
                        RewriteFiles(Cache.Instance.CurrentConfig.Logs.Last());
                        done = true;
                    }
                    if (Cache.Instance.CurrentConfig.Logs.First().Time > time && !done)
                    {

                        RewriteFiles(Cache.Instance.CurrentConfig.Logs.First());
                        done = true;
                    }
                    if (!done)
                    {
                        int currentLogNumber = 0;
                        for (int i = 1; i < Cache.Instance.CurrentConfig.Logs.Count; i++)
                        {
                            if (Cache.Instance.CurrentConfig.Logs[i].Time.CompareTo(time) < 0)
                            {
                                currentLogNumber = i - 1;
                            }
                        }
                        RewriteFiles(Cache.Instance.CurrentConfig.Logs[currentLogNumber]);

                    }
                }
            }
        }


        private void RewriteFiles(Folder folder)
        {
            LogManager logManager = new LogManager();
            CurrentFilesCondition log = new CurrentFilesCondition();
            log = logManager.ReadFromFile(folder.Path);
            FilesManager filesManager = new FilesManager();
            filesManager.ClearFolder();
            string path;
            foreach (FileData file in log.Files)
            {
                path = file.Path.Substring(0, file.Path.LastIndexOf('\\'));
                if (!Directory.Exists(path))
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                    }
                    catch (Exception ex)
                    {
                        throw new FileSystemError(string.Format(Resources.CreateFolderError, path), ex);
                    }
                }
                try
                {
                    File.Copy(Path.Combine(Cache.Instance.CurrentConfig.BackupFolder, file.BackupName), Path.Combine(path, file.Name), true);
                }
                catch (Exception ex)
                {
                    throw new FileSystemError(string.Format(Resources.CopyFileError, file.Name), ex);
                }
            }
        }
    }
}
