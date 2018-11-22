using PlannerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner
{
    static class ConsoleUI
    {
        static PlannerLib.Planner planner = new PlannerLib.Planner(true);

        public static void SetCommands()
        {
            CommandRegistry.Add(Patterns.HELP, Help.ShowCommands);
            CommandRegistry.Add(Patterns.HELP_GOALS, Help.ShowGoalsCommands);
            CommandRegistry.Add(Patterns.HELP_GOAL, Help.ShowGoalCommands);

            CommandRegistry.Add(Patterns.GOALS, ShowGoals);
            CommandRegistry.Add(Patterns.GOALS_CREATE, CreateGoal);
            CommandRegistry.Add(Patterns.GOALS_ACHIEVED, ShowAchievedGoals);
            CommandRegistry.Add(Patterns.GOALS_FAILED, ShowFailedGoals);

            CommandRegistry.Add(Patterns.GOAL_ID, ShowGoal);            
            CommandRegistry.Add(Patterns.GOAL_ID_EDIT, EditGoal);
            CommandRegistry.Add(Patterns.GOAL_ID_DELETE, RemoveGoal);
            CommandRegistry.Add(Patterns.GOAL_ID_CLOSE, CloseGoal);
            CommandRegistry.Add(Patterns.GOAL_ID_COPY, CopyGoal);
            CommandRegistry.Add(Patterns.GOAL_ID_STAGES, ShowStages);

            CommandRegistry.Add(Patterns.GOAL_ID_STAGES_CREATE, AddStage);
            CommandRegistry.Add(Patterns.GOAL_ID_STAGE_ID, ShowStage);
            CommandRegistry.Add(Patterns.GOAL_ID_STAGE_ID_EDIT, EditStage);
            CommandRegistry.Add(Patterns.GOAL_ID_STAGE_ID_CLOSE, CloseStage);
            CommandRegistry.Add(Patterns.GOAL_ID_STAGE_ID_OPEN, OpenStage);
            CommandRegistry.Add(Patterns.GOAL_ID_STAGE_ID_DELETE, RemoveStage);

            CommandRegistry.Add(Patterns.GOAL_ID_STAGE_ID_COPYTO_GOAL_ID, CopyToStage);
            CommandRegistry.Add(Patterns.GOAL_ID_STAGE_ID_MOVETO_GOAL_ID, MoveToStage);
            CommandRegistry.Add(Patterns.GOAL_ID_STAGE_ID_MOVE_UP, MoveUpStage);
            CommandRegistry.Add(Patterns.GOAL_ID_STAGE_ID_MOVE_DOWN, MoveDownStage);
            CommandRegistry.Add(Patterns.GOAL_ID_STAGE_ID_MOVE_BEGIN, MoveToBeginStage);
            CommandRegistry.Add(Patterns.GOAL_ID_STAGE_ID_MOVE_END, MoveToEndStage);
        }

        #region Goals Actions

        static void ShowGoals(String command)
        {
            PrintGoals(planner.GetGoals());
        }

        static void ShowAchievedGoals(String command)
        {
            PrintGoals(planner.GetAchievedGoals());
        }

        static void ShowFailedGoals(String command)
        {
            PrintGoals(planner.GetFailedGoals());
        }
        
        static void CreateGoal(String command)
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

        static void ShowGoal(String command)
        {
            if (FindGoal(command, out Goal goal))
            {
                PrintGoalHeader(goal);

                foreach (var stage in goal.Stages)
                {
                    PrintStage(stage);
                }
            }
        }

        static void EditGoal(String command)
        {
            if (FindGoal(command, out Goal goal))
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
        }

        static void RemoveGoal(String command)
        {
            if (FindGoal(command, out Goal goal))
            {
                planner.RemoveGoal(goal);
            }
        }

        static void CopyGoal(String command)
        {
            if (FindGoal(command, out Goal goal))
            {
                Console.WriteLine("Введите название для новой цели");
                var title = Console.ReadLine();

                planner.CopyGoal(goal, title);
            }
        }

        static void CloseGoal(String command)
        {
            if (FindGoal(command, out Goal goal))
            {
                Console.WriteLine("Выберите пункт: 1. цель достигнута \t2. цель провалена");
                var resultStr = Console.ReadLine();

                if (Int32.TryParse(resultStr, out int result))
                {
                    goal.Close(result == 1);
                }
            }
        }

        #endregion

        #region Stages Actions

        static void ShowStages(String command)
        {
            if (FindGoal(command, out Goal goal))
            {
                foreach (var stage in goal.Stages)
                {
                    PrintStage(stage);
                }
            }
        }

        static void AddStage(String command)
        {
            if (FindGoal(command, out Goal goal))
            {
                Console.WriteLine("Введите название этапа");
                var title = Console.ReadLine();

                Console.WriteLine("Введите описание этапа");
                var description = Console.ReadLine();

                goal.AddStage(title, description);
            }
        }

        static void ShowStage(String command)
        {
            if (FindStage(command, out Stage stage))
            {
                PrintStage(stage);
            }
        }
        
        static void EditStage(String command)
        {
            if(FindStage(command, out Stage stage))
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
        }

        static void CloseStage(String command)
        {
            if (FindStage(command, out Stage stage))
            {
                stage.Close();
            }
        }

        static void OpenStage(String command)
        {
            if (FindStage(command, out Stage stage))
            {
                stage.Open();   
            }
        }

        static void RemoveStage(String command)
        {
            if (FindStage(command, out Stage stage, out Goal goal))
            {
                goal.RemoveStage(stage);
            }
        }

        static void CopyToStage(String command)
        {
            if (FindStage(command, out Stage stage, out Goal currentGoal))
            {
                if (FindGoal(command, Patterns.POS_COPY_MOVE_STAGE_GOAL_ID, out Goal toGoal))
                {
                    currentGoal.CopyToStage(stage, toGoal);
                }
            }
        }

        static void MoveToStage(String command)
        {
            if (FindStage(command, out Stage stage, out Goal currentGoal))
            {
                if (FindGoal(command, Patterns.POS_COPY_MOVE_STAGE_GOAL_ID, out Goal toGoal))
                {
                    currentGoal.MoveToStage(stage, toGoal);
                }
            }
        }

        static void MoveUpStage(String command)
        {
            if (FindStage(command, out Stage stage, out Goal goal))
            {
                goal.MoveStage(stage, MoveType.StepUp);
            }
        }

        static void MoveDownStage(String command)
        {
            if (FindStage(command, out Stage stage, out Goal goal))
            {
                goal.MoveStage(stage, MoveType.StepDown);
            }
        }

        static void MoveToBeginStage(String command)
        {
            if (FindStage(command, out Stage stage, out Goal goal))
            {
                goal.MoveStage(stage, MoveType.ToBegin);
            }
        }

        static void MoveToEndStage(String command)
        {
            if (FindStage(command, out Stage stage, out Goal goal))
            {
                goal.MoveStage(stage, MoveType.ToEnd);
            }
        }

        #endregion

        #region Print
        static bool FindGoal(String command, out Goal goal)
        {
            return FindGoal(command, Patterns.POS_GOAL_ID, out goal);
        }

        static bool FindGoal(String command, int position, out Goal goal)
        {
            goal = null;
            var commandArr = command.Split(' ');
            var idStr = commandArr[position];

            if (Int32.TryParse(idStr, out int id))
            {
                goal = planner.FindGoal(id);
                if (goal == null)
                {
                    Console.WriteLine($"Цель с номером {id} не найдена");
                    return false;
                }
                return true;
            }
            else
            {
                Console.WriteLine("Некорректный формат Id");
                return false;
            }
        }

        static bool FindStage(String command, out Stage stage)
        {
            return FindStage(command, out stage, out Goal goal);
        }

        static bool FindStage(String command, out Stage stage, out Goal goal)
        {
            goal = null;
            stage = null;
            var commandArr = command.Split(' ');

            if (FindGoal(command, out goal))
            {
                if (Int32.TryParse(commandArr[Patterns.POS_STAGE_ID], out int number))
                {
                    stage = goal.FindStage(number);
                    if (stage == null)
                    {
                        Console.WriteLine($"Этап {number} не найден");
                        return false;
                    }
                    return true;
                }
                else
                {
                    Console.WriteLine("Некорректный формат номера этапа");
                    return false;
                }
            }
            return false;
        }

        static void PrintGoals(IEnumerable<Goal> goals)
        {
            foreach (var goal in goals)
            {
                PrintGoalHeader(goal);
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

        #endregion
    }
}
