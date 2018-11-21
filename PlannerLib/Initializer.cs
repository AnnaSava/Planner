using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerLib
{
    internal class Initializer
    {
        static internal List<Goal> Goals
        {
            get
            {
                var goals = new List<Goal>
                {
                    goal1(),
                    goal2()
                };                

                return goals;
            }
        }

        static Goal goal1()
        {
            var goal = new Goal
            {
                Title = "Шагомер",
                Description = "Пройти 30000 шагов за неделю",
                StartDate = DateTime.Now,
                FinishDate = DateTime.Now.AddDays(7)
            };

            var stage = new Stage
            {
                Number = 1,
                Title = "Пн - 5000",
                Description = ""
            };
            goal.Stages.Add(stage);
            stage = new Stage
            {
                Number = 2,
                Title = "Вт - 5000",
                Description = ""
            };
            goal.Stages.Add(stage);
            stage = new Stage
            {
                Number = 3,
                Title = "Ср - 5000",
                Description = ""
            };
            goal.Stages.Add(stage);
            stage = new Stage
            {
                Number = 4,
                Title = "Чт - 5000",
                Description = ""
            };
            goal.Stages.Add(stage);
            stage = new Stage
            {
                Number = 5,
                Title = "Пт - 5000",
                Description = ""
            };
            goal.Stages.Add(stage);
            stage = new Stage
            {
                Number = 6,
                Title = "Сб - 2500",
                Description = ""
            };
            goal.Stages.Add(stage);
            stage = new Stage
            {
                Number = 7,
                Title = "Вс - 2500",
                Description = ""
            };
            goal.Stages.Add(stage);
            return goal;
        }

        static Goal goal2()
        {
            var goal = new Goal
            {
                Title = "Испанский",
                Description = "Пройти три темы",
                StartDate = DateTime.Now,
                FinishDate = DateTime.Now.AddDays(7)
            };

            var stage = new Stage
            {
                Number = 1,
                Title = "Моя семья",
                Description = "Два текста и новые слова"
            };
            stage.CheckList.Add(new CheckPoint { Text = "Мамина помада" });
            stage.CheckList.Add(new CheckPoint { Text = "Папины трусы" });
            goal.Stages.Add(stage);

            stage = new Stage
            {
                Number = 2,
                Title = "Еда",
                Description = "Стихотворение и новые слова"
            };
            stage.CheckList.Add(new CheckPoint { Text = "Стих" });
            goal.Stages.Add(stage);

            stage = new Stage
            {
                Number = 3,
                Title = "Животные",
                Description = "Три диалога, грамматика и новые слова"
            };
            stage.CheckList.Add(new CheckPoint { Text = "Ворона и Лисица" });
            stage.CheckList.Add(new CheckPoint { Text = "Муравей и Стрекоза" });
            stage.CheckList.Add(new CheckPoint { Text = "Кошка и мышь" });
            goal.Stages.Add(stage);
            return goal;
        }
    }
}
