using AxeMan.DungeonObject;
using AxeMan.GameSystem.Blueprint;
using System;
using UnityEngine;

namespace AxeMan.GameSystem.ObjectPool
{
    public interface IObjectFactory
    {
        GameObject Create(IPrototype proto);

        void Remove(GameObject go);
    }

    public class CreatedObjectEventArgs : EventArgs
    {
        public CreatedObjectEventArgs(GameObject go)
        {
            Data = go;
        }

        public GameObject Data { get; }
    }

    public class ObjectFactoryCore : MonoBehaviour, IObjectFactory
    {
        public event EventHandler<CreatedObjectEventArgs> CreatedObject;

        public GameObject Create(IPrototype proto)
        {
            GameObject go = null;

            switch (proto.MTag)
            {
                case MainTag.Building:
                    go = GetComponent<OFBuilding>().Create(proto);
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
                OnCreatedObject(new CreatedObjectEventArgs(go));
            }
            return go;
        }

        public void Remove(GameObject go)
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

        protected virtual void OnCreatedObject(CreatedObjectEventArgs e)
        {
            CreatedObject?.Invoke(this, e);
        }
    }
}
