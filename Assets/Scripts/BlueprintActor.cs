using AxeMan.GameSystem.GameDataTag;
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
            if (e.BlueprintTag != BlueprintTag.Actor)
            {
                return;
            }

            // TODO: Change this later.
            Stack<IPrototype> blueprint = new Stack<IPrototype>();

            blueprint.Push(new ProtoObject(MainTag.Actor, SubTag.PC,
                new int[] { 0, 8 }));
            blueprint.Push(new ProtoObject(MainTag.Actor, SubTag.Dummy,
                new int[] { 0, 0 }));
            blueprint.Push(new ProtoObject(MainTag.Actor, SubTag.Dummy,
                new int[] { 0, 4 }));
            blueprint.Push(new ProtoObject(MainTag.Actor, SubTag.Dummy,
                new int[] { 8, 0 }));
            blueprint.Push(new ProtoObject(MainTag.Actor, SubTag.Dummy,
                new int[] { 8, 2 }));
            blueprint.Push(new ProtoObject(MainTag.Actor, SubTag.Dummy,
                new int[] { 8, 4 }));

            e.Data = blueprint.ToArray();
        }
    }
}
