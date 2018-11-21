using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlannerLib;

namespace Planner
{
    class Program
    {
        static void Main(string[] args)
        {
            var planner = new PlannerLib.Planner(true);            
            bool alive = true;
            while (alive)
            {
                Console.WriteLine("1. Создать цель \t 2. Показать цели \t 3. Показать цель");
                Console.WriteLine("4. Копировать цель \t 5. Завершить цель \t 6. Удалить цель");
                Console.WriteLine("9. Выйти из программы \t ");
                Console.WriteLine("Введите номер пункта:");
                try
                {
                    int command = Convert.ToInt32(Console.ReadLine());

                    switch (command)
                    {
                        case 1:
                            CreateGoal(planner);
                            break;
                        case 2:
                            ShowGoals(planner);
                            break;
                        case 3:
                            ShowGoal(planner);
                            break;
                        case 4:
                            CopyGoal(planner);
                            break;
                        case 5:
                            CloseGoal(planner);
                            break;
                        case 6:
                            RemoveGoal(planner);
                            break;
                        case 9:
                            alive = false;
                            continue;
                    }

                }
                catch (Exception ex) { }
            }
        }

        static void CreateGoal(PlannerLib.Planner planner)
        {
            Console.WriteLine("Введите заголовок цели");
            var title = Console.ReadLine();

            Console.WriteLine("Введите описание цели");
            var description = Console.ReadLine();

            Console.WriteLine("Через сколько дней цель должна быть достигнута?");
            var daysStr = Console.ReadLine();

            if (Int32.TryParse(daysStr, out int days) == false)
            {
                days = 30;
            }

            var id = planner.AddGoal(title, description, days);

            Console.WriteLine($"Цель создана! Id = {id}");
        }

        static void ShowGoals(PlannerLib.Planner planner)
        {
            var goals = planner.GetGoals();
            foreach (var goal in goals)
            {
                PrintGoalHeader(goal);
            }
        }

        static void ShowGoal(PlannerLib.Planner planner)
        {
            Console.WriteLine("Введите номер цели");
            var idStr = Console.ReadLine();

            if (Int32.TryParse(idStr, out int id))
            {
                var goal = planner.FindGoal(id);
                if (goal == null)
                {
                    Console.WriteLine($"Цель с номером {id} не найдена");
                }
                else
                {
                    PrintGoalHeader(goal);

                    foreach (var stage in goal.Stages)
                    {
                        PrintStage(stage);
                    }

                    bool alive = true;
                    while (alive)
                    {
                        Console.WriteLine("1. Добавить этап \t 2. Показать этапы \t 3. Добавить чеклист");
                        Console.WriteLine("4. Удалить этап \t ");
                        Console.WriteLine("9. Вернуться");
                        int command = Convert.ToInt32(Console.ReadLine());

                        switch (command)
                        {
                            case 1: AddStage(goal);
                                break;
                            case 2: ShowStages(goal);
                                break;
                            case 3: AddCheckList(goal);
                                break;
                            case 4: RemoveStage(goal);
                                break;
                            case 9:
                                alive = false;
                                continue;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод");
            }
        }

        static void RemoveGoal(PlannerLib.Planner planner)
        {
            Console.WriteLine("Введите номер цели");
            var idStr = Console.ReadLine();
            if (Int32.TryParse(idStr, out int id))
            {
                var goal = planner.FindGoal(id);
                if (goal != null)
                {
                    planner.RemoveGoal(goal);
                }
                else
                {
                    Console.WriteLine($"Цель с номером {id} не найдена");
                }
            }
        }

        static void CopyGoal(PlannerLib.Planner planner)
        {
            Console.WriteLine("Введите номер цели");
            var idStr = Console.ReadLine();
            if (Int32.TryParse(idStr, out int id))
            {
                var goal = planner.FindGoal(id);
                if (goal != null)
                {
                    Console.WriteLine("Введите название для новой цели");
                    var title = Console.ReadLine();

                    planner.CopyGoal(goal, title);
                }
                else
                {
                    Console.WriteLine($"Цель с номером {id} не найдена");
                }
            }
        }

        static void CloseGoal(PlannerLib.Planner planner)
        {
            Console.WriteLine("Введите номер цели");
            var idStr = Console.ReadLine();
            if (Int32.TryParse(idStr, out int id))
            {
                var goal = planner.FindGoal(id);
                if (goal != null)
                {
                    Console.WriteLine("Выберите пункт: 1. цель достигнута \t2. цель провалена");
                    var resultStr = Console.ReadLine();

                    if (Int32.TryParse(resultStr, out int result))
                    {
                        goal.Close(result == 1);
                    }
                }
                else
                {
                    Console.WriteLine($"Цель с номером {id} не найдена");
                }
            }
        }

        static void AddStage(Goal goal)
        {
            Console.WriteLine("Введите название этапа");
            var title = Console.ReadLine();

            Console.WriteLine("Введите описание этапа");
            var description = Console.ReadLine();

            goal.AddStage(title, description);
        }

        static void RemoveStage(Goal goal)
        {
            Console.WriteLine("Введите номер этапа");
            var numberStr = Console.ReadLine();

            if (Int32.TryParse(numberStr, out int number))
            {
                var stage = goal.FindStage(number);
                if (stage != null)
                {
                    goal.RemoveStage(stage);
                }
                else
                {
                    Console.WriteLine($"Этап {number} не найден");
                }
            }
        }

        static void ShowStages(Goal goal)
        {
            foreach (var stage in goal.Stages)
            {
                PrintStage(stage);
            }
        }

        static void AddCheckList(Goal goal)
        {
            Console.WriteLine("Введите номер этапа");
            var numberStr = Console.ReadLine();

            if (Int32.TryParse(numberStr, out int number))
            {
                var stage = goal.FindStage(number);

                if (stage == null)
                {
                    Console.WriteLine($"Этап {number} цели №{goal.Id} не найден");
                }
                else
                {
                    Console.WriteLine("Введите пункты списка через запятую");
                    var itemsStr = Console.ReadLine();

                    var items = itemsStr.Split(',').Select(i => i.Trim());

                    stage.AddCheckList(items);
                }
            }
        }

        static void PrintGoalHeader(Goal goal)
        {
            Console.WriteLine($"Цель №{goal.Id} {goal.Title}");
            Console.WriteLine($"Описание: {goal.Description}");
            if (goal.IsClosed)
            {
                Console.Write("ЗАВЕРШЕНА");
                if(goal.IsAchieved.Value)
                {
                    Console.WriteLine(" ДОСТИГНУТА");
                }
                else
                {
                    Console.WriteLine(" ПРОВАЛЕНА");
                }
            }
            else
            {
                Console.WriteLine($"С {goal.StartDate} по {goal.FinishDate}\n");
            }
        }

        static void PrintStage(Stage stage)
        {
            Console.WriteLine($"Этап {stage.Number}. {stage.Title}");
            Console.WriteLine($"\tОписание: {stage.Description}");

            foreach (var checkPoint in stage.CheckList)
            {
                Console.WriteLine($"\t\t{checkPoint.Text}");                
            }
        }
        
    }
}
