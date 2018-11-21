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
                Console.WriteLine("4. Копировать цель \t 5. Завершить цель \t 6. Редактировать цель");
                Console.WriteLine("7. Удалить цель \t 9. Выйти из программы \t ");
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
                            EditGoal(planner);
                            break;
                        case 7:
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
                        Console.WriteLine("1. Добавить этап \t 2. Показать этапы \t 3. Показать этап");
                        Console.WriteLine("4. Переместить этап \t 5. Редактировать этап \t 6. Удалить этап");
                        Console.WriteLine("7. Завершить этап \t 8. Возобновить этап \t 9. Вернуться");
                        int command = Convert.ToInt32(Console.ReadLine());

                        switch (command)
                        {
                            case 1:
                                AddStage(goal);
                                break;
                            case 2:
                                ShowStages(goal);
                                break;
                            case 3:
                                ShowStage(goal);
                                break;
                            case 4:
                                MoveStage(goal);
                                break;
                            case 5:
                                EditStage(goal);
                                break;
                            case 6:
                                RemoveStage(goal);
                                break;
                            case 7:
                                CloseStage(goal);
                                break;
                            case 8:
                                OpenStage(goal);
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

        static void EditGoal(PlannerLib.Planner planner)
        {
            Console.WriteLine("Введите номер цели");
            var idStr = Console.ReadLine();
            if (Int32.TryParse(idStr, out int id))
            {
                var goal = planner.FindGoal(id);
                if (goal != null)
                {
                    Console.WriteLine("Выберите поле для редактирования:");
                    Console.WriteLine("1. Заголовок");
                    Console.WriteLine("2. Описание");
                    Console.WriteLine("3. Количество дней на выполнение");
                    var resultStr = Console.ReadLine();

                    if (Int32.TryParse(resultStr, out int result))
                    {
                        switch (result)
                        {
                            case 1:
                                Console.WriteLine("Введите новый заголовок");
                                var newTitle = Console.ReadLine();
                                goal.Update(newTitle: newTitle);
                                break;
                            case 2:
                                Console.WriteLine("Введите новое описание");
                                var newDescription = Console.ReadLine();
                                goal.Update(newDescription: newDescription);
                                break;
                            case 3:
                                Console.WriteLine($"Введите количество дней, начиная с {goal.StartDate}");
                                var daysStr = Console.ReadLine();
                                if (Int32.TryParse(daysStr, out int days))
                                {
                                    goal.Update(days: days);
                                }
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Цель с номером {id} не найдена");
                }
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

        static void AddStage(Goal goal)
        {
            Console.WriteLine("Введите название этапа");
            var title = Console.ReadLine();

            Console.WriteLine("Введите описание этапа");
            var description = Console.ReadLine();

            goal.AddStage(title, description);
        }

        static void EditStage(Goal goal)
        {
            Console.WriteLine("Введите номер этапа");
            var numberStr = Console.ReadLine();

            if (Int32.TryParse(numberStr, out int number))
            {
                var stage = goal.FindStage(number);
                if (stage != null)
                {
                    Console.WriteLine("Выберите поле для редактирования:");
                    Console.WriteLine("1. Название");
                    Console.WriteLine("2. Описание");
                    var resultStr = Console.ReadLine();

                    if (Int32.TryParse(resultStr, out int result))
                    {
                        switch (result)
                        {
                            case 1:
                                Console.WriteLine("Введите новое название");
                                var newTitle = Console.ReadLine();
                                stage.Update(newTitle: newTitle);
                                break;
                            case 2:
                                Console.WriteLine("Введите новое описание");
                                var newDescription = Console.ReadLine();
                                stage.Update(newDescription: newDescription);
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Этап {number} не найден");
                }
            }
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

        static void CloseStage(Goal goal)
        {
            Console.WriteLine("Введите номер этапа");
            var numberStr = Console.ReadLine();

            if (Int32.TryParse(numberStr, out int number))
            {
                var stage = goal.FindStage(number);
                if (stage != null)
                {
                    stage.Close();
                }
                else
                {
                    Console.WriteLine($"Этап {number} не найден");
                }
            }
        }

        static void OpenStage(Goal goal)
        {
            Console.WriteLine("Введите номер этапа");
            var numberStr = Console.ReadLine();

            if (Int32.TryParse(numberStr, out int number))
            {
                var stage = goal.FindStage(number);
                if (stage != null)
                {
                    stage.Open();
                }
                else
                {
                    Console.WriteLine($"Этап {number} не найден");
                }
            }
        }

        static void MoveStage(Goal goal)
        {
            Console.WriteLine("Введите номер этапа");
            var numberStr = Console.ReadLine();

            if (Int32.TryParse(numberStr, out int number))
            {
                var stage = goal.FindStage(number);
                if (stage != null)
                {
                    Console.WriteLine("Куда переместить? 1. Шаг вверх 2. Шаг вниз 3. В начало 4. В конец");
                    var resultStr = Console.ReadLine();

                    if (Int32.TryParse(resultStr, out int result))
                    {
                        goal.MoveStage(stage, (MoveType)(--result));
                    }
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

        static void PrintGoalHeader(Goal goal)
        {
            Console.WriteLine($"Цель №{goal.Id} {goal.Title}");
            Console.WriteLine($"Описание: {goal.Description}");
            if (goal.IsClosed)
            {
                Console.Write("ЗАВЕРШЕНА");
                if (goal.IsAchieved.Value)
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
