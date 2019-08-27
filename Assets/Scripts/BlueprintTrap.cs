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

            blueprint.Push(new ProtoObject(MainTag.Trap, SubTag.EarthTrap,
                new int[] { 0, 0 }));
            blueprint.Push(new ProtoObject(MainTag.Trap, SubTag.FireTrap,
                new int[] { 0, 1 }));
            blueprint.Push(new ProtoObject(MainTag.Trap, SubTag.AirTrap,
                new int[] { 0, 2 }));
            blueprint.Push(new ProtoObject(MainTag.Trap, SubTag.WaterTrap,
                new int[] { 0, 3 }));

            e.Data = blueprint.ToArray();
        }
    }
}
