using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.GameSystem.PrototypeFactory
{
    public class BlueprintStartScreenCursor : MonoBehaviour
    {
        private void BlueprintStartScreenCursor_DrawingBlueprint(object sender,
            DrawingBlueprintEventArgs e)
        {
            if (e.BlueprintTag != BlueprintTag.StartScreenCursor)
            {
                return;
            }

            int invalid = -999;
            int[] position = new int[] { invalid, invalid };

            e.Data = new IPrototype[]
            {
                new ProtoObject(MainTag.Marker, SubTag.StartScreenCursor,
                position)
            };
        }

        private void Start()
        {
            GetComponent<Blueprint>().DrawingBlueprint
                += BlueprintStartScreenCursor_DrawingBlueprint;
        }
    }
}
