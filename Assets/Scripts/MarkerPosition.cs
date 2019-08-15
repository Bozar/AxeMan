using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using System;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class MarkerPosition : MonoBehaviour
    {
        private MetaInfo pcMetaInfo;

        private void MarkerPosition_EnteringAimMode(object sender,
            EnterAimModeEventArgs e)
        {
            if (e.SubTag == SubTag.AimMarker)
            {
                return;
            }
            MoveMarkerToPC();
        }

        private void MarkerPosition_LeavingAimMode(object sender, EventArgs e)
        {
            ResetMarkerPosition();
        }

        private void MarkerPosition_SettingReference(object sender,
            SettingReferenceEventArgs e)
        {
            GameObject pc = e.PC;
            pcMetaInfo = pc.GetComponent<MetaInfo>();
        }

        private void MoveMarkerToPC()
        {
            int[] position = pcMetaInfo.Position;

            GetComponent<LocalManager>().SetPosition(position);
            GameCore.AxeManCore.GetComponent<TileOverlay>().TryHideTile(position);
        }

        private void ResetMarkerPosition()
        {
            int invalid = -999;
            int[] outOfScreen = new int[] { invalid, invalid };
            int[] current = GetComponent<MetaInfo>().Position;

            GetComponent<LocalManager>().SetPosition(outOfScreen);
            GameCore.AxeManCore.GetComponent<TileOverlay>().TryHideTile(current);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<Wizard>().SettingReference
                += MarkerPosition_SettingReference;

            GameCore.AxeManCore.GetComponent<AimMode>().EnteringAimMode
                += MarkerPosition_EnteringAimMode;
            GameCore.AxeManCore.GetComponent<AimMode>().LeavingAimMode
                += MarkerPosition_LeavingAimMode;
        }
    }
}
