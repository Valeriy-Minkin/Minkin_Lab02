using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Minkin_Lab02
{
    class Program
    {
        static Config config;

        static void Main(string[] args)
        {
            ReadConfig();
            CheckFirstStart();
            CheckKeys(args);
            Console.WriteLine("Press any key for exit");
            Console.ReadKey();
        }

        private static void CheckFirstStart()
        {
            if (!Directory.Exists(config.BackupFolder))
            {
                Backup backup = new Backup();
                backup.Path = config.MonitorableFolders.First();
                backup.BackupAllFolder(config);
            }
        }

        private static void ReadConfig()
        {
            config = new Config();
            if (config.Exist())
            {
                config = Config.ReadFromFile();
            }
            else
            {
                config.WriteToFile();
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
            Console.WriteLine("Watch");
            foreach (string str in config.MonitorableFolders)
            {
                TimeMachine timeMachine = new TimeMachine(str);
            }
        }
    }
}
