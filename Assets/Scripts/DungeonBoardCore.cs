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
        GameObject Data { get; }

        MainTag MTag { get; }
    }

    public class DungeonBoardCore : MonoBehaviour
    {
    }

    public class DungeonObject : IDungeonObject
    {
        public DungeonObject(MainTag mTag, GameObject data)
        {
            MTag = mTag;
            Data = data;
        }

        public GameObject Data { get; }

        public MainTag MTag { get; }
    }
}
