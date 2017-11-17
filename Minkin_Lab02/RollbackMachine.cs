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
            if (CurrentData.getInstance().CurrentConfig.Logs.Count != 0)
            {
                if (CurrentData.getInstance().CurrentConfig.Logs.Count == 1)
                {
                    if (CurrentData.getInstance().CurrentConfig.Logs.Last().Time > time)
                    {
                        RewriteFiles(CurrentData.getInstance().CurrentConfig.Logs.Last());
                    }
                }
                else
                {
                    if (CurrentData.getInstance().CurrentConfig.Logs.First().Time > time)
                    {
                        RewriteFiles(CurrentData.getInstance().CurrentConfig.Logs.First());
                    }
                    else
                    {
                        int currentLogNumber = 0;
                        for(int i=1; i< CurrentData.getInstance().CurrentConfig.Logs.Count; i++)
                        {
                            if (CurrentData.getInstance().CurrentConfig.Logs[i].Time.CompareTo(time)<0)
                            {
                                currentLogNumber = i - 1;
                            }
                        }
                        RewriteFiles(CurrentData.getInstance().CurrentConfig.Logs[currentLogNumber]);
                    }
                }
            }
        }

        public void Rollback()
        {
            if (CurrentData.getInstance().CurrentConfig.Logs.Count != 0)
            {
                Folder folder = CurrentData.getInstance().CurrentConfig.Logs.Last();
            }
        }

        private void RewriteFiles(Folder folder)
        {
            LogManager logManager = new LogManager();
            Log log = logManager.ReadFromFile(folder.Path);
            foreach(var currentfolder in CurrentData.getInstance().CurrentConfig.MonitorableFolders)
            {
                IEnumerable<string> list = Directory.EnumerateFiles(currentfolder, "*.txt", SearchOption.AllDirectories);
                foreach(var file in list)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {
                        Console.WriteLine("Can't delete file");
                    }
                }
            }
            Console.WriteLine("");
            foreach(FileData file in log.files)
            {
                File.Copy(Path.Combine(CurrentData.getInstance().CurrentConfig.BackupFolder, file.BackupName), file.Path);
            }
        }
    }
}
