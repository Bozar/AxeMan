using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.SearchGameObject;
using UnityEngine;

namespace AxeMan.GameSystem.PrototypeFactory
{
    public class BlueprintAimMarker : MonoBehaviour
    {
        private void BlueprintMarker_DrawingBlueprint(object sender,
            DrawingBlueprintEventArgs e)
        {
            if (e.BTag != BlueprintTag.AimMarker)
            {
                return;
            }

            GameObject pc = GetComponent<SearchObject>().Search(SubTag.PC)[0];
            int[] position = GetComponent<ConvertCoordinate>().Convert(
                pc.transform.position);

            e.Data = new IPrototype[]
            {
                new ProtoObject(MainTag.Marker, SubTag.AimMarker, position)
            };
        }

        private void Start()
        {
            GetComponent<Blueprint>().DrawingBlueprint
                += BlueprintMarker_DrawingBlueprint;
        }
    }
}
