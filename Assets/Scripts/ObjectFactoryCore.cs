using AxeMan.DungeonObject;
using AxeMan.GameSystem.Blueprint;
using System;
using UnityEngine;

namespace AxeMan.GameSystem.ObjectPool
{
    public interface IObjectFactory
    {
        GameObject CreateObject(IPrototype proto);

        void RemoveObject(GameObject go);
    }

    public class CreatingObjectEventArgs : EventArgs
    {
        public CreatingObjectEventArgs(GameObject go)
        {
            Data = go;
        }

        public GameObject Data { get; }
    }

    public class ObjectFactoryCore : MonoBehaviour, IObjectFactory
    {
        public event EventHandler<CreatingObjectEventArgs> CreatingObject;

        public GameObject CreateObject(IPrototype proto)
        {
            GameObject go = null;

            switch (proto.MTag)
            {
                case MainTag.Building:
                    go = GetComponent<OFBuilding>().CreateObject(proto);
                    break;

                case MainTag.Terrain:
                    break;

                case MainTag.Actor:
                    break;

                default:
                    break;
            }

            if (go != null)
            {
                OnCreatingObject(new CreatingObjectEventArgs(go));
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

        protected virtual void OnCreatingObject(CreatingObjectEventArgs e)
        {
            CreatingObject?.Invoke(this, e);
        }
    }
}
