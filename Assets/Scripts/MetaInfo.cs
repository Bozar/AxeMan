using AxeMan.GameSystem;
using AxeMan.GameSystem.PrototypeFactory;
using UnityEngine;

namespace AxeMan.Actor
{
    public interface IMetaInfo
    {
        MainTag MTag { get; }

        SubTag STag { get; }
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
