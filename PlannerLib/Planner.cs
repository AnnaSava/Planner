using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerLib
{
    public class Planner
    {
        List<Goal> goals;

        public Planner()
        {
            goals = new List<Goal>();
        }

        public Planner(bool initialize)
        {
            if (initialize)
            {
                goals = Initializer.Goals;
            }
            else
            {
                goals = new List<Goal>();
            }
        }

        public int AddGoal(String title, String description, int days)
        {
            var goal = new Goal
            {
                Title = title,
                Description = description,
                FinishDate = DateTime.Now.AddDays(days)
            };
            goals.Add(goal);
            return goal.Id;
        }

        public void RemoveGoal(Goal goal)
        {
            if (goal == null) return;
            goals.Remove(goal);
        }

        public void CopyGoal(Goal goal, string newTitle)
        {
            var newGoal = goal.Clone() as Goal;
            newGoal.Title = String.IsNullOrEmpty(newTitle) ? goal.Title : newTitle;
            goals.Add(newGoal);
        }

        public Goal FindGoal(int id)
        {
            return goals.Find(g => g.Id == id);
        }

        public IEnumerable<Goal> GetGoals()
        {
            return goals.Where(g => g.IsClosed == false);
        }

        public IEnumerable<Goal> GetAchievedGoals()
        {
            return goals.Where(g => g.IsClosed && g.IsAchieved.Value);
        }

        public IEnumerable<Goal> GetFailedGoals()
        {
            return goals.Where(g => g.IsClosed && g.IsAchieved.Value == false);
        }
    }
}
