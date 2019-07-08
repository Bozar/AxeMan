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
            if (e.Data.GetComponent<MetaInfo>().STag != SubTag.AimMarker)
            {
                return;
            }
            Debug.Log("Add AimMarker components;");
        }

        private void Start()
        {
            GetComponent<CreateObject>().AddingComponent
                += AimMarkerComponent_AddingComponent;
        }
    }
}
