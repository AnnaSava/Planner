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
            stage.CheckLists.Add(new CheckList
            {
                Title = "Тексты",
                Items = new List<CheckPoint>
                    {
                        new CheckPoint{ Text = "Мамина помада" },
                        new CheckPoint{ Text = "Папины трусы" },
                    }
            });
            stage.CheckLists.Add(new CheckList
            {
                Title = "Слова",
                Items = new List<CheckPoint>
                    {
                        new CheckPoint{ Text = "мама" },
                        new CheckPoint{ Text = "папа" },
                        new CheckPoint{ Text = "брат" },
                        new CheckPoint{ Text = "сестра" },
                    }
            });
            goal.Stages.Add(stage);

            stage = new Stage
            {
                Number = 2,
                Title = "Еда",
                Description = "Стихотворение и новые слова"
            };
            stage.CheckLists.Add(new CheckList
            {
                Title = "Стихотворение",
                Items = new List<CheckPoint>
                    {
                        new CheckPoint{ Text = "Ода капусте" }
                    }
            });
            stage.CheckLists.Add(new CheckList
            {
                Title = "Слова",
                Items = new List<CheckPoint>
                    {
                        new CheckPoint{ Text = "морковь" },
                        new CheckPoint{ Text = "капуста" },
                        new CheckPoint{ Text = "овощи" }
                    }
            });
            goal.Stages.Add(stage);

            stage = new Stage
            {
                Number = 3,
                Title = "Животные",
                Description = "Три диалога, грамматика и новые слова"
            };
            stage.CheckLists.Add(new CheckList
            {
                Title = "Диалоги",
                Items = new List<CheckPoint>
                    {
                        new CheckPoint{ Text = "Ворона и Лисица" },
                        new CheckPoint{ Text = "Муравей и Стрекоза" },
                        new CheckPoint{ Text = "Кошка и мышь" }
                    }
            });
            stage.CheckLists.Add(new CheckList
            {
                Title = "Слова",
                Items = new List<CheckPoint>
                    {
                        new CheckPoint{ Text = "ворона" },
                        new CheckPoint{ Text = "стрекоза" }
                    }
            });
            stage.CheckLists.Add(new CheckList
            {
                Title = "Грамматика",
                Items = new List<CheckPoint>
                    {
                        new CheckPoint{ Text = "Прошедшее время" },
                        new CheckPoint{ Text = "Неправильные глаголы" }
                    }
            });
            goal.Stages.Add(stage);
            return goal;
        }
    }
}
