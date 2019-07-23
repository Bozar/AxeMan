using System;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface IDistance
    {
        int GetDistance(int[] source, int[] target);

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

        public int[] GetRelativePosition(int[] source, int[] target)
        {
            int x = target[0] - source[0];
            int y = target[1] - source[1];

            return new int[] { x, y };
        }
    }
}
