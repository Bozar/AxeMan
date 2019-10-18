using AxeMan.GameSystem.GameDataTag;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem.PrototypeFactory
{
    public class BlueprintTrap : MonoBehaviour
    {
        private void BlueprintTrap_DrawingBlueprint(object sender,
            DrawingBlueprintEventArgs e)
        {
            if (e.BlueprintTag != BlueprintTag.Trap)
            {
                return;
            }
            Test(e);
        }

        private void Start()
        {
            GetComponent<Blueprint>().DrawingBlueprint
                += BlueprintTrap_DrawingBlueprint;
        }

        private void Test(DrawingBlueprintEventArgs e)
        {
            Stack<IPrototype> blueprint = new Stack<IPrototype>();

            for (int i = 2; i < 7; i++)
            {
                blueprint.Push(new ProtoObject(MainTag.Trap, SubTag.AirTrap,
                    new int[] { i, 1 }));
                blueprint.Push(new ProtoObject(MainTag.Trap, SubTag.AirTrap,
                    new int[] { i, 7 }));

                blueprint.Push(new ProtoObject(MainTag.Trap, SubTag.AirTrap,
                    new int[] { 1, i }));
                blueprint.Push(new ProtoObject(MainTag.Trap, SubTag.AirTrap,
                    new int[] { 7, i }));
            }

            e.Data = blueprint.ToArray();
        }
    }
}
