using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Planner
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleUI.SetCommands();

            bool alive = true;
            while (alive)
            {
                Console.Write("Введите команду: ");

                try
                {
                    var command = Console.ReadLine();

                    if (command == Commands.EXIT)
                    {
                        alive = false;
                        continue;
                    }

                    CommandRegistry.Execute(command);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
