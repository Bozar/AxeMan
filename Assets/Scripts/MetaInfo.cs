using AxeMan.GameSystem;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface IMetaInfo
    {
        MainTag MTag { get; }

        SubTag STag { get; }
    }

    public class MetaInfo : MonoBehaviour, IMetaInfo
    {
        public MetaInfo(MainTag mTag, SubTag sTag)
        {
            MTag = mTag;
            STag = sTag;
        }

        public MainTag MTag { get; }

        public SubTag STag { get; }
    }
}
