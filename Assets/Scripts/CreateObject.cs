using AxeMan.DungeonObject;
using AxeMan.DungeonObject.SearchGameObject;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PrototypeFactory;
using System;
using UnityEngine;

namespace AxeMan.GameSystem.ObjectFactory
{
    public interface ICreateObject
    {
        GameObject Create(IPrototype proto);
    }

    public class AddingComponentEventArgs : EventArgs
    {
        public AddingComponentEventArgs(GameObject data)
        {
            Data = data;
        }

        public GameObject Data { get; set; }
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
        public event EventHandler<AddingComponentEventArgs> AddingComponent;

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
                go = Instantiate(Resources.Load(proto.SubTag.ToString())
                   as GameObject);
                go.AddComponent<MetaInfo>().SetValue(proto);
                go.AddComponent<LocalManager>();
                go.AddComponent<SubscribeSearch>();

                // Puslish an event to add specific components when necessary.
                OnAddingComponent(new AddingComponentEventArgs(go));
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

        protected virtual void OnAddingComponent(AddingComponentEventArgs e)
        {
            AddingComponent?.Invoke(this, e);
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
            MainTag = core.MainTag;
            SubTag = core.SubTag;
            Position = core.Position;
        }

        public GameObject Data { get; set; }

        public MainTag MainTag { get; }

        public int[] Position { get; }

        public SubTag SubTag { get; }
    }
}
