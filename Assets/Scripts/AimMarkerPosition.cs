using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class AimMarkerPosition : MonoBehaviour
    {
        private void AimMarkerPosition_EnteringAimMode(object sender,
            EventArgs e)
        {
            GameObject pc = GameCore.AxeManCore.GetComponent<SearchObject>()
                .Search(SubTag.PC)[0];
            Vector3 v3Position = pc.transform.position;
            int[] intPosition = pc.GetComponent<LocalManager>().GetPosition();

            gameObject.transform.position = v3Position;
            GameCore.AxeManCore.GetComponent<TileOverlay>().TryHideTile(
                intPosition);
        }

        private void AimMarkerPosition_LeavingAimMode(object sender,
            EventArgs e)
        {
            int invalid = -999;
            int[] position
                = GameCore.AxeManCore.GetComponent<ConvertCoordinate>()
                .Convert(gameObject.transform.position);

            gameObject.transform.position
                = GameCore.AxeManCore.GetComponent<ConvertCoordinate>()
                .Convert(new int[] { invalid, invalid });
            GameCore.AxeManCore.GetComponent<TileOverlay>()
                .TryHideTile(position);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<AimMode>().EnteringAimMode
                += AimMarkerPosition_EnteringAimMode;
            GameCore.AxeManCore.GetComponent<AimMode>().LeavingAimMode
                += AimMarkerPosition_LeavingAimMode;
        }
    }
}
