using Minkin_Lab02.Properties;
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
            bool done = false;
            if (Cache.getInstance().CurrentConfig.Logs.Count != 0)
            {
                if (Cache.getInstance().CurrentConfig.Logs.Count == 1)
                {
                    if (Cache.getInstance().CurrentConfig.Logs.Last().Time > time)
                    {
                        try
                        {
                            RewriteFiles(Cache.getInstance().CurrentConfig.Logs.Last());
                        }
                        catch(Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                        done = true;
                    }
                }
                else
                {
                    if(Cache.getInstance().CurrentConfig.Logs.Last().Time < time && !done)
                    {
                        try
                        {
                            RewriteFiles(Cache.getInstance().CurrentConfig.Logs.Last());
                        }
                        catch(Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                        done = true;
                    }
                    if (Cache.getInstance().CurrentConfig.Logs.First().Time > time && !done)
                    {
                        try
                        {
                            RewriteFiles(Cache.getInstance().CurrentConfig.Logs.First());
                        }
                        catch(Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
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
                        try
                        {
                            RewriteFiles(Cache.getInstance().CurrentConfig.Logs[currentLogNumber]);
                        }
                        catch(Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }
            }
        }


        private void RewriteFiles(Folder folder)
        {
            LogManager logManager = new LogManager();
            CurrentFilesCondition log = new CurrentFilesCondition();
            try
            {
                log = logManager.ReadFromFile(folder.Path);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            FilesManager filesManager = new FilesManager();
            try
            {
                filesManager.ClearFolder();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
                    catch
                    {
                        throw new Exception(Resources.CreateFolderError);
                    }
                }
                try
                {
                    File.Copy(Path.Combine(Cache.getInstance().CurrentConfig.BackupFolder, file.BackupName), Path.Combine(path, file.Name), true);
                }
                catch
                {
                    throw new Exception(Resources.CopyFileError);
                }
            }
        }
    }
}
