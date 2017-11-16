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
        static void Main(string[] args)
        {
            ReadConfig();
            Timer timer = new Timer(RewriteConfig, null, 1000, 10000);
            CheckFirstStart();
            CheckKeys(args);
            Console.WriteLine("Press any key for exit");
            Console.ReadKey();
        }

        private static void RewriteConfig(object state)
        {
            ConfigManager configManager = new ConfigManager();
            configManager.WriteToFile(CurrentData.getInstance().CurrentConfig);
        }

        private static void CheckFirstStart()
        {
            if (!Directory.Exists(CurrentData.getInstance().CurrentConfig.LogsOfBackupFolder)) //config.LogsOfBackupFolder))
            { 
                Backup backup = new Backup();
                backup.Path = CurrentData.getInstance().CurrentConfig.MonitorableFolders.First();
                backup.BackupAllFolder();
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
                CurrentData.getInstance().CurrentLog = logManager.ReadFromFile(CurrentData.getInstance().CurrentConfig.Logs.Last().Path);
                //Dictionary<string, int> dick = new Dictionary<string, int>();
                //foreach(var temp in CurrentData.getInstance().CurrentLog.files)
                //{
                //    if (dick.ContainsKey(temp.BackupName))
                //    {
                //        dick[temp.BackupName]++;
                //    }
                //    else
                //    {
                //        dick.Add(temp.BackupName, 0);
                //    }
                //}
                //int dick2 = dick.Count(x => x.Value > 0);
                //Console.WriteLine();
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
                    case "watch":
                        Watch();
                        break;
                    case "reset":
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
