using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Minkin_Lab02
{
    class Program
    {
        const string ConsoleArgumentWatch = "watch";
        const string ConsoleArgumentReset = "reset";

        static void Main(string[] args)
        {
            ReadConfig();
            Timer timer = new Timer(RewriteConfig, null, 1000, 2000);
            CheckFirstStart();
            CheckKeys(args);
            Console.WriteLine("Press any key for exit");
            Console.ReadKey();
        }

        private static void RewriteConfig(object state)
        {
            if (CurrentData.getInstance().ChangedFiles.files!=null && CurrentData.getInstance().ChangedFiles.files.Count != 0)
            {
                ConfigManager configManager = new ConfigManager();
                configManager.WriteToFile(CurrentData.getInstance().CurrentConfig);
                Backup backup = new Backup();
                backup.BackupAllFolder(CurrentData.getInstance().ChangedFiles);
                CurrentData.getInstance().ChangedFiles.files = new List<FileData>();
            }
        }

        private static void CheckFirstStart()
        {
            if (!Directory.Exists(CurrentData.getInstance().CurrentConfig.LogsOfBackupFolder))
            { 
                Backup backup = new Backup();
                backup.Path = CurrentData.getInstance().CurrentConfig.MonitorableFolders.First();
                IEnumerable<string> list = Directory.EnumerateFiles(backup.Path, "*", SearchOption.AllDirectories);
                Log log = new Log(list);
                backup.BackupAllFolder(log);
            }
        }

        private static void ReadConfig()
        {
            ConfigManager configManager = new ConfigManager();
            CurrentData.getInstance().CurrentConfig = new Config();
            if (configManager.Exist())
            {
                CurrentData.getInstance().CurrentConfig = configManager.ReadFromFile();
                LogManager logManager = new LogManager();
                if (CurrentData.getInstance().CurrentConfig.Logs.Count != 0)
                {
                    CurrentData.getInstance().CurrentLog = logManager.ReadFromFile(CurrentData.getInstance().CurrentConfig.Logs.Last().Path);
                }
                else
                {
                    CurrentData.getInstance().CurrentLog = new Log();
                }
            }
            else
            {
                configManager.WriteToFile(CurrentData.getInstance().CurrentConfig);
            }
        }

        private static void CheckKeys(string[] args)
        {
            if (args != null && args.Length != 0)
            {
                switch (args[0])
                {
                    case ConsoleArgumentWatch:
                        Watch();
                        break;
                    case ConsoleArgumentReset:
                        Reset();
                        break;
                    default:
                        AskUser();
                        break;
                }
            }
            else
            {
                AskUser();
            }
        }

        private static void AskUser()
        {
            Console.WriteLine("Select mode:{0}   1:Watch {0}   2:Reset", Environment.NewLine);
            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    Watch();
                    break;
                case '2':
                    Reset();
                    break;
                default:
                    Console.WriteLine("{0}Wrong mode!", Environment.NewLine);
                    break;
            }
        }

        private static void Reset()
        {
            Console.WriteLine("Reset");
        }

        private static void Watch()
        {
            foreach (string str in CurrentData.getInstance().CurrentConfig.MonitorableFolders)
            {
                TimeMachine timeMachine = new TimeMachine(str);
            }
        }
    }
}
