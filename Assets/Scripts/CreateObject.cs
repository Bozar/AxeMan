using AxeMan.DungeonObject;
using AxeMan.GameSystem.PrototypeFactory;
using System;
using UnityEngine;

namespace AxeMan.GameSystem.ObjectFactory
{
    public interface ICreateObject
    {
        GameObject Create(IPrototype proto);
    }

    public class CreatedObjectEventArgs : EventArgs
    {
        public CreatedObjectEventArgs(GameObject go)
        {
            Data = go;
        }

        public GameObject Data { get; }
    }

    public class CreateObject : MonoBehaviour, ICreateObject
    {
        public event EventHandler<CreatedObjectEventArgs> CreatedObject;

        public event EventHandler<CreatingObjectEventArgs> CreatingObject;

        public void Create(IPrototype[] proto)
        {
            foreach (IPrototype p in proto)
            {
                Create(p);
            }
        }

        public GameObject Create(IPrototype proto)
        {
            var objFromPool = new CreatingObjectEventArgs(proto);
            OnCreatingObject(objFromPool);

            GameObject go = objFromPool.Data;
            if (go == null)
            {
                go = Instantiate(Resources.Load(proto.STag.ToString())
                   as GameObject);
                go.AddComponent<MetaInfo>().SetValue(proto);
                go.AddComponent<LocalManager>();
                // Puslish an event to add specific components when necessary.
            }
            else
            {
                go.SetActive(true);
            }
            go.transform.position = GetComponent<ConvertCoordinate>().Convert(
                proto.Position);

            OnCreatedObject(new CreatedObjectEventArgs(go));
            return go;
        }

        protected virtual void OnCreatedObject(CreatedObjectEventArgs e)
        {
            CreatedObject?.Invoke(this, e);
        }

        protected virtual void OnCreatingObject(CreatingObjectEventArgs e)
        {
            CreatingObject?.Invoke(this, e);
        }
    }

    public class CreatingObjectEventArgs : EventArgs, IPrototype
    {
        public CreatingObjectEventArgs(IPrototype core)
        {
            MTag = core.MTag;
            STag = core.STag;
            Position = core.Position;
        }

        public GameObject Data { get; set; }

        public MainTag MTag { get; }

        public int[] Position { get; }

        public SubTag STag { get; }
    }
}
