using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Minkin_Lab02.Properties;

namespace Minkin_Lab02
{
    class Program
    {
        const string ConsoleArgumentWatch = "watch";
        const string ConsoleArgumentReset = "reset";

        static void Main(string[] args)
        {
            ReadConfig();
            Timer timer = new Timer(BackgroundCacheWriter, null, 1000, 2000);
            CheckFirstStart();
            CheckKeys(args);
            Console.WriteLine(Resources.PressAnyKey);
            Console.ReadKey();
        }

        private static void BackgroundCacheWriter(object state)
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            try
            {
                backgroundWorker.CacheWrite();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void CheckFirstStart()
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            try
            {
                backgroundWorker.CheckFirstStart();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ReadConfig()
        {
            ConfigManager configManager = new ConfigManager();
            try
            {
                configManager.ReadConfig();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                        try
                        {
                            Reset();
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
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
            Console.WriteLine(Resources.SelectModeText, Environment.NewLine);
            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    Watch();
                    break;
                case '2':
                    try
                    {
                        Reset();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                default:
                    Console.WriteLine(Resources.WrongMode, Environment.NewLine);
                    break;
            }
            Console.WriteLine(string.Empty);
        }

        private static void Reset()
        {
            Console.WriteLine(Resources.ResetText);
            DateTime time = DateTime.Now;
            string data = Console.ReadLine();
            RollbackMachine rollbackMachine = new RollbackMachine();
            if (data.Equals(string.Empty))
            {
                try
                {
                    rollbackMachine.Rollback(DateTime.Now);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                if (DateTime.TryParse(data, out time))
                {
                    try
                    {
                        rollbackMachine.Rollback(time);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        private static void Watch()
        {
            foreach (string str in Cache.getInstance().CurrentConfig.MonitorableFolders)
            {
                TimeMachine timeMachine = new TimeMachine(str);
            }
        }
    }
}
