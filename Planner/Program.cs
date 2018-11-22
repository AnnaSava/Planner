using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PlannerLib;

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

        static void ShowStage(Goal goal)
        {
            Console.WriteLine("Введите номер этапа");
            var numberStr = Console.ReadLine();

            if (Int32.TryParse(numberStr, out int number))
            {
                var stage = goal.FindStage(number);
                if (stage != null)
                {
                    PrintStage(stage);

                    bool alive = true;
                    while (alive)
                    {
                        Console.WriteLine("1. Добавить пункты \t 2. Переместить пункт  \t 3. Удалить пункт");
                        Console.WriteLine("4. Завершить пункт \t 5. Возобновить пункт  \t ");
                        Console.WriteLine("9. Вернуться");
                        int command = Convert.ToInt32(Console.ReadLine());

                        switch (command)
                        {
                            case 1:
                                AddCheckList(stage);
                                break;
                            case 2: MoveCheckPoint(stage);
                                break;
                            case 3: RemoveCheckPoint(stage);
                                break;
                            case 4: CloseCheckPoint(stage);
                                break;
                            case 5: OpenCheckPoint(stage);
                                break;
                            case 9:
                                alive = false;
                                continue;
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Этап {number} не найден");
                }
            }
        }

        static void AddCheckList(Stage stage)
        {
            Console.WriteLine("Введите пункты списка через запятую");
            var itemsStr = Console.ReadLine();

            var items = itemsStr.Split(',').Select(i => i.Trim());

            stage.AddCheckList(items);
        }

        static void MoveCheckPoint(Stage stage)
        {
            Console.WriteLine("Введите номер пункта");
            var numberStr = Console.ReadLine();

            if (Int32.TryParse(numberStr, out int number))
            {
                if (number > stage.CheckList.Count || number < 1)
                {
                    Console.WriteLine($"Пункт {number} не найден");
                    return;
                }

                var checkPoint = stage.CheckList[number - 1];

                Console.WriteLine("Куда переместить? 1. Шаг вверх 2. Шаг вниз 3. В начало 4. В конец");
                var resultStr = Console.ReadLine();

                if (Int32.TryParse(resultStr, out int result))
                {
                    stage.MoveCheckPoint(checkPoint, (MoveType)(--result));
                }
            }
        }

        static void RemoveCheckPoint(Stage stage)
        {
            Console.WriteLine("Введите номер пункта");
            var numberStr = Console.ReadLine();

            if (Int32.TryParse(numberStr, out int number))
            {
                if (number > stage.CheckList.Count || number < 1)
                {
                    Console.WriteLine($"Пункт {number} не найден");
                    return;
                }

                var checkPoint = stage.CheckList[number - 1];
                stage.RemoveCheckPoint(checkPoint);
            }
        }

        static void CloseCheckPoint(Stage stage)
        {
            Console.WriteLine("Введите номер пункта");
            var numberStr = Console.ReadLine();

            if (Int32.TryParse(numberStr, out int number))
            {
                if (number > stage.CheckList.Count || number < 1)
                {
                    Console.WriteLine($"Пункт {number} не найден");
                    return;
                }

                var checkPoint = stage.CheckList[number - 1];
                checkPoint.Close();
            }
        }

        static void OpenCheckPoint(Stage stage)
        {
            Console.WriteLine("Введите номер пункта");
            var numberStr = Console.ReadLine();

            if (Int32.TryParse(numberStr, out int number))
            {
                if (number > stage.CheckList.Count || number < 1)
                {
                    Console.WriteLine($"Пункт {number} не найден");
                    return;
                }

                var checkPoint = stage.CheckList[number - 1];
                checkPoint.Open();
            }
        }

        static void PrintStage(Stage stage)
        {
            var doneChar = stage.IsDone ? '+' : '-';

            Console.WriteLine($"[{doneChar}] Этап {stage.Number}. {stage.Title}");
            Console.WriteLine($"\tОписание: {stage.Description}");

            for (int i = 0; i < stage.CheckList.Count; i++)
            {
                var checkPoint = stage.CheckList[i];
                doneChar = checkPoint.IsDone ? '+' : '-';

                Console.WriteLine($"\t\t[{doneChar}] {i + 1}. {checkPoint.Text}");
            }
        }
    }
}
