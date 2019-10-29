using AxeMan.DungeonObject;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PrototypeFactory;
using System;
using System.Collections.Generic;
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

        public GameObject Data { get; }
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

        public GameObject[] Create(IPrototype[] proto)
        {
            GameObject go;
            Stack<GameObject> goStack = new Stack<GameObject>();

            foreach (IPrototype p in proto)
            {
                go = Create(p);
                goStack.Push(go);
            }
            return goStack.ToArray();
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

                // Puslish an event to add specific components when necessary.
                OnAddingComponent(new AddingComponentEventArgs(go));
            }
            else
            {
                go.SetActive(true);
                // TODO: Puslish an event to reset object data.
            }
            go.transform.position = GetComponent<ConvertCoordinate>().Convert(
                proto.Position);

            // Test code.
            if (go.GetComponent<MetaInfo>().SubTag == SubTag.LifeAltar)
            {
                go.GetComponent<SpriteRenderer>().sprite
                    = Resources.LoadAll<Sprite>("curses_vector_32x48")[29];
            }

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
