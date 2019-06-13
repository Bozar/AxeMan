using UnityEngine;

namespace AxeMan.GameSystem.Blueprint
{
    public interface IBlueprint
    {
        IPrototype[] GetBlueprint();
    }

    public interface IPrototype
    {
        MainTag MTag { get; }

        int[] Position { get; }

        SubTag STag { get; }
    }

    public class BlueprintCore : MonoBehaviour
    {
    }

    public class ProtoObject : IPrototype
    {
        public ProtoObject(MainTag mTag, SubTag sTag, int[] position)
        {
            MTag = mTag;
            STag = sTag;
            Position = position;
        }

        public MainTag MTag { get; }

        public int[] Position { get; }

        public SubTag STag { get; }
    }
}
