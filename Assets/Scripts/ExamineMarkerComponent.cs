using AxeMan.DungeonObject;
using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.GameSystem.ObjectFactory
{
    public class ExamineMarkerComponent : MonoBehaviour
    {
        private void ExamineMarkerComponent_AddingComponent(object sender,
            AddingComponentEventArgs e)
        {
            if (e.Data.GetComponent<MetaInfo>().SubTag != SubTag.ExamineMarker)
            {
                return;
            }
            //e.Data.AddComponent<AimMarkerInputManager>().enabled = false;
            //e.Data.AddComponent<AimMarkerInputSwitcher>();
            e.Data.AddComponent<MarkerPosition>();
            e.Data.AddComponent<PCMove>();
        }

        private void Start()
        {
            GetComponent<CreateObject>().AddingComponent
                += ExamineMarkerComponent_AddingComponent;
        }
    }
}
