using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minkin_Lab02
{
    class Program
    {
        //https://docs.microsoft.com/ru-ru/dotnet/csharp/programming-guide/main-and-command-args/command-line-arguments

        static void Main(string[] args)
        {
            CheckKeys(args);
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
        }
    }
}
