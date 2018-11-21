using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerLib
{
    static class MovingExtention
    {
        public static void MoveElement<T>(this IList<T> sourceList, T element, MoveType moveType)
        {
            var index = sourceList.IndexOf(element);
            if (index < 0) return;

            switch (moveType)
            {
                case MoveType.StepUp:
                    moveStep(sourceList, index, -1);
                    break;
                case MoveType.StepDown:
                    moveStep(sourceList, index, 1);
                    break;
                case MoveType.ToBegin:
                    moveToBegin(sourceList, index);
                    break;
                case MoveType.ToEnd:
                    moveToEnd(sourceList, index);
                    break;
            }
        }

        private static void moveStep<T>(IList<T> sourceList, int fromIndex, int step)
        {
            int toIndex = fromIndex + step;
            if (fromIndex == toIndex) return;
            if (toIndex < 0 || toIndex > sourceList.Count - 1) return;

            var stage = sourceList[fromIndex];
            sourceList[fromIndex] = sourceList[toIndex];
            sourceList[toIndex] = stage;
        }

        private static void moveToBegin<T>(IList<T> sourceList, int stageIndex)
        {
            if (stageIndex < 1) return;
            var stage = sourceList[stageIndex];
            sourceList.RemoveAt(stageIndex);
            sourceList.Insert(0, stage);
        }

        private static void moveToEnd<T>(IList<T> sourceList, int stageIndex)
        {
            if (stageIndex > sourceList.Count - 2) return;
            var stage = sourceList[stageIndex];
            sourceList.RemoveAt(stageIndex);
            sourceList.Add(stage);
        }
    }

    public enum MoveType
    {
        StepUp,
        StepDown,
        ToBegin,
        ToEnd
    }
}
