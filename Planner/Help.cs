using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner
{
    static class Help
    {
        public static void ShowCommands()
        {
            Console.WriteLine($"{Commands.GOALS} - показать актуальные цели");
            Console.WriteLine($"{Commands.GOAL} [номер] - показать цель");
            Console.WriteLine($"{Commands.HELP} - список команд");
            Console.WriteLine($"{Commands.EXIT} - выход из программы");
            Console.WriteLine();
        }

        public static void ShowGoalsCommands()
        {
            Console.WriteLine($"{Commands.CREATE} - добавить цель");
            Console.WriteLine($"{Commands.CREATE} [заголовок] [срок] - добавить цель сразу");
            Console.WriteLine($"{Commands.ACHIEVED} - достигнутые цели");
            Console.WriteLine($"{Commands.FAILED} - проваленные цели");
            Console.WriteLine();
        }

        public static void ShowGoalCommands()
        {
            Console.WriteLine($"{Commands.EDIT} - редактировать цель");
            Console.WriteLine($"{Commands.DELETE} - удалить цель");
            Console.WriteLine($"{Commands.COPY} - копировать цель");
            Console.WriteLine($"{Commands.CLOSE} - завершить цель");
            Console.WriteLine($"{Commands.STAGES} - этапы");
            Console.WriteLine($"{Commands.STAGES} {Commands.CREATE} - добавить этап");
            Console.WriteLine();
        }

    }
}
