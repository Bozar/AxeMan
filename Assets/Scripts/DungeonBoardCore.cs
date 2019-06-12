using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface IDungeonBoard
    {
        bool AddObject(int x, int y, IDungeonObject ido, bool overwrite);

        bool ExistObject(int x, int y, MainTag mtag);

        IDungeonObject GetObject(int x, int y, MainTag mtag);

        IDungeonObject RemoveObject(int x, int y, MainTag mtag);
    }

    public interface IDungeonObject
    {
        MainTag DataTag { get; }

        // The actual data could be GameObject, GameObject[], or
        // Dictionary<MainTag, GameObject>.
    }

    public class DungeonBoardCore : MonoBehaviour
    {
    }

    public class DungeonObject : IDungeonObject
    {
        public DungeonObject(MainTag mTag, GameObject data)
        {
            DataTag = mTag;
            Data = data;
        }

        public GameObject Data { get; }

        public MainTag DataTag { get; }
    }
}
