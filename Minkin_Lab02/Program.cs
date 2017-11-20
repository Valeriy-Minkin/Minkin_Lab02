using System;
using System.Threading;
using Minkin_Lab02.Properties;

namespace Minkin_Lab02
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ReadConfig();
                Timer timer = new Timer(BackgroundCacheWriter, null, 1000, 2000);
                CheckFirstStart();
                CheckKeys(args);
            }
            catch (FileSystemError ex)
            {
                ShowErrors(ex.Message);
            }
            catch(Exception ex)
            {
                ProcessUndefinedError(ex);
            }
            finally
            {
                Console.WriteLine(Resources.PressAnyKey);
                Console.ReadKey();
            }
        }

        private static void ProcessUndefinedError(Exception ex)
        {
            //We can add this information into errorlog later
            Console.WriteLine(Resources.UndefinedError);
        }

        private static void ShowErrors(string message)
        {
            //We can add this information into errorlog later
            Console.WriteLine(message);
        }

        private static void BackgroundCacheWriter(object state)
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.CacheWrite();
        }

        private static void CheckFirstStart()
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.CheckFirstStart();
        }

        private static void ReadConfig()
        {
            ConfigManager configManager = new ConfigManager();
            configManager.ReadConfig();
        }

        private static void CheckKeys(string[] args)
        {
            if (args != null && args.Length != 0)
            {
                switch (args[0])
                {
                    case Constants.ConsoleArgumentWatch:
                        Watch();
                        break;
                    case Constants.ConsoleArgumentReset:
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
            Console.WriteLine(Resources.SelectModeText, Environment.NewLine);
            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    Watch();
                    Console.WriteLine(string.Empty);
                    break;
                case '2':
                    Reset();
                    Console.WriteLine(string.Empty);
                    break;
                default:
                    Console.WriteLine(Resources.WrongMode, Environment.NewLine);
                    break;
            }
        }

        private static void Reset()
        {
            Console.WriteLine(Resources.ResetText);
            DateTime time = DateTime.Now;
            string data = Console.ReadLine();
            RollbackMachine rollbackMachine = new RollbackMachine();
            if (data.Equals(string.Empty))
            {
                rollbackMachine.Rollback(DateTime.Now);
            }
            else
            {
                if (DateTime.TryParse(data, out time))
                {
                    rollbackMachine.Rollback(time);
                }
                else
                {
                    Console.WriteLine(Resources.DateFormatError);
                }
            }
        }

        private static void Watch()
        {
            foreach (string str in Cache.Instance.CurrentConfig.MonitorableFolders)
            {
                TimeMachine timeMachine = new TimeMachine(str);
            }
        }
    }
}
