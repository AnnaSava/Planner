using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Planner
{
    class Command
    {
        public String Pattern { get; }

        public ConsoleAction Execute { get; }

        public Command(String pattern, ConsoleAction action)
        {
            Pattern = pattern;
            Execute = action;
        }
    }

    static class CommandRegistry
    {
        public static List<Command> Commands { get; private set; }

        static CommandRegistry()
        {
            Commands = new List<Command>();
        }

        public static void Add(String pattern, ConsoleAction action)
        {
            Commands.Add(new Command(pattern, action));
        }

        public static void Execute(String command)
        {
            foreach (var cmd in Commands)
            {
                var rx = new Regex(cmd.Pattern);
                if (rx.IsMatch(command))
                {
                    cmd.Execute(command);
                }
            }
        }
    }

    static class Commands
    {
        public const String EXIT = "выход";
        public const String HELP = "помощь";
        public const String GOALS = "цели";
        public const String GOAL = "цель";
        public const String EDIT = "ред";
        public const String DELETE = "удал";
        public const String COPY = "копир";
        public const String CLOSE = "всё";
        public const String CREATE = "+";
        public const String ACHIEVED = "успеш";
        public const String FAILED = "провал";
        public const String STAGES = "этапы";        
    }

    static class Patterns
    {
        public const String HELP = @"^помощь$";
        public const String HELP_GOALS = @"^помощь цели$";
        public const String HELP_GOAL = @"^помощь цель$";

        public const String GOALS = @"^цели$";
        public const String GOALS_CREATE = @"^цели \+$";
        public const String GOALS_ACHIEVED = @"^цели успеш$";
        public const String GOALS_FAILED = @"^цели провал$";

        public const String GOAL_ID = @"^цель \d*$";        
        public const String GOAL_ID_EDIT = @"^цель \d* ред$";
        public const String GOAL_ID_COPY = @"^цель \d* копир$";
        public const String GOAL_ID_CLOSE = @"^цель \d* всё$";
        public const String GOAL_ID_DELETE = @"^цель \d* удал$";

        public const String GOAL_ID_STAGES = @"^цель \d* этапы$";
        public const String GOAL_ID_STAGES_CREATE = @"^цель \d* этапы \+$";
        public const String GOAL_ID_STAGE_ID = @"^цель \d* этап \d*$";
        public const String GOAL_ID_STAGE_ID_EDIT = @"^цель \d* этап \d* ред$";
        public const String GOAL_ID_STAGE_ID_CLOSE = @"^цель \d* этап \d* всё$";
        public const String GOAL_ID_STAGE_ID_OPEN = @"^цель \d* этап \d* снова$";
        public const String GOAL_ID_STAGE_ID_DELETE = @"^цель \d* этап \d* удал$";        
        public const String GOAL_ID_STAGE_ID_COPYTO_GOAL_ID = @"^цель \d* этап \d* копир в цель \d*$";        
        public const String GOAL_ID_STAGE_ID_MOVETO_GOAL_ID = @"^цель \d* этап \d* перенос в цель \d*$";
        public const String GOAL_ID_STAGE_ID_MOVE_UP = @"^цель \d* этап \d* вверх$";
        public const String GOAL_ID_STAGE_ID_MOVE_DOWN = @"^цель \d* этап \d* вниз";
        public const String GOAL_ID_STAGE_ID_MOVE_BEGIN = @"^цель \d* этап \d* в начало$";
        public const String GOAL_ID_STAGE_ID_MOVE_END = @"^цель \d* этап \d* в конец$";

        public const int POS_GOAL_ID = 1;         
        public const int POS_COPY_MOVE_STAGE_GOAL_ID = 7;         
        public const int POS_STAGE_ID = 3;         
    }

    delegate void ConsoleAction(String command);
}
