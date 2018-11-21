using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerLib
{
    public class Goal
    {
        public int Id { get; set; }

        static int idCounter = 0;

        public String Title { get; set; }

        public String Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        public List<Stage> Stages { get; set; }

        public Goal()
        {
            idCounter++;
            Id = idCounter;
            Stages = new List<Stage>();
        }

        public void AddStage(String title, String description)
        {
            var stage = new Stage
            {
                Number= Stages.Count + 1,
                Title = title,
                Description = description
            };

            Stages.Add(stage);
        }

        public Stage FindStage(int number)
        {
            return Stages.Find(s => s.Number == number);
        }
    }

    public class Stage
    {
        public int Number { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        public bool IsDone { get; set; }

        public List<CheckPoint> CheckList { get; set; }

        public Stage()
        {
            CheckList = new List<CheckPoint>();
        }

        public void AddCheckList(IEnumerable<string> items)
        {
            CheckList.AddRange(items.Select(i => new CheckPoint { Text = i }));
        }
    }

    public class CheckPoint
    {
        public String Text { get; set; }

        public bool IsDone { get; set; }
    }
}
