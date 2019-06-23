using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem.PrototypeFactory
{
    public class BlueprintFloor : MonoBehaviour
    {
        private void BlueprintFloor_DrawingBlueprint(object sender,
            DrawingBlueprintEventArgs e)
        {
            if (e.BTag != BlueprintTag.Floor)
            {
                return;
            }

            Stack<IPrototype> blueprint = new Stack<IPrototype>();
            for (int i = 0; i < GetComponent<DungeonBoard>().Width; i++)
            {
                for (int j = 0; j < GetComponent<DungeonBoard>().Height;
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
            e.Data = blueprint.ToArray();
        }

        private void Start()
        {
            GetComponent<Blueprint>().DrawingBlueprint
                += BlueprintFloor_DrawingBlueprint;
        }
    }
}
