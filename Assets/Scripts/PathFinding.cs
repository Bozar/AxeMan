using System;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public class PathFinding : MonoBehaviour
    {
        public event EventHandler<SearchObstacleEventArgs> SearchingObstacle;

        public void PublishSearchObstacle(SearchObstacleEventArgs e)
        {
            OnSearchingObstacle(e);
        }

        protected virtual void OnSearchingObstacle(SearchObstacleEventArgs e)
        {
            SearchingObstacle?.Invoke(this, e);
        }
    }

    public class SearchObstacleEventArgs : EventArgs
    {
        public SearchObstacleEventArgs(int[] pc, Stack<int[]> impassable,
            Stack<int[]> trap)
        {
            PC = pc;
            Impassable = impassable;
            Trap = trap;
        }

        public Stack<int[]> Impassable { get; set; }

        public int[] PC { get; set; }

        public Stack<int[]> Trap { get; set; }
    }
}
