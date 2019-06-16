using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface IDungeonBoard
    {
        int DungeonHeight { get; }

        int DungeonWidth { get; }

        bool IndexOutOfRange(int x, int y);
    }

    public class DungeonBoard : MonoBehaviour, IDungeonBoard
    {
        public int DungeonHeight { get; private set; }

        public int DungeonWidth { get; private set; }

        public bool IndexOutOfRange(int x, int y)
        {
            return (x < 0)
                || (x >= DungeonWidth)
                || (y < 0)
                || (y >= DungeonHeight);
        }

        private void Awake()
        {
            DungeonWidth = 9;
            DungeonHeight = 9;
        }
    }
}
