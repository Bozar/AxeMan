using AxeMan.GameSystem.GameDataTag;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem.PrototypeFactory
{
    public class BlueprintMarker : MonoBehaviour
    {
        private void BlueprintMarker_DrawingBlueprint(object sender,
            DrawingBlueprintEventArgs e)
        {
            if (e.BTag != BlueprintTag.Marker)
            {
                return;
            }

            int invalidPosition = -999;
            Stack<IPrototype> blueprint = new Stack<IPrototype>();

            blueprint.Push(new ProtoObject(MainTag.Marker, SubTag.AimMarker,
                new int[] { invalidPosition, invalidPosition }));

            e.Data = blueprint.ToArray();
        }

        private void Start()
        {
            GetComponent<Blueprint>().DrawingBlueprint
                += BlueprintMarker_DrawingBlueprint;
        }
    }
}
