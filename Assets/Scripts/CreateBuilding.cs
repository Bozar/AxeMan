﻿using AxeMan.DungeonObject;
using AxeMan.GameSystem.PrototypeFactory;
using UnityEngine;

namespace AxeMan.GameSystem.ObjectFactory
{
    public class CreateBuilding : MonoBehaviour, ICreateObject
    {
        public GameObject Create(IPrototype proto)
        {
            GameObject go = (proto as CreatingObjectEventArgs).Data;

            if (go == null)
            {
                go = Instantiate(Resources.Load(proto.STag.ToString())
                   as GameObject);
                go.AddComponent<MetaInfo>().SetValue(proto);
            }
            else
            {
                go.SetActive(true);
            }
            go.transform.position = GetComponent<ConvertCoordinate>().Convert(
                proto.Position);

            return go;
        }
    }
}