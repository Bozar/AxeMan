using AxeMan.DungeonObject;
using AxeMan.GameSystem.GameDataTag;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem.ObjectFactory
{
    public interface IObjectPool
    {
        GameObject LoadFromPool(SubTag sTag);

        void SaveToPool(GameObject go);
    }

    public class ObjectPool : MonoBehaviour, IObjectPool
    {
        private Dictionary<SubTag, Stack<GameObject>> pool;

        public GameObject LoadFromPool(SubTag sTag)
        {
            if (pool.TryGetValue(sTag, out Stack<GameObject> goStack)
                && (goStack.Count > 0))
            {
                return pool[sTag].Pop();
            }
            return null;
        }

        public void SaveToPool(GameObject go)
        {
            if (go.GetComponent<MetaInfo>() == null)
            {
                throw new System.Exception("Require: MetaInfo.");
            }

            SubTag tag = go.GetComponent<MetaInfo>().SubTag;
            if (!pool.ContainsKey(tag))
            {
                pool[tag] = new Stack<GameObject>();
            }
            pool[tag].Push(go);
        }

        private void Awake()
        {
            pool = new Dictionary<SubTag, Stack<GameObject>>();
        }

        private void ObjectPool_CreatingObject(object sender,
            CreatingObjectEventArgs e)
        {
            e.Data = LoadFromPool(e.SubTag);
        }

        private void ObjectPool_RemovingObject(object sender,
            RemovingObjectEventArgs e)
        {
            SaveToPool(e.Data);
        }

        private void Start()
        {
            GetComponent<CreateObject>().CreatingObject
                += ObjectPool_CreatingObject;
            GetComponent<RemoveObject>().RemovingObject
                += ObjectPool_RemovingObject;
        }
    }
}
