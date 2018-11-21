using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerLib
{
    public class Goal : ICloneable
    {
        public int Id { get; private set; }

        static int idCounter = 0;

        public String Title { get; internal set; }

        public String Description { get; internal set; }

        public DateTime StartDate { get; private set; }

        public DateTime FinishDate { get; internal set; }

        public bool IsClosed { get; private set; }

        public bool? IsAchieved { get; private set; }

        public List<Stage> Stages { get; internal set; }

        public Goal()
        {
            idCounter++;
            Id = idCounter;
            StartDate = DateTime.Now;
            Stages = new List<Stage>();
        }

        public object Clone()
        {
            var goal = new Goal
            {
                Title = this.Title,
                Description = this.Description
            };
            goal.FinishDate = goal.StartDate.AddDays((this.FinishDate - this.StartDate).TotalDays);
            goal.Stages.AddRange(this.Stages.Select(s => s.Clone() as Stage));
            return goal;
        }

        public void Update(String newTitle = null, String newDescription = null,
            int days = 0)
        {
            if (newTitle != null) Title = newTitle;
            if (newDescription != null) Description = newDescription;
            if (days != 0)
            {
                var date = StartDate.AddDays(days);
                if (date > DateTime.Now)
                {
                    FinishDate = date;
                }
                else
                {
                    throw new Exception("Дата окончания не может быть раньше текущей даты");
                }
            }
        }

        public void Close(bool isAchieved)
        {
            this.IsClosed = true;
            this.IsAchieved = isAchieved;
            this.FinishDate = DateTime.Now;
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

        public void RemoveStage(Stage stage)
        {
            Stages.Remove(stage);
            reNumberStages();
        }

        public void MoveStage(Stage stage, MoveType move)
        {
            Stages.MoveElement(stage, move);
            reNumberStages();
        }

        private void reNumberStages()
        {
            for (int i = 0; i < this.Stages.Count; i++)
            {
                this.Stages[i].Number = i + 1;
            }
        }

        public Stage FindStage(int number)
        {
            return Stages.Find(s => s.Number == number);
        }
    }

    public class Stage : ICloneable
    {
        public int Number { get; internal set; }

        public String Title { get; internal set; }

        public String Description { get; internal set; }

        public bool IsDone { get; internal set; }

        public List<CheckPoint> CheckList { get; internal set; }

        public Stage()
        {
            CheckList = new List<CheckPoint>();
        }

        public void Update(String newTitle = null, String newDescription = null)
        {
            if (newTitle != null) Title = newTitle;
            if (newDescription != null) Description = newDescription;
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

        public void AddCheckList(IEnumerable<string> items)
        {
            CheckList.AddRange(items.Select(i => new CheckPoint { Text = i }));
        }

        public void MoveCheckPoint(CheckPoint checkPoint, MoveType move)
        {
            CheckList.MoveElement(checkPoint, move);
        }

        public void RemoveCheckPoint(CheckPoint checkPoint)
        {
            CheckList.Remove(checkPoint);
        }

        public void Close()
        {
            IsDone = true;
        }

        public void Open()
        {
            IsDone = false;
        }
    }

    public class CheckPoint : ICloneable
    {
        public String Text { get; internal set; }

        public bool IsDone { get; private set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void Close()
        {
            IsDone = true;
        }

        public void Open()
        {
            IsDone = false;
        }
    }


}
