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
                    goal2(),
                    goal3(),
                    goal4()
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
                FinishDate = DateTime.Now.AddDays(7)
            };

            var stage = new Stage
            {
                Number = 1,
                Title = "Пн - 1000",
                Description = ""
            };
            goal.Stages.Add(stage);
            stage = new Stage
            {
                Number = 2,
                Title = "Вт - 2000",
                Description = ""
            };
            goal.Stages.Add(stage);
            stage = new Stage
            {
                Number = 3,
                Title = "Ср - 3000",
                Description = ""
            };
            goal.Stages.Add(stage);
            stage = new Stage
            {
                Number = 4,
                Title = "Чт - 4000",
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
                Title = "Сб - 6000",
                Description = ""
            };
            goal.Stages.Add(stage);
            stage = new Stage
            {
                Number = 7,
                Title = "Вс - 7000",
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
                FinishDate = DateTime.Now.AddDays(7)
            };

            var stage = new Stage
            {
                Number = 1,
                Title = "Моя семья",
                Description = "Пять текстов"
            };
            stage.CheckList.Add(new CheckPoint { Text = "Текст 1" });
            stage.CheckList.Add(new CheckPoint { Text = "Текст 2" });
            stage.CheckList.Add(new CheckPoint { Text = "Текст 3" });
            stage.CheckList.Add(new CheckPoint { Text = "Текст 4" });
            stage.CheckList.Add(new CheckPoint { Text = "Текст 5" });
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

        static Goal goal3()
        {
            var goal = new Goal
            {
                Title = "Немецкий",
                Description = "Пройти три темы",
                FinishDate = DateTime.Now.AddDays(7),
            };
            goal.Close(true);
            return goal;
        }

        static Goal goal4()
        {
            var goal = new Goal
            {
                Title = "Французский",
                Description = "Пройти три темы",
                FinishDate = DateTime.Now.AddDays(7),
            };
            goal.Close(false);
            return goal;
        }
    }
}
