using System;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface IDistance
    {
        int GetDistance(int[] source, int[] target);

        int[][] GetNeighbor(int[] source);

        int[] GetRelativePosition(int[] source, int[] target);
    }

    public class Distance : MonoBehaviour, IDistance
    {
        public int GetDistance(int[] source, int[] target)
        {
            int x = Math.Abs(Math.Abs(source[0]) - Math.Abs(target[0]));
            int y = Math.Abs(Math.Abs(source[1]) - Math.Abs(target[1]));

            return x + y;
        }

        public int[][] GetNeighbor(int[] source)
        {
            int x = source[0];
            int y = source[1];
            int[][] neighbor = new int[][]
            {
                new int[] { x - 1, y },
                new int[] { x + 1, y },
                new int[] { x, y - 1 },
                new int[] { x, y + 1 },
            };
            Stack<int[]> valid = new Stack<int[]>();

            foreach (int[] position in neighbor)
            {
                if (!GetComponent<DungeonBoard>().IndexOutOfRange(
                    position[0], position[1]))
                {
                    valid.Push(position);
                }
            }
            return valid.ToArray();
        }

        public int[] GetRelativePosition(int[] source, int[] target)
        {
            int x = target[0] - source[0];
            int y = target[1] - source[1];

            return new int[] { x, y };
        }
    }
}
