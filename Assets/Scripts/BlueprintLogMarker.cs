using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.GameSystem.PrototypeFactory
{
    public class BlueprintLogMarker : MonoBehaviour
    {
        private void BlueprintLogMarker_DrawingBlueprint(object sender,
            DrawingBlueprintEventArgs e)
        {
            if (e.BlueprintTag != BlueprintTag.LogMarker)
            {
                return;
            }

            int invalid = -999;
            int[] position = new int[] { invalid, invalid };

            e.Data = new IPrototype[]
            {
                new ProtoObject(MainTag.Marker, SubTag.LogMarker, position)
            };
        }

        private void Start()
        {
            GetComponent<Blueprint>().DrawingBlueprint
                += BlueprintLogMarker_DrawingBlueprint;
        }
    }
}
