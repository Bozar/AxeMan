using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface IDungeonBoard
    {
        int Height { get; }

        int Width { get; }

        bool IndexOutOfRange(int x, int y);
    }

    public class DungeonBoard : MonoBehaviour, IDungeonBoard
    {
        public int Height { get; private set; }

        public int Width { get; private set; }

        public bool IndexOutOfRange(int x, int y)
        {
            return (x < 0)
                || (x >= Width)
                || (y < 0)
                || (y >= Height);
        }

        private void Awake()
        {
            Width = 9;
            Height = 9;
        }
    }
}
