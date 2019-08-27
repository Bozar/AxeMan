using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PrototypeFactory;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface IMetaInfo
    {
        MainTag MainTag { get; }

        int ObjectID { get; }

        string ObjectName { get; }

        int[] Position { get; }

        SubTag SubTag { get; }
    }

    public class MetaInfo : MonoBehaviour, IMetaInfo
    {
        public MainTag MainTag { get; private set; }

        public int ObjectID { get { return gameObject.GetInstanceID(); } }

        public string ObjectName
        {
            get
            {
                return GameCore.AxeManCore.GetComponent<ActorData>()
                    .GetStringData(SubTag, ActorDataTag.Name);
            }
        }

        public int[] Position
        {
            get
            {
                return GameCore.AxeManCore.GetComponent<ConvertCoordinate>()
                    .Convert(transform.position);
            }
        }

        public SubTag SubTag { get; private set; }

        public void SetValue(IPrototype proto)
        {
            if ((MainTag == MainTag.INVALID) && (SubTag == SubTag.INVALID))
            {
                MainTag = proto.MainTag;
                SubTag = proto.SubTag;
            }
        }
    }
}
