using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public enum DungeonObjectTag { INVALID, Building, Terrain, Actor }

    public interface IDungeonBoard
    {
        bool AddObject(int x, int y, IDungeonObject ido, bool overwrite);

        bool ExistObject(int x, int y, DungeonObjectTag dot);

        IDungeonObject GetObject(int x, int y, DungeonObjectTag dot);

        IDungeonObject RemoveObject(int x, int y, DungeonObjectTag dot);
    }

    public interface IDungeonObject
    {
        DungeonObjectTag DataTag { get; }
    }

    // DungeonBoard is a data hub which records and updates every dungeon
    // object's position.
    public class DungeonBoard : MonoBehaviour, IDungeonBoard
    {
        private Dictionary<DungeonObjectTag, IDungeonObject>[,] board;

        public int DungeonHeight { get; private set; }

        public int DungeonWidth { get; private set; }

        public bool AddObject(int x, int y, IDungeonObject ido, bool overwrite)
        {
            throw new System.NotImplementedException();
        }

        public bool ExistObject(int x, int y, DungeonObjectTag dot)
        {
            throw new System.NotImplementedException();
        }

        public IDungeonObject GetObject(int x, int y, DungeonObjectTag dot)
        {
            throw new System.NotImplementedException();
        }

        public IDungeonObject RemoveObject(int x, int y, DungeonObjectTag dot)
        {
            throw new System.NotImplementedException();
        }

        private void Awake()
        {
            DungeonWidth = 9;
            DungeonHeight = 9;
            board = new Dictionary<DungeonObjectTag, IDungeonObject>[
                DungeonWidth, DungeonHeight];

            for (int i = 0; i < DungeonWidth; i++)
            {
                for (int j = 0; j < DungeonHeight; j++)
                {
                    board[i, j]
                        = new Dictionary<DungeonObjectTag, IDungeonObject>();
                }
            }
        }
    }
}
