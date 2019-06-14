using AxeMan.GameSystem;
using AxeMan.GameSystem.PrototypeFactory;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface IMetaInfo
    {
        MainTag MTag { get; }

        SubTag STag { get; }

        void SetValue(IPrototype proto);
    }

    public class MetaInfo : MonoBehaviour, IMetaInfo
    {
        public MainTag MTag { get; private set; }

        public SubTag STag { get; private set; }

        public void SetValue(IPrototype proto)
        {
            MTag = proto.MTag;
            STag = proto.STag;
        }
    }
}
