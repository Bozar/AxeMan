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
            EnteringAimModeEventArgs e)
        {
            if (e.SubTag == SubTag.AimMarker)
            {
                return;
            }

            GameObject pc = GameCore.AxeManCore.GetComponent<SearchObject>()
                .Search(SubTag.PC)[0];
            int[] position = pc.GetComponent<LocalManager>().Position;

            GetComponent<LocalManager>().SetPosition(position);
            GameCore.AxeManCore.GetComponent<TileOverlay>().TryHideTile(position);
        }

        private void AimMarkerPosition_LeavingAimMode(object sender,
            EventArgs e)
        {
            int invalid = -999;
            int[] position = GetComponent<LocalManager>().Position;

            GetComponent<LocalManager>().SetPosition(
                new int[] { invalid, invalid });
            GameCore.AxeManCore.GetComponent<TileOverlay>().TryHideTile(position);
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
