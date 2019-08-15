using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.GameSystem.PrototypeFactory
{
    public class BlueprintExamineMarker : MonoBehaviour
    {
        private void BlueprintExamineMarker_DrawingBlueprint(object sender,
            DrawingBlueprintEventArgs e)
        {
            if (e.BlueprintTag != BlueprintTag.ExamineMarker)
            {
                return;
            }

            int invalid = -999;
            int[] position = new int[] { invalid, invalid };

            e.Data = new IPrototype[]
            {
                new ProtoObject(MainTag.Marker, SubTag.ExamineMarker, position)
            };
        }

        private void Start()
        {
            GetComponent<Blueprint>().DrawingBlueprint
                += BlueprintExamineMarker_DrawingBlueprint;
        }
    }
}
