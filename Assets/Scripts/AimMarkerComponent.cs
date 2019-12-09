using AxeMan.DungeonObject;
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
            e.Data.AddComponent<AimMarkerPosition>();
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
