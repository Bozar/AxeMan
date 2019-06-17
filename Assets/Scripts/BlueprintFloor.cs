using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem.PrototypeFactory
{
    public class BlueprintFloor : MonoBehaviour, IBlueprint
    {
        public IPrototype[] GetBlueprint()
        {
            Stack<IPrototype> blueprint = new Stack<IPrototype>();

            for (int i = 0; i < GetComponent<DungeonBoard>().DungeonWidth; i++)
            {
                for (int j = 0; j < GetComponent<DungeonBoard>().DungeonHeight;
                    j++)
                {
                    if (GetComponent<SearchObject>().Search(i, j,
                        MainTag.Building, out _))
                    {
                        continue;
                    }
                    blueprint.Push(new ProtoObject(MainTag.Floor, SubTag.Floor,
                        new int[] { i, j }));
                }
            }
            return blueprint.ToArray();
        }
    }
}
