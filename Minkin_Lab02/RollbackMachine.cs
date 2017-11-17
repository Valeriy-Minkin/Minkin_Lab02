using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Console.WriteLine(time);
            Console.WriteLine("");
            bool done = false;
            if (Cache.getInstance().CurrentConfig.Logs.Count != 0)
            {
                if (Cache.getInstance().CurrentConfig.Logs.Count == 1)
                {
                    if (Cache.getInstance().CurrentConfig.Logs.Last().Time > time)
                    {
                        RewriteFiles(Cache.getInstance().CurrentConfig.Logs.Last());
                        done = true;
                    }
                }
                else
                {
                    if(Cache.getInstance().CurrentConfig.Logs.Last().Time < time && !done)
                    {
                        RewriteFiles(Cache.getInstance().CurrentConfig.Logs.Last());
                        done = true;
                    }
                    if (Cache.getInstance().CurrentConfig.Logs.First().Time > time && !done)
                    {
                        RewriteFiles(Cache.getInstance().CurrentConfig.Logs.First());
                        done = true;
                    }
                    if(!done)
                    {
                        int currentLogNumber = 0;
                        for(int i=1; i< Cache.getInstance().CurrentConfig.Logs.Count; i++)
                        {
                            if (Cache.getInstance().CurrentConfig.Logs[i].Time.CompareTo(time)<0)
                            {
                                currentLogNumber = i - 1;
                            }
                        }
                        RewriteFiles(Cache.getInstance().CurrentConfig.Logs[currentLogNumber]);
                    }
                }
            }
        }

        public void Rollback()
        {
            if (Cache.getInstance().CurrentConfig.Logs.Count != 0)
            {
                Folder folder = Cache.getInstance().CurrentConfig.Logs.Last();
            }
        }

        private void RewriteFiles(Folder folder)
        {
            LogManager logManager = new LogManager();
            CurrentFilesCondition log = logManager.ReadFromFile(folder.Path);
            FilesManager filesManager = new FilesManager();
            filesManager.ClearFolder();
            Console.WriteLine("");
            //for(int i = 0;i<log.files.Count; i++)
            //{
            //    FileInfo info = new FileInfo(Path.Combine(CurrentData.getInstance().CurrentConfig.BackupFolder, log.files[i].BackupName));
            //    info.CopyTo(log.files[i].Path);
            //   // File.Copy(Path.Combine(CurrentData.getInstance().CurrentConfig.BackupFolder, log.files[i].BackupName), log.files[i].Path, true);
            //}
            string path;
            Dictionary<string, bool> str = new Dictionary<string, bool>();
            foreach (FileData file in log.files)
            {
                path = file.Path.Substring(0, file.Path.LastIndexOf('\\'));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                File.Copy(Path.Combine(Cache.getInstance().CurrentConfig.BackupFolder, file.BackupName), Path.Combine(path, file.Name), true);
                str.Add(Path.Combine(Cache.getInstance().CurrentConfig.BackupFolder, file.BackupName), File.Exists(Path.Combine(Cache.getInstance().CurrentConfig.BackupFolder, file.BackupName)));
            }
            Console.WriteLine("");
        }
    }
}
