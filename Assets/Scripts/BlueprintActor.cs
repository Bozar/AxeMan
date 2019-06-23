using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem.PrototypeFactory
{
    public class BlueprintActor : MonoBehaviour
    {
        private void BlueprintActor_DrawingBlueprint(object sender,
            DrawingBlueprintEventArgs e)
        {
            Test(e);
        }

        private void Start()
        {
            GetComponent<Blueprint>().DrawingBlueprint
                += BlueprintActor_DrawingBlueprint;
        }

        private void Test(DrawingBlueprintEventArgs e)
        {
            if (e.BTag != BlueprintTag.Actor)
            {
                return;
            }

            Stack<IPrototype> blueprint = new Stack<IPrototype>();
            blueprint.Push(new ProtoObject(MainTag.Actor, SubTag.PC,
             new int[] { 0, 8 }));
            e.Data = blueprint.ToArray();
        }
    }
}
