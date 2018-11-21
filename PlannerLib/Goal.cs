using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerLib
{
    public class Goal : ICloneable
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

        public object Clone()
        {
            var goal = new Goal
            {
                Title = this.Title,
                Description = this.Description,
                StartDate = DateTime.Now
            };
            goal.FinishDate = goal.StartDate.AddDays((this.FinishDate - this.StartDate).TotalDays);
            goal.Stages.AddRange(this.Stages.Select(s => s.Clone() as Stage));
            return goal;
        }

        public void AddStage(String title, String description)
        {
            var stage = new Stage
            {
                Number = Stages.Count + 1,
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

    public class Stage : ICloneable
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

        public object Clone()
        {
            var stage = new Stage
            {
                Number = this.Number,
                Title = this.Title,
                Description = this.Description,
                IsDone = this.IsDone,
            };

            stage.CheckList.AddRange(this.CheckList.Select(i => i.Clone() as CheckPoint));
            return stage;
        }
    }

    public class CheckPoint : ICloneable
    {
        public String Text { get; set; }

        public bool IsDone { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
