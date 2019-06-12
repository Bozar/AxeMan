using AxeMan.DungeonObject;
using UnityEngine;

namespace AxeMan.GameSystem.ObjectPool
{
    public interface IObjectPool
    {
        GameObject CreateObject(MainTag mTag, SubTag sTag);

        void RemoveObject(GameObject go);
    }

    public class ObjectPoolCore : MonoBehaviour, IObjectPool
    {
        public GameObject CreateObject(MainTag mTag, SubTag sTag)
        {
            GameObject go = null;

            switch (mTag)
            {
                case MainTag.Building:
                    break;

                case MainTag.Terrain:
                    break;

                case MainTag.Actor:
                    break;

                default:
                    break;
            }
            return go;
        }

        public void RemoveObject(GameObject go)
        {
            if (go.GetComponent<MetaInfo>() == null)
            {
                return;
            }

            switch (go.GetComponent<MetaInfo>().MTag)
            {
                case MainTag.Building:
                    break;

                case MainTag.Terrain:
                    break;

                case MainTag.Actor:
                    break;

                default:
                    break;
            }
        }
    }
}
