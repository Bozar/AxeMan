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

            blueprint.Push(new ProtoObject(MainTag.Trap, SubTag.AirTrap,
                new int[] { 3, 2 }));
            blueprint.Push(new ProtoObject(MainTag.Trap, SubTag.AirTrap,
                new int[] { 4, 2 }));
            blueprint.Push(new ProtoObject(MainTag.Trap, SubTag.AirTrap,
                new int[] { 5, 2 }));

            blueprint.Push(new ProtoObject(MainTag.Trap, SubTag.AirTrap,
                new int[] { 3, 6 }));
            blueprint.Push(new ProtoObject(MainTag.Trap, SubTag.AirTrap,
                new int[] { 4, 6 }));
            blueprint.Push(new ProtoObject(MainTag.Trap, SubTag.AirTrap,
                new int[] { 5, 6 }));

            blueprint.Push(new ProtoObject(MainTag.Trap, SubTag.AirTrap,
                new int[] { 2, 3 }));
            blueprint.Push(new ProtoObject(MainTag.Trap, SubTag.AirTrap,
                new int[] { 2, 4 }));
            blueprint.Push(new ProtoObject(MainTag.Trap, SubTag.AirTrap,
                new int[] { 2, 5 }));

            blueprint.Push(new ProtoObject(MainTag.Trap, SubTag.AirTrap,
                new int[] { 6, 3 }));
            blueprint.Push(new ProtoObject(MainTag.Trap, SubTag.AirTrap,
                new int[] { 6, 4 }));
            blueprint.Push(new ProtoObject(MainTag.Trap, SubTag.AirTrap,
                new int[] { 6, 5 }));

            e.Data = blueprint.ToArray();
        }
    }
}
