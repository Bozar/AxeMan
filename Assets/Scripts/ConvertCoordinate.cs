using System;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface IConvertCoordinate
    {
        Vector3 Convert(int x, int y);

        int[] Convert(Vector3 coord);
    }

    public class ConvertCoordinate : MonoBehaviour, IConvertCoordinate
    {
        // [0, 0] is at the top-left corner.

        private readonly float originX = -5.5f;
        private readonly float originY = 0.5f;
        private readonly float step = 0.5f;

        public int[] Convert(Vector3 coord)
        {
            int x = (int)Math.Floor((coord.x - originX) / step);
            int y = (int)Math.Floor((coord.y - originY) / (-step));

            return new int[] { x, y };
        }

        public Vector3 Convert(int x, int y)
        {
            float floatX = originX + x * step;
            float floatY = originY - y * step;

            return new Vector3(floatX, floatY);
        }
    }
}
