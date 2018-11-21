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
            goals = Initializer.Goals;
        }

        public int AddGoal(String title, String description, int days)
        {
            var goal = new Goal
            {
                Title = title,
                Description = description,
                StartDate = DateTime.Now,
                FinishDate = DateTime.Now.AddDays(days)
            };
            goals.Add(goal);
            return goal.Id;
        }

        public Goal FindGoal(int id)
        {
            return goals.Find(g => g.Id == id);
        }

        public List<Goal> GetGoals()
        {
            return goals;
        }
    }
}
