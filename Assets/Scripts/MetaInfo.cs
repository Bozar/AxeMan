using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PrototypeFactory;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface IMetaInfo
    {
        MainTag MainTag { get; }

        SubTag SubTag { get; }
    }

    public class MetaInfo : MonoBehaviour, IMetaInfo
    {
        public MainTag MainTag { get; private set; }

        public SubTag SubTag { get; private set; }

        public void SetValue(IPrototype proto)
        {
            MainTag = proto.MTag;
            SubTag = proto.STag;
        }
    }
}
