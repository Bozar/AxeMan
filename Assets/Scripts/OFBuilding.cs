using AxeMan.DungeonObject;
using AxeMan.GameSystem.Blueprint;
using UnityEngine;

namespace AxeMan.GameSystem.ObjectPool
{
    public class OFBuilding : MonoBehaviour, IObjectFactory
    {
        public GameObject CreateObject(IPrototype proto)
        {
            GameObject go = Instantiate(Resources.Load(proto.STag.ToString())
                as GameObject);
            go.transform.position = GetComponent<ConvertCoordinate>().Convert(
                proto.Position);
            go.AddComponent<MetaInfo>().SetValue(proto);

            return go;
        }

        public void RemoveObject(GameObject go)
        {
            throw new System.NotImplementedException();
        }
    }
}
