﻿using System;
using UnityEngine;

namespace AxeMan.GameSystem.ObjectFactory
{
    public interface IRemoveObject
    {
        void Remove(GameObject go);
    }

    public class RemoveObject : MonoBehaviour, IRemoveObject
    {
        public event EventHandler<RemovingObjectEventArgs> RemovingObject;

        public void Remove(GameObject go)
        {
            int invalidCoord = -999;

            OnRemovingObject(new RemovingObjectEventArgs(go));
            go.transform.position = GetComponent<ConvertCoordinate>().Convert(
                invalidCoord, invalidCoord);
            go.SetActive(false);
        }

        protected virtual void OnRemovingObject(RemovingObjectEventArgs e)
        {
            RemovingObject?.Invoke(this, e);
        }
    }

    public class RemovingObjectEventArgs : EventArgs
    {
        public RemovingObjectEventArgs(GameObject data)
        {
            Data = data;
        }

        public GameObject Data { get; }
    }
}
