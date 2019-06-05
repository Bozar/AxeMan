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
        private Dictionary<DungeonObjectTag, IDungeonObject> board;

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
    }
}
