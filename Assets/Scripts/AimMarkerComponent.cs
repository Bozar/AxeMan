using AxeMan.DungeonObject;
using AxeMan.DungeonObject.PlayerInput;
using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.GameSystem.ObjectFactory
{
    public class AimMarkerComponent : MonoBehaviour
    {
        private void AimMarkerComponent_AddingComponent(object sender,
            AddingComponentEventArgs e)
        {
            if (e.Data.GetComponent<MetaInfo>().SubTag != SubTag.AimMarker)
            {
                return;
            }
            e.Data.AddComponent<AimMarkerInputManager>().enabled = false;
            e.Data.AddComponent<AimMarkerInputSwitcher>();
            e.Data.AddComponent<MarkerPosition>();
            e.Data.AddComponent<MarkerCheckTerrain>();
            e.Data.AddComponent<PCMove>();
        }

        private void Start()
        {
            GetComponent<CreateObject>().AddingComponent
                += AimMarkerComponent_AddingComponent;
        }
    }
}
