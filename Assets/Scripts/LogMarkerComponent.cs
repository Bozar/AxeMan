using AxeMan.DungeonObject;
using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.GameSystem.ObjectFactory
{
    public class LogMarkerComponent : MonoBehaviour
    {
        private void LogMarkerComponent_AddingComponent(object sender,
            AddingComponentEventArgs e)
        {
            if (e.Data.GetComponent<MetaInfo>().SubTag != SubTag.LogMarker)
            {
                return;
            }
            //e.Data.AddComponent<LogMarkerInputManager>().enabled = false;
            //e.Data.AddComponent<LogMarkerInputSwitcher>();
        }

        private void Start()
        {
            GetComponent<CreateObject>().AddingComponent
                += LogMarkerComponent_AddingComponent;
        }
    }
}
