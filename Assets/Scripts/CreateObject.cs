﻿using AxeMan.GameSystem.PrototypeFactory;
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
            GameObject go = null;
            var objFromPool = new CreatingObjectEventArgs(proto);
            OnCreatingObject(objFromPool);

            switch (objFromPool.MTag)
            {
                case MainTag.Building:
                    go = GetComponent<CreateBuilding>().Create(objFromPool);
                    break;

                case MainTag.Floor:
                    go = GetComponent<CreateFloor>().Create(objFromPool);
                    break;

                case MainTag.Trap:
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
