using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minkin_Lab02
{
    class Program
    {
        //https://docs.microsoft.com/ru-ru/dotnet/csharp/programming-guide/main-and-command-args/command-line-arguments

        static List<string> listOfFolder = new List<string>(new string[]{@"C:\Users\ray-s_000\Documents\Lab_01\Task10\TestFolder"});

        static void Main(string[] args)
        {
            CheckKeys(args);
            Console.WriteLine("Press any key for exit");
            Console.ReadKey();
        }

        private static void CheckKeys(string[] args)
        {
            if(args!=null && args.Length!=0)
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
                    Restart();
                    break;
            }
        }

        private static void Restart()
        {
            Console.Write("Try again? (y/n) ");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.KeyChar == 'y' || key.KeyChar == 'Y')
            {
                Console.WriteLine("");
                AskUser();
            }
        }

        private static void Reset()
        {
            Console.WriteLine("Reset");
        }

        private static void Watch()
        {
            Console.WriteLine("Watch");
            foreach (string str in listOfFolder)
            {
                TimeMachine timeMachine = new TimeMachine(str);
            }
        }
    }

    class TimeMachine
    {
        public string Path { get; private set; }
        private Dictionary<string, DateTime> FilesLog { get; set; }

        public TimeMachine(string path)
        {
            Path = path;
            FilesLog = new Dictionary<string, DateTime>();
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = Path;
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "*.txt";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);
            watcher.EnableRaisingEvents = true;
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (!FilesLog.ContainsKey(e.FullPath) || (FilesLog.ContainsKey(e.FullPath) && FilesLog[e.FullPath] != DateTime.Now))
            {
                Console.WriteLine("File: {0} is {1}", e.FullPath, e.ChangeType);
                FilesLog.Add(e.FullPath, DateTime.Now);
            }
        }
    }
}
